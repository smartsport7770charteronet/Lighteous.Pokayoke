using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using com.Lighteous.Pokayoke.Utils;

namespace com.Lighteous.Pokayoke {
    public class DirectoryPokayoke : PokayokeBase {

        public DirectoryPokayoke(string name, string directoryPath) : base() { _directoryPath = directoryPath; Title = $"{name} Exists"; }

        private string _directoryPath { get; set; }

        public override void Test() {
            
            try {
                Pass = Directory.Exists(_directoryPath);
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
