using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

using com.Lighteous.Pokayoke.Utils;

namespace com.Lighteous.Pokayoke {
    public class InternetConnectionPokayoke : PokayokeBase {

        public InternetConnectionPokayoke() : base() { Title = "Internet Connection Available"; }


        public override void Test() {
            try { 
                Pass = NetworkInterface.GetIsNetworkAvailable();
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
