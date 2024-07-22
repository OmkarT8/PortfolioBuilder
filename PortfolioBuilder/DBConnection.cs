using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortfolioBuilder
{
    public class DBConnection
    {
        public SqlConnection ConnString()
        {
            var mainconn = System.Configuration.ConfigurationManager.ConnectionStrings["portfolioDb"].ConnectionString;
            SqlConnection con = new SqlConnection(mainconn);
            return con;
        }
    }
}