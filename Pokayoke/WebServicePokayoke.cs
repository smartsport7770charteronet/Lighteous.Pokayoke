using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using com.Lighteous.Pokayoke.Utils;
using System.Net.Http;

namespace com.Lighteous.Pokayoke {
    public class WebServicePokayoke : PokayokeBase {

        public WebServicePokayoke(string name, string address) : base() { _address = address; Title = string.Format("{0} Available", name); }

        private string _address { get; set; }

        public override void Test() {
            var url = _address;
            if (!url.ToLower().Contains("?wsdl")) { 
                url = $"{_address}?wsdl";
            }
            try {
                using(var wc = new WebClient()) {
                    wc.BaseAddress = url;
                    wc.DownloadString(url);
                    Pass = true;
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
