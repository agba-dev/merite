using Merite.SaaS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Merite.SaaS;

public abstract class SaaSController : AbpControllerBase
{
    protected SaaSController()
    {
        LocalizationResource = typeof(SaaSResource);
    }
}
