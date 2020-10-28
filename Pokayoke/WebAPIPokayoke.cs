using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using com.Lighteous.Pokayoke.Utils;

namespace com.Lighteous.Pokayoke {
    public class WebAPIPokayoke : PokayokeBase {
        /// <summary>
        /// getRoute HAS TO BE a GET method route. getRoute is the route fragment, and NOT the full URL: baseAddress will be concatented.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="baseAddress"></param>
        /// <param name="getRoute"></param>
        public WebAPIPokayoke(string name, string baseAddress, string getRoute) : base() { _baseAddress = baseAddress; _route = getRoute; Title = string.Format("{0} Available", name); }

        private string _baseAddress { get; set; }
        private string _route { get; set; }

        public override void Test() {
            
            try {
                using(var wc = new WebClient()) {
                    var forwardSlashChar = "/";
                    wc.BaseAddress = _baseAddress;
                    _baseAddress = _baseAddress.TrimEnd(forwardSlashChar.ToCharArray());
                    if(!_route.StartsWith(forwardSlashChar)) {
                        _route = $"{forwardSlashChar}{_route}";
                    }
                    var url = $"{_baseAddress}{_route}";
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
