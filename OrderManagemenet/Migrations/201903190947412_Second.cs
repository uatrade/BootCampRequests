namespace OrderManagemenet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "RequestList_id", "dbo.RequestLists");
            DropIndex("dbo.Requests", new[] { "RequestList_id" });
            DropColumn("dbo.Requests", "RequestList_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "RequestList_id", c => c.Int());
            CreateIndex("dbo.Requests", "RequestList_id");
            AddForeignKey("dbo.Requests", "RequestList_id", "dbo.RequestLists", "id");
        }
    }
}
