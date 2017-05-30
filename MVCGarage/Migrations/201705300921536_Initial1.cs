namespace MVCGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "CheckInTime", c => c.DateTime());
            AlterColumn("dbo.Vehicles", "CheckOutTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "CheckOutTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vehicles", "CheckInTime", c => c.DateTime(nullable: false));
        }
    }
}
