namespace StudentPortalCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAttendanceModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        RosterId = c.Int(nullable: false),
                        isPresent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rosters", t => t.RosterId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RosterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "UserId", "dbo.Users");
            DropForeignKey("dbo.Attendances", "RosterId", "dbo.Rosters");
            DropIndex("dbo.Attendances", new[] { "RosterId" });
            DropIndex("dbo.Attendances", new[] { "UserId" });
            DropTable("dbo.Attendances");
        }
    }
}
