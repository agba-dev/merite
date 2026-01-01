using Merite.IdentityService.Localization;
using Volo.Abp.Application.Services;

namespace Merite.IdentityService;

public abstract class IdentityServiceAppService : ApplicationService
{
    protected IdentityServiceAppService()
    {
        LocalizationResource = typeof(IdentityServiceResource);
        ObjectMapperContext = typeof(IdentityServiceApplicationModule);
    }
}
