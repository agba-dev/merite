using Merite.Administration.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Merite.Administration;

public abstract class AdministrationController : AbpControllerBase
{
    protected AdministrationController()
    {
        LocalizationResource = typeof(AdministrationResource);
    }
}
