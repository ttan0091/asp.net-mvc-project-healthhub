namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddefaultvaluetostatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patient", "Status", c => c.String(nullable: false, maxLength: 50, defaultValue: "patient"));
            AlterColumn("dbo.Doctor", "Status", c => c.String(nullable: false, maxLength: 50, defaultValue: "doctor"));
        }

        public override void Down()
        {
            AlterColumn("dbo.Patient", "Status", c => c.String(nullable: false, maxLength: 50, defaultValue: ""));
        }
    }
}
