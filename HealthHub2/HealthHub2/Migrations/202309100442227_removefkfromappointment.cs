namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removefkfromappointment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "DoctorId", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "DoctorId" });
            AlterColumn("dbo.Appointments", "DoctorId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "DoctorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Appointments", "DoctorId");
            AddForeignKey("dbo.Appointments", "DoctorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
