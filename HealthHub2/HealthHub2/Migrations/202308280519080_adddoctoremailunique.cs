namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddoctoremailunique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Doctor", "Email", unique: true);
        }

        public override void Down()
        {
            DropIndex("dbo.Doctor", new[] { "Email" });
        }
    }
}
