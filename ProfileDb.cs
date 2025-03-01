using Microsoft.EntityFrameworkCore;

class ProfileDb : DbContext
{
    public ProfileDb(DbContextOptions<ProfileDb> options)
        : base(options) { }

    public DbSet<Profile> Profiles => Set<Profile>();
}