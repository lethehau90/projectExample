namespace App.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private AppDbContext dbContext;

        public AppDbContext Init()
        {
            return dbContext ?? (dbContext = new AppDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}