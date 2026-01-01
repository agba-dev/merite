using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Merite.Projects;

[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(ProjectsDomainSharedModule))]
public class ProjectsDomainModule : AbpModule { }
