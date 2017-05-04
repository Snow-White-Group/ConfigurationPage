namespace UIConfiguration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SnowwhiteUsers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Thumbnail = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SnowwhiteUsers");
        }
    }
}
