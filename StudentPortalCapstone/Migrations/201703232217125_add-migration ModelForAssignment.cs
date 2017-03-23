namespace StudentPortalCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationModelForAssignment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AssignmentName = c.String(),
                        RosterId = c.Int(nullable: false),
                        MaxPts = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rosters", t => t.RosterId, cascadeDelete: true)
                .Index(t => t.RosterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assignments", "RosterId", "dbo.Rosters");
            DropIndex("dbo.Assignments", new[] { "RosterId" });
            DropTable("dbo.Assignments");
        }
    }
}
