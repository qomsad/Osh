using Microsoft.EntityFrameworkCore;
using OshService.Domain.User;

namespace OshService.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<UserModel> User { get; init; }
}
