namespace App.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatatable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ItemMasters", "IMIsHazardousMaterial", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemMasters", "IMIsHazardousMaterial", c => c.String());
        }
    }
}
