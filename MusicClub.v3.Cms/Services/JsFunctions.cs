using Microsoft.JSInterop;

namespace MusicClub.v3.Cms.Services
{
    public class JsFunctions(IJSRuntime jsRuntime)
    {
        public async Task ScrollTo(string id)
        {
            await jsRuntime.InvokeVoidAsync("eval", $"document.getElementById('{id}').scrollIntoView();");
        }

        public async Task ScrollToTop()
        {
            await jsRuntime.InvokeVoidAsync("eval", "window.scrollTo(0, 0);");
        }

        public async Task AddHandleOnScroll()
        {
            await jsRuntime.InvokeVoidAsync("eval",
            "window.onscroll = function() { if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) { document.body.classList.add(\"scroll\"); } else { document.body.classList.remove(\"scroll\");} }");
        }
    }
}
