using Merite.Administration;
using Merite.Administration.EntityFrameworkCore;
using Merite.IdentityService;
using Merite.IdentityService.EntityFrameworkCore;
using Merite.Projects;
using Merite.Projects.EntityFrameworkCore;
using Merite.SaaS;
using Merite.SaaS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.Tokens;

namespace Merite.DbMigrator;

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpBackgroundJobsAbstractionsModule))]
[DependsOn(typeof(AdministrationEntityFrameworkCoreModule))]
[DependsOn(typeof(AdministrationApplicationContractsModule))]
[DependsOn(typeof(IdentityServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(IdentityServiceApplicationContractsModule))]
[DependsOn(typeof(ProjectsEntityFrameworkCoreModule))]
[DependsOn(typeof(ProjectsApplicationContractsModule))]
[DependsOn(typeof(SaaSEntityFrameworkCoreModule))]
[DependsOn(typeof(SaaSApplicationContractsModule))]
//[DependsOn(typeof(WebAppEntityFrameworkCoreModule))]
//[DependsOn(typeof(WebAppApplicationContractsModule))]
public class MeriteDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        Configure<TokenCleanupOptions>(options => options.IsCleanupEnabled = false);
    }
}
