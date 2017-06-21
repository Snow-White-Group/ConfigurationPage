using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIConfiguration.Models
{
    public class MirrorAction
    {
        [Key]
        public string Id { get;  set; }
        public UserForAction User { get; set; }
        public MirrorForAction TargetMirror { get; set; }
        public ActionForMirror TargetAction { get; set; }
        public DateTime RequestedAt { get; set; }
        public bool IsDone { get; set; }
    }

    public class UserForAction
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class MirrorForAction
    {
        [Key]
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