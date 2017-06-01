namespace UIConfiguration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsAndWeather : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Source = c.String(),
                        SortBy = c.String(),
                        LastUpdate = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        URL = c.String(),
                        URLToImage = c.String(),
                        PublishedAt = c.String(),
                        News_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.News_Id)
                .Index(t => t.News_Id);
            
            CreateTable(
                "dbo.NewsSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        LastUpdate = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        URL = c.String(),
                        Category = c.String(),
                        Language = c.String(),
                        Country = c.String(),
                        URLToLogos_Id = c.Int(),
                        NewsSource_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Logos", t => t.URLToLogos_Id)
                .ForeignKey("dbo.NewsSources", t => t.NewsSource_Id)
                .Index(t => t.URLToLogos_Id)
                .Index(t => t.NewsSource_Id);
            
            CreateTable(
                "dbo.Logos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Small = c.String(),
                        Medium = c.String(),
                        Large = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeatherForecasts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cod = c.String(),
                        Message = c.Double(nullable: false),
                        Cnt = c.Int(nullable: false),
                        LastUpdate = c.Int(nullable: false),
                        City_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        Coord_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coords", t => t.Coord_Id)
                .Index(t => t.Coord_Id);
            
            CreateTable(
                "dbo.Coords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lat = c.Double(nullable: false),
                        Lon = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DT = c.Int(nullable: false),
                        DT_txt = c.String(),
                        Clouds_Id = c.Int(),
                        Main_Id = c.Int(),
                        Rain_Id = c.Int(),
                        Sys_Id = c.Int(),
                        Wind_Id = c.Int(),
                        WeatherForecast_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clouds", t => t.Clouds_Id)
                .ForeignKey("dbo.Mains", t => t.Main_Id)
                .ForeignKey("dbo.Rains", t => t.Rain_Id)
                .ForeignKey("dbo.Sys", t => t.Sys_Id)
                .ForeignKey("dbo.Winds", t => t.Wind_Id)
                .ForeignKey("dbo.WeatherForecasts", t => t.WeatherForecast_Id)
                .Index(t => t.Clouds_Id)
                .Index(t => t.Main_Id)
                .Index(t => t.Rain_Id)
                .Index(t => t.Sys_Id)
                .Index(t => t.Wind_Id)
                .Index(t => t.WeatherForecast_Id);
            
            CreateTable(
                "dbo.Clouds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        All = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Temp = c.Double(nullable: false),
                        Temp_min = c.Double(nullable: false),
                        Temp_max = c.Double(nullable: false),
                        Pressure = c.Double(nullable: false),
                        Sea_level = c.Double(nullable: false),
                        Grnd_level = c.Double(nullable: false),
                        Humidity = c.Int(nullable: false),
                        Temp_kf = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThreeHours = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pod = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Main = c.String(),
                        Description = c.String(),
                        Icon = c.String(),
                        List_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lists", t => t.List_Id)
                .Index(t => t.List_Id);
            
            CreateTable(
                "dbo.Winds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Speed = c.Double(nullable: false),
                        Deg = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lists", "WeatherForecast_Id", "dbo.WeatherForecasts");
            DropForeignKey("dbo.Lists", "Wind_Id", "dbo.Winds");
            DropForeignKey("dbo.Weathers", "List_Id", "dbo.Lists");
            DropForeignKey("dbo.Lists", "Sys_Id", "dbo.Sys");
            DropForeignKey("dbo.Lists", "Rain_Id", "dbo.Rains");
            DropForeignKey("dbo.Lists", "Main_Id", "dbo.Mains");
            DropForeignKey("dbo.Lists", "Clouds_Id", "dbo.Clouds");
            DropForeignKey("dbo.WeatherForecasts", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Coord_Id", "dbo.Coords");
            DropForeignKey("dbo.Sources", "NewsSource_Id", "dbo.NewsSources");
            DropForeignKey("dbo.Sources", "URLToLogos_Id", "dbo.Logos");
            DropForeignKey("dbo.Articles", "News_Id", "dbo.News");
            DropIndex("dbo.Weathers", new[] { "List_Id" });
            DropIndex("dbo.Lists", new[] { "WeatherForecast_Id" });
            DropIndex("dbo.Lists", new[] { "Wind_Id" });
            DropIndex("dbo.Lists", new[] { "Sys_Id" });
            DropIndex("dbo.Lists", new[] { "Rain_Id" });
            DropIndex("dbo.Lists", new[] { "Main_Id" });
            DropIndex("dbo.Lists", new[] { "Clouds_Id" });
            DropIndex("dbo.Cities", new[] { "Coord_Id" });
            DropIndex("dbo.WeatherForecasts", new[] { "City_Id" });
            DropIndex("dbo.Sources", new[] { "NewsSource_Id" });
            DropIndex("dbo.Sources", new[] { "URLToLogos_Id" });
            DropIndex("dbo.Articles", new[] { "News_Id" });
            DropTable("dbo.Winds");
            DropTable("dbo.Weathers");
            DropTable("dbo.Sys");
            DropTable("dbo.Rains");
            DropTable("dbo.Mains");
            DropTable("dbo.Clouds");
            DropTable("dbo.Lists");
            DropTable("dbo.Coords");
            DropTable("dbo.Cities");
            DropTable("dbo.WeatherForecasts");
            DropTable("dbo.Logos");
            DropTable("dbo.Sources");
            DropTable("dbo.NewsSources");
            DropTable("dbo.Articles");
            DropTable("dbo.News");
        }
    }
}
