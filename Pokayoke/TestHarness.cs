using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Lighteous.Pokayoke {
    public class TestHarness {

        public TestHarness(List<IPokayoke> pokayokes) { _pokayokes = pokayokes; }

        public bool Pass { get; set; }
        public bool Fail { get; set; }

        private List<IPokayoke> _pokayokes { get; set; } = new List<IPokayoke>();

        public List<IEvent> Events { get; set; } = new List<IEvent>();

        public void Test() {
            foreach(var pokayoke in _pokayokes) {
                pokayoke.Test();

                Events.Add(new Event(string.Format("{0} : {1}", pokayoke.Title, pokayoke.Pass ? "Pass" : "Fail")));

                if(pokayoke.Errors != null && pokayoke.Errors.Any()) {
                    pokayoke.Errors.ToList().ForEach(forea => Events.Add(new ErrorEvent(string.Format("{0} Error: {1}", pokayoke.Title, forea))));
                }

            }

            Pass = _pokayokes.All(are => are.Pass);
            Fail = !Pass;
        }
    }
}
