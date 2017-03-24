namespace StudentPortalCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelForReportCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentPts = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                        AssignmentsId = c.Int(nullable: false),
                        HasBeenGraded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assignments", t => t.AssignmentsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AssignmentsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportCards", "UserId", "dbo.Users");
            DropForeignKey("dbo.ReportCards", "AssignmentsId", "dbo.Assignments");
            DropIndex("dbo.ReportCards", new[] { "AssignmentsId" });
            DropIndex("dbo.ReportCards", new[] { "UserId" });
            DropTable("dbo.ReportCards");
        }
    }
}
