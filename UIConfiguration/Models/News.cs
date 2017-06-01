using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UIConfiguration.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public string Source { get; set; }
        public string SortBy { get; set; }
        public virtual List<Article> Articles { get; set; }
        public long LastUpdate { get; set; }
    }

    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string URLToImage { get; set; }
        public string PublishedAt { get; set; }
    }
}