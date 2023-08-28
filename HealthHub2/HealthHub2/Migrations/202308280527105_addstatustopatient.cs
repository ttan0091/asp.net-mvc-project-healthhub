namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatustopatient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patient", "Status", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patient", "Status");
        }
    }
}
