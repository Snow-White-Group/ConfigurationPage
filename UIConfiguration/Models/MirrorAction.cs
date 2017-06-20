using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIConfiguration.Models
{
    public class MirrorAction
    {
        public SnowwhiteUser User { get; set; }
        public Mirror TargetMirror { get; set; }
        public Action TargetAction { get; set; }

    }

    public enum Action
    {
        Record,
        Handshake
    }
}