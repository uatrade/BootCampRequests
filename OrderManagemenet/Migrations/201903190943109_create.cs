namespace OrderManagemenet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        requestId = c.Long(nullable: false, identity: true),
                        clientId = c.Int(nullable: false),
                        name = c.String(),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        RequestList_id = c.Int(),
                    })
                .PrimaryKey(t => t.requestId)
                .ForeignKey("dbo.RequestLists", t => t.RequestList_id)
                .Index(t => t.RequestList_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "RequestList_id", "dbo.RequestLists");
            DropIndex("dbo.Requests", new[] { "RequestList_id" });
            DropTable("dbo.Requests");
        }
    }
}
