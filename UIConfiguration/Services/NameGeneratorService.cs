using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Microsoft.VisualBasic.FileIO;
using UIConfiguration.Models;
using FieldType = Microsoft.Ajax.Utilities.FieldType;

namespace UIConfiguration.Services
{   
    public class NameGeneratorService
    {
        private const string NounFilePath = @"~/Content/RandomNames/nouns.csv";
        private const string AdjectiveFilePath = @"~/Content/RandomNames/adjectives.csv";

        private readonly Random _random;
        private readonly string[] _nouns;
        private readonly string[] _adjectives;

        public NameGeneratorService()
        {
            this._random = new Random();
            this._nouns = LoadFileToArray(NounFilePath);
            this._adjectives = LoadFileToArray(AdjectiveFilePath);
        }

        private static string[] LoadFileToArray(string path) => File.ReadAllLines(HostingEnvironment.MapPath(path));

        // generate name of the form "adjective-noun-4digitnumber"
        public string GenerateDisplayName()
        {
            var ret = _adjectives[_random.Next(0, _adjectives.Length-1)];
            ret += _nouns[_random.Next(0, _nouns.Length - 1)];
            ret += "#" + _random.Next(0, 9999).ToString("D" + 4);
            return ret;
        }

        public string GenerateSecretName()
        {
            return Guid.NewGuid().ToString();
        }

        public MirrorNames GenerateMirrorNames()
        {
            return new MirrorNames(
                GenerateDisplayName(),
                GenerateSecretName());
        }
    }
}