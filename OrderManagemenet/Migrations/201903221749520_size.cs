namespace OrderManagemenet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class size : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "name", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "name", c => c.String());
        }
    }
}
