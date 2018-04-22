namespace Players.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        MatchID = c.Int(nullable: false, identity: true),
                        City = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(nullable: false),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.MatchID);
            
            CreateTable(
                "dbo.Statistic",
                c => new
                    {
                        StatisticID = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        MatchID = c.Int(nullable: false),
                        goals = c.Int(nullable: false),
                        assists = c.Int(nullable: false),
                        minutes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatisticID)
                .ForeignKey("dbo.Match", t => t.MatchID, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID)
                .Index(t => t.MatchID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Statistic", "PlayerID", "dbo.Player");
            DropForeignKey("dbo.Statistic", "MatchID", "dbo.Match");
            DropIndex("dbo.Statistic", new[] { "MatchID" });
            DropIndex("dbo.Statistic", new[] { "PlayerID" });
            DropTable("dbo.Player");
            DropTable("dbo.Statistic");
            DropTable("dbo.Match");
        }
    }
}
