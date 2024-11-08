using MusicClub.v3.ApiServices.Helpers;
using MusicClub.v3.Dto.Enums;
using MusicClub.v3.Dto.Transfer;
using System.Net.Http.Json;
using MusicClub.v3.Dto.Extensions;

namespace MusicClub.v3.ApiServices.Extensions
{
    internal static class HttpClientFactoryExtensions
    {
        public static async Task<ServiceResult<TDataResult>> Create<TDataRequest, TDataResult>(this IHttpClientFactory httpClientFactory, string client, string endpoint, TDataRequest dataRequest)
        {
            var httpClient = httpClientFactory.CreateClient(client);

            var httpResponseMessage = await httpClient.PostAsJsonAsync(endpoint, dataRequest);

            if (!httpResponseMessage.IsSuccessStatusCode || await httpResponseMessage.Content.ReadFromJsonAsync<ServiceResult<TDataResult>>() is not { } serviceResult)
            {
                return new ServiceResult<TDataResult>
                {
                    Messages = [new ServiceMessage { Code = ErrorCode.FetchError, Description = "Fetch error." }],
                };
            }

            return serviceResult;
        }

        public static async Task<ServiceResult<TDataResult>> Delete<TDataResult>(this IHttpClientFactory httpClientFactory, string client, string endpoint, int id)
        {
            var httpClient = httpClientFactory.CreateClient(client);

            var httpResponseMessage = await httpClient.DeleteAsync(endpoint + id);

            if (!httpResponseMessage.IsSuccessStatusCode || await httpResponseMessage.Content.ReadFromJsonAsync<ServiceResult<TDataResult>>() is not { } serviceResult)
            {
                return new ServiceResult<TDataResult>
                {
                    Messages = [new ServiceMessage { Code = ErrorCode.FetchError, Description = $"Failed to fetch the {typeof(TDataResult)}" }],
                };
            }

            return serviceResult;
        }

        public static async Task<ServiceResult<TDataResult>> Get<TDataResult>(this IHttpClientFactory httpClientFactory, string client, string endpoint, int id)
        {
            var httpClient = httpClientFactory.CreateClient(client);

            var httpResponseMessage = await httpClient.GetAsync(endpoint + id);

            if (!httpResponseMessage.IsSuccessStatusCode || await httpResponseMessage.Content.ReadFromJsonAsync<ServiceResult<TDataResult>>() is not { } serviceResult)
            {
                return new ServiceResult<TDataResult>
                {
                    Messages = [new ServiceMessage { Code = ErrorCode.FetchError, Description = $"Failed to fetch the {typeof(TDataResult)}." }],
                };
            }

            return serviceResult;
        }

        public static async Task<PagedServiceResult<IList<TDataResult>, TFilterResult>> GetAll<TDataResult, TFilterRequest, TFilterResult>(this IHttpClientFactory httpClientFactory, IFilterRequestHelpers<TFilterRequest, TFilterResult> filterRequestHelpers, string client, string endpoint, PaginationRequest paginationRequest, TFilterRequest filterRequest) //where TFilterRequest : IFilterRequestConverter<TFilterResult>
        {
            var httpClient = httpClientFactory.CreateClient(client);

            var httpResponseMessage = await TryCatchHelpers.HandleHttpRequestExceptions(async () => await httpClient.GetAsync(endpoint + paginationRequest.ToQueryString() + filterRequestHelpers.ToQueryString(filterRequest)));

            //TODO: verify async implementation, also in HandleHttpRequestExceptions!!

            //var httpResponseMessage = TryCatchHttpRequestHelpers.HandleHttpRequestExceptions(() => httpClient.GetAsync(endpoint + paginationRequest.ToQueryString() + filterRequest.ToQueryString()));


            if (httpResponseMessage is null || !httpResponseMessage.IsSuccessStatusCode || await httpResponseMessage.Content.ReadFromJsonAsync<PagedServiceResult<IList<TDataResult>, TFilterResult>>() is not { } pagedServiceResult)
            {
                return new PagedServiceResult<IList<TDataResult>, TFilterResult>
                {
                    Messages = [new ServiceMessage { Code = ErrorCode.FetchError, Description = $"Failed to fetch the {typeof(TDataResult)}s." }],
                    PaginationResponse = paginationRequest.ToResponse(0),
                    Filter = filterRequestHelpers.ToResult(filterRequest)
                };
            }

            return pagedServiceResult;
        }

        public static async Task<ServiceResult<TDataResult>> Update<TDataRequest, TDataResult>(this IHttpClientFactory httpClientFactory, string client, string endpoint, int id, TDataRequest request)
        {
            var httpClient = httpClientFactory.CreateClient(client);

            var httpResponseMessage = await httpClient.PutAsJsonAsync(endpoint + id, request);

            if (!httpResponseMessage.IsSuccessStatusCode || await httpResponseMessage.Content.ReadFromJsonAsync<ServiceResult<TDataResult>>() is not { } serviceResult)
            {
                return new ServiceResult<TDataResult>
                {
                    Messages = [new ServiceMessage { Code = ErrorCode.FetchError, Description = $"Failed to update the {typeof(TDataRequest)}." }],
                };
            }

            return serviceResult;
        }
    }
}