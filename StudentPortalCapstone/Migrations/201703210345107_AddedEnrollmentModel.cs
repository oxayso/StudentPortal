namespace StudentPortalCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEnrollmentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RosterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rosters", t => t.RosterId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RosterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Enrollments", "RosterId", "dbo.Rosters");
            DropIndex("dbo.Enrollments", new[] { "RosterId" });
            DropIndex("dbo.Enrollments", new[] { "UserId" });
            DropTable("dbo.Enrollments");
        }
    }
}
