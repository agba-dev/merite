using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Merite;

[Dependency(ReplaceServices = true)]
public class MeriteBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Merite";
}
