namespace TheProject.EFCore
{
    using Microsoft.EntityFrameworkCore;
    using Core;

    public class AppDbContext : AppBaseContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
    }
}
