namespace HealthHub2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequiretodoctorstatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctor", "Status", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctor", "Status", c => c.String(maxLength: 50));
        }
    }
}
