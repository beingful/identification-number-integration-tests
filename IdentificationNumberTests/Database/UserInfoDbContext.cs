using Microsoft.EntityFrameworkCore;
using PINTests.Database.Constants;
using PINTests.Database.Models;

namespace PINTests.Database;

internal sealed class UserInfoDbContext : DbContext
{
    public DbSet<UserInfoContext> UsersInfo { get; init; }

    public UserInfoDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserInfoContext>()
            .ToContainer(nameof(UserInfoContext))
            .HasNoDiscriminator();

        modelBuilder.Entity<UserInfoContext>()
            .Property(userInfo => userInfo.Id)
            .ToJsonProperty(UserInfoContextOriginalColumnNames.Id);

        modelBuilder.Entity<UserInfoContext>()
            .Property(userInfo => userInfo.BirthDate)
            .ToJsonProperty(UserInfoContextOriginalColumnNames.BirthDate);

        modelBuilder.Entity<UserInfoContext>()
            .Property(userInfo => userInfo.AccountNumber)
            .ToJsonProperty(UserInfoContextOriginalColumnNames.AccountNumber);
    }
}
