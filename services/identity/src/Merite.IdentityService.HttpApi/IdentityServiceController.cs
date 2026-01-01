using Merite.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Merite.IdentityService;

public abstract class IdentityServiceController : AbpControllerBase
{
    protected IdentityServiceController()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}
