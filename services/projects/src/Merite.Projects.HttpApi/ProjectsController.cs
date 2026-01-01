using Merite.Projects.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Merite.Projects;

public abstract class ProjectsController : AbpControllerBase
{
    protected ProjectsController()
    {
        LocalizationResource = typeof(ProjectsResource);
    }
}
