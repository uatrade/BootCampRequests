namespace OrderManagemenet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Id : DbMigration
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
                    })
                .PrimaryKey(t => t.requestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Requests");
        }
    }
}
