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
    }
}