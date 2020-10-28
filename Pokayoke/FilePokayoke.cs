using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using com.Lighteous.Pokayoke.Utils;

namespace com.Lighteous.Pokayoke {
    public class FilePokayoke : PokayokeBase {
        public FilePokayoke(string name, string fileFullNameAndPath) : base() { _fileFullNameAndPath = fileFullNameAndPath; Title = $"{name} File Exists"; }

        private string _fileFullNameAndPath { get; set; }

        public override void Test() {
            
            try {
                Pass = File.Exists(_fileFullNameAndPath);
                Fail = !Pass;
            }
            catch(Exception err) {
                Pass = false;
                Fail = !Pass;
                Errors = ErrorMessage.Coalesce(err);
            }
        }
    }
}
