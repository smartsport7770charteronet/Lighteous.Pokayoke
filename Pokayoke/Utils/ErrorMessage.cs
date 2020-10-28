using System;
using System.Collections.Generic;
using System.Text;

namespace com.Lighteous.Pokayoke.Utils {
    public static class ErrorMessage {
        public static string[] Coalesce(Exception err) {
            var value = string.Empty;
            var delimiter = "|";
            if (err != null && !string.IsNullOrEmpty(err.Message)) {
                value = string.Format("{0}{1}{2}", value, err.Message, delimiter);
                if(err.InnerException != null && !string.IsNullOrEmpty(err.InnerException.Message)) {
                    value = string.Format("{0}{1}{2}", value, err.InnerException.Message, delimiter);
                    if(err.InnerException.InnerException != null && !string.IsNullOrEmpty(err.InnerException.InnerException.Message)) {
                        value = string.Format("{0}{1}{2}", value, err.InnerException.InnerException.Message, delimiter);
                        if(err.InnerException.InnerException.InnerException != null && string.IsNullOrEmpty(err.InnerException.InnerException.InnerException.Message)) {
                            value = string.Format("{0}{1}{2}", value, err.InnerException.InnerException.InnerException.Message, delimiter);
                        }
                    }
                }
            }
            value = value.TrimEnd(delimiter.ToCharArray());
            return value.Split(delimiter.ToCharArray());
        }
    }
}
