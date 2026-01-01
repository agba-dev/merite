using Volo.Abp.Modularity;

namespace Merite.Projects;

[DependsOn(typeof(ProjectsApplicationModule))]
[DependsOn(typeof(ProjectsDomainTestModule))]
public class ProjectsApplicationTestModule : AbpModule { }
