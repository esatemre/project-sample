namespace TheProject.FileStore
{
    using Microsoft.EntityFrameworkCore;
    using Core;

    public class AppFileContext : AppBaseContext
    {
        public AppFileContext(DbContextOptions<AppFileContext> options)
            : base(options)
        {

        }
    }
}
