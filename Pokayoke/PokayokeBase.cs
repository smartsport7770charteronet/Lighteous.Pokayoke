using System;
using System.Collections.Generic;
using System.Text;

namespace com.Lighteous.Pokayoke {
    public class PokayokeBase : IPokayoke {

        public PokayokeBase() { }

        public string Title { get; set; }
        public bool Pass { get; set; }
        public bool Fail { get; set; }


        public virtual void Test() {
            // Do nothing
        }


        public string[] Errors { get; set; }
    }
}
