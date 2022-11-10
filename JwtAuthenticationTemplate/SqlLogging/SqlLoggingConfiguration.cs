using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthenticationTemplate
{
    public class SqlLoggingConfiguration
    {
        public string ErrorLogTable { get; set; }
        public string[] ErrorLogTableColumns { get; set; }
        public string ApiLogTable { get; set; }
        public string[] ApiLogTableColumns { get; set; }
        public string SqlConnectionString { get; set; }
        public SqlLoggingConfiguration()
        {
            ErrorLogTable = "Error log table name";
            ErrorLogTableColumns = new string[7] { "c1", "c2", "", "", "", "", "" };
            ApiLogTable = "Api log table name";
            ApiLogTableColumns = new string[7] { "c1", "c2", "", "", "", "", "" };
            SqlConnectionString = "connection string";

        }
    }
}
