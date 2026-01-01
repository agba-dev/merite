using Merite.Administration.Localization;
using Volo.Abp.Application.Services;

namespace Merite.Administration;

public abstract class AdministrationAppService : ApplicationService
{
    protected AdministrationAppService()
    {
        LocalizationResource = typeof(AdministrationResource);
        ObjectMapperContext = typeof(AdministrationApplicationModule);
    }
}
