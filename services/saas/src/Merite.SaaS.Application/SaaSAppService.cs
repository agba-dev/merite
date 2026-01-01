using Merite.SaaS.Localization;
using Volo.Abp.Application.Services;

namespace Merite.SaaS;

public abstract class SaaSAppService : ApplicationService
{
    protected SaaSAppService()
    {
        LocalizationResource = typeof(SaaSResource);
        ObjectMapperContext = typeof(SaaSApplicationModule);
    }
}
