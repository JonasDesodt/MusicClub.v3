using Microsoft.AspNetCore.Components;
using MusicClub.v3.Dto.Transfer;
using MusicClub.v3.Abstractions.Services;
using MusicClub.v3.Dto.Helpers;
using MusicClub.v3.Cms.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace MusicClub.v3.Cms.Controllers
{
    public abstract class DataControllerBase(NavigationManager navigationManager, MemoryService memoryService, AuthenticationStateProvider authenticationStateProvider)
    {
        public object? Data { get; set; } //todo: make private & add method that gets and deletes the data

        public event EventHandler<bool>? OnFetchStateChanged;

        private CancellationTokenSource? _cancellationSource;

        public async Task<bool> HandleLocationChanged(string targetLocation)
        {
            //if ((await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity?.IsAuthenticated is not true)
            //{
            //    return true;
            //}

            Uri uri;

            var domain = new Uri(navigationManager.Uri).GetLeftPart(UriPartial.Authority);

            if (!targetLocation.StartsWith(domain))
            {
                uri = new Uri(domain + targetLocation);
            }
            else
            {
                uri = new Uri(targetLocation);
            }

            var route = uri.GetLeftPart(UriPartial.Path).Replace(domain, string.Empty);

            return await HandleRoute(route);
        }


        public async Task<TResult> Fetch<TResult>(Func<Task<TResult>> function)
        {
            Data = null;
            _cancellationSource = new CancellationTokenSource();

            OnFetchStateChanged?.Invoke(this, true);

            var task = function();

            var result = await task.WaitAsync(_cancellationSource.Token);

            OnFetchStateChanged?.Invoke(this, false);

            return result;
        }

        public async Task CancelCurrentFetch()
        {
            if (_cancellationSource is not null)
            {
                await _cancellationSource.CancelAsync();
                //_cancellationSource.Dispose();

                OnFetchStateChanged?.Invoke(this, false);
                Data = null;
            }
        }

        public void InvokeOnFetchStateChanged()
        {
            OnFetchStateChanged?.Invoke(this, true);
        }

        protected async Task<bool> HandleIndexRouteMatchFound<TDataRequest, TDataResult, TFilterRequest, TFilterResult>(IService<TDataRequest, TDataResult, TFilterRequest, TFilterResult> apiService, IFilterResponseHelpers<TFilterRequest, TFilterResult> filterResponseHelpers) where TFilterResult : class where TFilterRequest : new()
        {
            var paginationRequest = new PaginationRequest
            {
                Page = memoryService.Get<PaginationResponse>()?.Page ?? MemoryService.DefaultPage,
                PageSize = memoryService.Get<PaginationResponse>()?.PageSize ?? MemoryService.DefaultPageSize
            };

            var filterResult = memoryService.Get<TFilterResult>();
            TFilterRequest? filterRequest;
            if (filterResult is not null)
            {
                filterRequest = filterResponseHelpers.ToRequest(filterResult);
                //filterRequest = filterResult.ToRequest();
            }
            else
            {
                filterRequest = new TFilterRequest();
            }

            Data = await Fetch(async () => await apiService.GetAll(paginationRequest, filterRequest));

            if (Data is PagedServiceResult<IList<TDataResult>, TFilterResult> pagedServiceResult)
            {
                memoryService.Set(pagedServiceResult.Filter);
                memoryService.Set(pagedServiceResult.PaginationResponse);

                return pagedServiceResult.Messages?.HasMessage is not true;
            }

            return false;
        }

        protected async Task<bool> HandleDeleteEditRouteMatchFound<TDataRequest, TDataResult, TFilterRequest, TFilterResult>(IService<TDataRequest, TDataResult, TFilterRequest, TFilterResult> apiService, string route) where TFilterResult : class?
        {
            Data = await Fetch(async () => await apiService.Get(int.Parse(route.Split('/').Last())));

            if (Data is ServiceResult<TDataResult> serviceResult)
            {
                return serviceResult.Messages?.HasMessage is not true;
            }

            return false;
        }

        protected abstract Task<bool> HandleRoute(string route);

        protected virtual Task<bool> HandleCustomRoute(string route)
        {
            return Task.FromResult(true);
        }
    }
}
