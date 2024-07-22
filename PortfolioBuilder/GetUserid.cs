using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortfolioBuilder
{
    public class GetUserid
    {
        public string getUserId(string query,Dictionary<string,string>param)
        {
            string userid="";
            DBConnection dbc = new DBConnection();
            SqlConnection conn = new SqlConnection();
            conn = dbc.ConnString();
            SqlCommand cmd = new SqlCommand(query, conn);
            foreach(var item in param)
            {
                cmd.Parameters.AddWithValue(item.Key, item.Value);
            }
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                userid = sdr["userid"].ToString();
            }
            return userid;
        }
    }
}