namespace UIConfiguration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Actions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TargetAction = c.Int(nullable: false),
                        RequestedAt = c.DateTime(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        TargetMirror_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MirrorForActions", t => t.TargetMirror_Id)
                .ForeignKey("dbo.UserForActions", t => t.User_Id)
                .Index(t => t.TargetMirror_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.MirrorForActions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(),
                        SecretName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserForActions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actions", "User_Id", "dbo.UserForActions");
            DropForeignKey("dbo.Actions", "TargetMirror_Id", "dbo.MirrorForActions");
            DropIndex("dbo.Actions", new[] { "User_Id" });
            DropIndex("dbo.Actions", new[] { "TargetMirror_Id" });
            DropTable("dbo.UserForActions");
            DropTable("dbo.MirrorForActions");
            DropTable("dbo.Actions");
        }
    }
}
