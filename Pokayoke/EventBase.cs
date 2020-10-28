using System;
using System.Collections.Generic;
using System.Text;

namespace com.Lighteous.Pokayoke {
    public class EventBase : IEvent {

        public EventBase() { }

        public string Note { get; set; }
    }
}
