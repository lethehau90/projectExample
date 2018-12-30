namespace App.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using App.Model.Entity;

    internal sealed class Configuration : DbMigrationsConfiguration<App.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(App.Data.AppDbContext context)
        {
            CreateSize(context);
        }

        private void CreateSize(AppDbContext context)
        {
            if (context.Sites.Count() == 0)
            {
                List<Site> listSite = new List<Site>()
                {
                    new Site() { Name="CET" },
                    new Site() { Name="SEA"},
                };
                context.Sites.AddRange(listSite);
                context.SaveChanges();
            }
        }
    }
}
