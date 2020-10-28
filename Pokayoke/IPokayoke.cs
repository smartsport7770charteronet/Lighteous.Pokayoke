using System;
using System.Collections.Generic;
using System.Text;

namespace com.Lighteous.Pokayoke {
    public interface IPokayoke {

        string Title { get; set; }

        bool Pass { get; set; }
        bool Fail { get; set; }

        void Test();


        string[] Errors { get; set; }
    }
}
