namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YourMigrationNameHere : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Patient", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Patient", new[] { "Email" });
        }
    }
}
