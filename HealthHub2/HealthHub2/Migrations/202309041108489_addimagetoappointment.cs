namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimagetoappointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointment", "ImageUrl", c => c.String());
            AddColumn("dbo.Appointment", "UploadDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointment", "UploadDate");
            DropColumn("dbo.Appointment", "ImageUrl");
        }
    }
}
