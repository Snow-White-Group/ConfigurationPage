using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIConfiguration.Models
{
    public class TargetMirrorUserViewModel
    {
        public SnowwhiteUser User { get; set; }
        public Mirror Mirror { get; set; }
    }
    public class MirrorsUserViewModel
    {
        public SnowwhiteUser User { get; set; }
        public List<Mirror> Mirrors { get; set; }
    }
}