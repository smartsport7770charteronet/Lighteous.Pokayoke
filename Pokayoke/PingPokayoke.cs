using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

using com.Lighteous.Pokayoke.Utils;

namespace com.Lighteous.Pokayoke {
    public class PingPokayoke : PokayokeBase {
        public PingPokayoke(string hostNameOrAddress) : base() { _hostNameOrAddress = hostNameOrAddress; Title = string.Format("Ping {0}", hostNameOrAddress); }

        private string _hostNameOrAddress { get; set; }

        public override void Test() {
            try { 
                using(var ping = new Ping()) {
                    var pReply = ping.Send(_hostNameOrAddress);
                    Pass = pReply.Status == IPStatus.Success;
                    Fail = !Pass;
                }
            }
            catch(Exception err) {
                Pass = false;
                Fail = !Pass;

                Errors = ErrorMessage.Coalesce(err);
            }
        }

    }
}
