using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace UIConfiguration.Models
{
    // sources
    public class NewsSource
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public virtual List<Source> Sources { get; set; }
        public long LastUpdate { get; set; }
    }

    public class Source
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public virtual Logos URLToLogos { get; set; }
        public List<string> SortBysAvailable { get; set; }
    }

    public class Logos
    {
        [Key]
        public int Id { get; set; }
        public string Small { get; set; }
        public string Medium { get; set; }
        public string Large { get; set; }
    }
}
