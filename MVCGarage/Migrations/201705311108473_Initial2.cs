namespace MVCGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParkingSpots", "Fee", c => c.Double(nullable: false));
            AddColumn("dbo.Vehicles", "ParkingSpotID", c => c.Int());
            DropColumn("dbo.Vehicles", "Fee");
            DropColumn("dbo.Vehicles", "CheckOutTime");
            DropColumn("dbo.Vehicles", "ParkingSpot");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "ParkingSpot", c => c.Int());
            AddColumn("dbo.Vehicles", "CheckOutTime", c => c.DateTime());
            AddColumn("dbo.Vehicles", "Fee", c => c.Double(nullable: false));
            DropColumn("dbo.Vehicles", "ParkingSpotID");
            DropColumn("dbo.ParkingSpots", "Fee");
        }
    }
}
