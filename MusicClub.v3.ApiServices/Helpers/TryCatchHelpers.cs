namespace MusicClub.v3.ApiServices.Helpers
{
    internal static class TryCatchHelpers
    {
        public static async Task<HttpResponseMessage?> HandleHttpRequestExceptions(Func<Task<HttpResponseMessage>> httpRequest)
        {
            HttpResponseMessage? httpResponseMessage;

            try
            {
                httpResponseMessage = await httpRequest.Invoke();
            }
            catch (Exception ex)
            {
                return ex switch
                {
                    //TODO: logs exceptions, rethrow if not expected
                    _ => null,
                };
            }

            return httpResponseMessage;
        }
    }
}
