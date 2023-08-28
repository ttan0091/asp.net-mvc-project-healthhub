namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmailStringLength1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctor", "Email", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctor", "Email", c => c.String(nullable: false));
        }
    }
}
