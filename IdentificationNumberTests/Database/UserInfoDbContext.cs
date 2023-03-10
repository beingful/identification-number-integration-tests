using Microsoft.EntityFrameworkCore;
using IntegrationTests.Database.Constants;
using IntegrationTests.Database.Models;

namespace IntegrationTests.Database;

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
            .ToJsonProperty(OriginalColumnNames.Id);

        modelBuilder.Entity<UserInfoContext>()
            .Property(userInfo => userInfo.BirthDate)
            .ToJsonProperty(OriginalColumnNames.BirthDate);

        modelBuilder.Entity<UserInfoContext>()
            .Property(userInfo => userInfo.AccountNumber)
            .ToJsonProperty(OriginalColumnNames.AccountNumber);
    }
}
