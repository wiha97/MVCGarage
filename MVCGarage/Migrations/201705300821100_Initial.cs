namespace MVCGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParkingSpots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VehicleID = c.Int(nullable: false),
                        Label = c.String(),
                        VehicleType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VehicleType = c.Int(nullable: false),
                        Owner = c.String(),
                        Fee = c.Double(nullable: false),
                        RegistrationPlate = c.String(),
                        CheckInTime = c.DateTime(nullable: false),
                        CheckOutTime = c.DateTime(nullable: false),
                        ParkingSpot = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vehicles");
            DropTable("dbo.ParkingSpots");
        }
    }
}