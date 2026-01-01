using Merite.Projects.Localization;
using Volo.Abp.Application.Services;

namespace Merite.Projects;

public abstract class ProjectsAppService : ApplicationService
{
    protected ProjectsAppService()
    {
        LocalizationResource = typeof(ProjectsResource);
        ObjectMapperContext = typeof(ProjectsApplicationModule);
    }
}
