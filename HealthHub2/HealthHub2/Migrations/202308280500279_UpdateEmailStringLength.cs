namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmailStringLength : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Patient", new[] { "Email" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Patient", "Email", unique: true);
        }
    }
}
