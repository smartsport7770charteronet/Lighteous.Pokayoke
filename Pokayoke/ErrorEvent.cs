using System;
using System.Collections.Generic;
using System.Text;

namespace com.Lighteous.Pokayoke {
    public class ErrorEvent : EventBase {
        public ErrorEvent(string note) : base() { Note = note; }
    }
}
