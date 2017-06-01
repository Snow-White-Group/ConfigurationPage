using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace UIConfiguration.Models
{
    public class Main
    {
        [Key]
        public int Id { get; set; }
        public double Temp { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public double Pressure { get; set; }
        public double Sea_level { get; set; }
        public double Grnd_level { get; set; }
        public int Humidity { get; set; }
        public double Temp_kf { get; set; }
    }

    public class Weather
    {
        [Key]
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Clouds
    {
        [Key]
        public int Id { get; set; }
        public int All { get; set; }
    }

    public class Wind
    {
        [Key]
        public int Id { get; set; }
        public double Speed { get; set; }
        public double Deg { get; set; }
    }

    public class Sys
    {
        [Key]
        public int Id { get; set; }
        public string Pod { get; set; }
    }

    public class Rain
    {
        [Key]
        public int Id { get; set; }
        public double ThreeHours { get; set; }
    }

    public class List
    {
        [Key]
        public int Id { get; set; }
        public int DT { get; set; }
        public virtual Main Main { get; set; }
        public virtual List<Weather> Weather { get; set; }
        public virtual Clouds Clouds { get; set; }
        public virtual Wind Wind { get; set; }
        public virtual Sys Sys { get; set; }
        public string DT_txt { get; set; }
        public virtual Rain Rain { get; set; }
    }

    public class Coord
    {
        [Key]
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Coord Coord { get; set; }
        public string Country { get; set; }
    }

    public class WeatherForecast
    {
        [Key]
        public int Id { get; set; }
        public string Cod { get; set; }
        public double Message { get; set; }
        public int Cnt { get; set; }
        public virtual List<List> List { get; set; }
        public virtual City City { get; set; }
        public int LastUpdate { get; set; }
    }
}
