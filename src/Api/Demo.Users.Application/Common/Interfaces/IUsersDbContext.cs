using System.Threading;
using System.Threading.Tasks;
using Demo.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Users.Application.Common.Interfaces
{
    public interface IUsersDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
