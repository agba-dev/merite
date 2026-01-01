using Microsoft.Extensions.Hosting;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Merite.IdentityService.EntityFrameworkCore;

[ConnectionStringName(MeriteNames.IdentityServiceDb)]
public interface IIdentityServiceDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
