namespace ZE1Sharp
{
    using System;

    public abstract class ControllerBase
    {
        protected readonly IAPICaller ApiCaller;

        protected ControllerBase(IAPICaller apiCaller = null)
        {
            this.ApiCaller = apiCaller ?? new HttpClientAPICaller();
        }
    }
}