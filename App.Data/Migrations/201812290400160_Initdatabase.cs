namespace App.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initdatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemMasterInventorys", "IMISiteID", "dbo.ItemMasters");
            AddColumn("dbo.ItemMasterInventorys", "ItemMasterID", c => c.Int(nullable: false));
            CreateIndex("dbo.ItemMasterInventorys", "ItemMasterID");
            AddForeignKey("dbo.ItemMasterInventorys", "IMISiteID", "dbo.Sites", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ItemMasterInventorys", "ItemMasterID", "dbo.ItemMasters", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemMasterInventorys", "ItemMasterID", "dbo.ItemMasters");
            DropForeignKey("dbo.ItemMasterInventorys", "IMISiteID", "dbo.Sites");
            DropIndex("dbo.ItemMasterInventorys", new[] { "ItemMasterID" });
            DropColumn("dbo.ItemMasterInventorys", "ItemMasterID");
            AddForeignKey("dbo.ItemMasterInventorys", "IMISiteID", "dbo.ItemMasters", "Id", cascadeDelete: true);
        }
    }
}
