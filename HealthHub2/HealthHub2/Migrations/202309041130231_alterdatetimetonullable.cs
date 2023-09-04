namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterdatetimetonullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointment", "UploadDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointment", "UploadDate", c => c.DateTime(nullable: false));
        }
    }
}
