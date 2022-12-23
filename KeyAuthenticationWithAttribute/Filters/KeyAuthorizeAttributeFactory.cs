using Microsoft.AspNetCore.Mvc.Filters;

namespace KeyAuthenticationWithAttribute.Filters
{
    public class KeyAuthorizeAttributeFactory : IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
