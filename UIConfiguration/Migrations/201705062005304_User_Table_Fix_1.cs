namespace UIConfiguration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_Table_Fix_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
        }
    }
}
