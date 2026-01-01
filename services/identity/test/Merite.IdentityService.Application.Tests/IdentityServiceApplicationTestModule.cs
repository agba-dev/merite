using Volo.Abp.Modularity;

namespace Merite.IdentityService;

[DependsOn(typeof(IdentityServiceApplicationModule))]
[DependsOn(typeof(IdentityServiceDomainTestModule))]
public class IdentityServiceApplicationTestModule : AbpModule { }
