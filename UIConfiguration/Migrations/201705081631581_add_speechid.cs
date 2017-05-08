namespace UIConfiguration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_speechid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SpeechID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SpeechID");
        }
    }
}
