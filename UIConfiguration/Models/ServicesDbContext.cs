using System.Data.Entity;

namespace UIConfiguration.Models
{
    public class ServicesDbContext : DbContext
    {
        public ServicesDbContext() 
            : base("name=ServicesContext")
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<NewsSource> NewsSources { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}