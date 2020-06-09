using Demo.Users.Application.Common.Interfaces;
using Demo.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Users.Infrastructure.Persistence
{
    public class UsersDbContext : DbContext, IUsersDbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {

        }
    }
}
