namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removetablemedicalimage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalImage", "AppointmentId", "dbo.Appointment");
            DropIndex("dbo.MedicalImage", new[] { "AppointmentId" });
            DropTable("dbo.MedicalImage");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MedicalImage",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        AppointmentId = c.Int(nullable: false),
                        CaptureDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateIndex("dbo.MedicalImage", "AppointmentId");
            AddForeignKey("dbo.MedicalImage", "AppointmentId", "dbo.Appointment", "AppointmentId", cascadeDelete: true);
        }
    }
}
