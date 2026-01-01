using Microsoft.Extensions.Hosting;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Merite.Projects.EntityFrameworkCore;

[ConnectionStringName(MeriteNames.ProjectsDb)]
public interface IProjectsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
