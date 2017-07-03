using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UIConfiguration.Models
{
    public struct MirrorNames
    {
        public readonly string DisplayName;
        public readonly string SecretName;

        public MirrorNames(string displayName, string secretName)
        {
            DisplayName = displayName;
            SecretName = secretName;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is MirrorNames)) return false;
            
            var working = obj as MirrorNames;
            return working.DisplayName.Equals(this.DisplayName) && working.SecretName.Equals(this.SecretName);
           
        }
    }
}