using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIConfiguration.Models
{
    public class MirrorAction
    {
        public UserForAction User { get; set; }
        public MirrorForAction TargetMirror { get; set; }
        public ActionForMirror TargetAction { get; set; }

    }

    public class UserForAction
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class MirrorForAction
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string SecretName { get; set; }
    }

    public enum ActionForMirror
    {
        Record,
        Handshake
    }
}