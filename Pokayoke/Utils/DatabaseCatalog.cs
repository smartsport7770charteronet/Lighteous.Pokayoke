using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Lighteous.Pokayoke.Utils {
    public static class DatabaseCatalog {
        public static string Name(string connectionString) {
            var value = string.Empty;
            if(connectionString.ToLower().Contains("initial catalog")) {
                var semiColonDelimiter = ";";
                var equalSignDelimiter = "=";
                var semiColonSplitted = connectionString.Split(semiColonDelimiter.ToCharArray());
                var initialCatalogItem = string.Empty;
                if(ContainsInitialCatalog(connectionString)) {
                    initialCatalogItem = semiColonSplitted.FirstOrDefault(whr => whr.ToLower().Contains("initial catalog"));
                }
                else if(ContainsDatabase(connectionString)) {
                    initialCatalogItem = semiColonSplitted.FirstOrDefault(whr => whr.ToLower().Contains("database"));
                }
                else if (ContainsDataSource(connectionString)) {
                    initialCatalogItem = semiColonSplitted.FirstOrDefault(whr => whr.ToLower().Contains("data source"));
                }
                if(initialCatalogItem != null) {
                    var equalSignSplitted = initialCatalogItem.Split(equalSignDelimiter.ToCharArray());
                    if(equalSignSplitted != null && equalSignSplitted.Length > 1) {
                        var strValue = equalSignSplitted[1];
                        value = strValue.Trim();
                    }
                }
            }

            return value;
        }

        private static bool ContainsInitialCatalog(string connString) {
            return connString.ToLower().Contains("initial catalog");
        }

        private static bool ContainsDatabase(string connString) {
            return connString.ToLower().Contains("database");
        }

        private static bool ContainsDataSource(string connString) {
            return connString.ToLower().Contains("data source");
        }
    }
}
