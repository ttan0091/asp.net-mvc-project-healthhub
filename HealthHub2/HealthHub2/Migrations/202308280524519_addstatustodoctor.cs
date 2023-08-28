namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatustodoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctor", "Status", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctor", "Status");
        }
    }
}
