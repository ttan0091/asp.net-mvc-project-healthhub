namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removepatientidfrommedicalimages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalImage", "DoctorId", "dbo.Doctor");
            DropForeignKey("dbo.MedicalImage", "PatientId", "dbo.Patient");
            DropIndex("dbo.MedicalImage", new[] { "PatientId" });
            DropIndex("dbo.MedicalImage", new[] { "DoctorId" });
            AddColumn("dbo.MedicalImage", "AppointmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.MedicalImage", "AppointmentId");
            AddForeignKey("dbo.MedicalImage", "AppointmentId", "dbo.Appointment", "AppointmentId", cascadeDelete: true);
            DropColumn("dbo.MedicalImage", "PatientId");
            DropColumn("dbo.MedicalImage", "DoctorId");
            DropColumn("dbo.MedicalImage", "ServiceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalImage", "ServiceId", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalImage", "DoctorId", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalImage", "PatientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.MedicalImage", "AppointmentId", "dbo.Appointment");
            DropIndex("dbo.MedicalImage", new[] { "AppointmentId" });
            DropColumn("dbo.MedicalImage", "AppointmentId");
            CreateIndex("dbo.MedicalImage", "DoctorId");
            CreateIndex("dbo.MedicalImage", "PatientId");
            AddForeignKey("dbo.MedicalImage", "PatientId", "dbo.Patient", "PatientId", cascadeDelete: true);
            AddForeignKey("dbo.MedicalImage", "DoctorId", "dbo.Doctor", "DoctorId", cascadeDelete: true);
        }
    }
}
