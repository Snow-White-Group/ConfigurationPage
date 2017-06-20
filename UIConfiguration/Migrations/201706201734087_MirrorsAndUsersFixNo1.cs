namespace UIConfiguration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MirrorsAndUsersFixNo1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Id", "dbo.Mirrors");
            DropIndex("dbo.AspNetUsers", new[] { "Id" });
            CreateTable(
                "dbo.SnowwhiteUserMirrors",
                c => new
                    {
                        SnowwhiteUser_Id = c.String(nullable: false, maxLength: 128),
                        Mirror_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SnowwhiteUser_Id, t.Mirror_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.SnowwhiteUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Mirrors", t => t.Mirror_Id, cascadeDelete: true)
                .Index(t => t.SnowwhiteUser_Id)
                .Index(t => t.Mirror_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SnowwhiteUserMirrors", "Mirror_Id", "dbo.Mirrors");
            DropForeignKey("dbo.SnowwhiteUserMirrors", "SnowwhiteUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SnowwhiteUserMirrors", new[] { "Mirror_Id" });
            DropIndex("dbo.SnowwhiteUserMirrors", new[] { "SnowwhiteUser_Id" });
            DropTable("dbo.SnowwhiteUserMirrors");
            CreateIndex("dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Id", "dbo.Mirrors", "Id");
        }
    }
}
