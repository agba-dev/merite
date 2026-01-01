using Merite.WebApp.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Merite.WebApp.Blazor.Client;

public abstract class WebAppComponentBase : AbpComponentBase
{
    protected WebAppComponentBase()
    {
        LocalizationResource = typeof(WebAppResource);
    }
}
