namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addgeolocationtoappointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointment", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointment", "LocationId");
            AddForeignKey("dbo.Appointment", "LocationId", "dbo.GeoLocation", "LocationId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointment", "LocationId", "dbo.GeoLocation");
            DropIndex("dbo.Appointment", new[] { "LocationId" });
            DropColumn("dbo.Appointment", "LocationId");
        }
    }
}
