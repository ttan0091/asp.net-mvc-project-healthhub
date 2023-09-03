namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteservicetypeentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointment", "ServiceId", "dbo.ServiceType");
            DropForeignKey("dbo.MedicalImage", "ServiceId", "dbo.ServiceType");
            DropIndex("dbo.Appointment", new[] { "ServiceId" });
            DropIndex("dbo.MedicalImage", new[] { "ServiceId" });
            AddColumn("dbo.Appointment", "ServiceType", c => c.String(nullable: false));
            AddColumn("dbo.Appointment", "Note", c => c.String());
            AddColumn("dbo.Appointment", "Gender", c => c.String());
            AddColumn("dbo.MedicalImage", "DoctorId", c => c.Int(nullable: false));
            CreateIndex("dbo.MedicalImage", "DoctorId");
            AddForeignKey("dbo.MedicalImage", "DoctorId", "dbo.Doctor", "DoctorID", cascadeDelete: true);
            DropColumn("dbo.Appointment", "ServiceId");
            DropTable("dbo.ServiceType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceType",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            AddColumn("dbo.Appointment", "ServiceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.MedicalImage", "DoctorId", "dbo.Doctor");
            DropIndex("dbo.MedicalImage", new[] { "DoctorId" });
            DropColumn("dbo.MedicalImage", "DoctorId");
            DropColumn("dbo.Appointment", "Gender");
            DropColumn("dbo.Appointment", "Note");
            DropColumn("dbo.Appointment", "ServiceType");
            CreateIndex("dbo.MedicalImage", "ServiceId");
            CreateIndex("dbo.Appointment", "ServiceId");
            AddForeignKey("dbo.MedicalImage", "ServiceId", "dbo.ServiceType", "ServiceId", cascadeDelete: true);
            AddForeignKey("dbo.Appointment", "ServiceId", "dbo.ServiceType", "ServiceId", cascadeDelete: true);
        }
    }
}
