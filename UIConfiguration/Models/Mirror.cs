using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UIConfiguration.Models
{
    public class Mirror
    {
        [Key]
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string SecretName { get; set; }

        [ForeignKey("Id")]
        public virtual List<SnowwhiteUser> Users { get; set; }
    }
}