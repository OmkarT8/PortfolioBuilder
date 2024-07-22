using PortfolioBuilder.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfolioBuilder.Controllers
{
    public class ExperienceController : Controller
    {
        DBConnection dbc;
        SqlConnection conn;

        [HttpGet]
        public ActionResult AddExperience()
        {
            if (Session["userRegid"] != null)
            {
                return View(new ExperienceModel());
            }
            return RedirectToAction("LoginView", "Login");
            
        }

        [HttpPost]
        public ActionResult AddExperience(ExperienceModel ex)
        {
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);

                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();

                string query = "Insert into userExperienceTable(userid,exp1StartDate,exp1EndDate,exp1Des,exp2StartDate,exp2EndDate,exp2Des) Values(@userid,@exp1sd,@exp1ed,@exp1des,@exp2sd,@exp2ed,@exp2des)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userid", userID);
                cmd.Parameters.AddWithValue("@exp1sd", ex.exp1_start_dt);
                cmd.Parameters.AddWithValue("@exp1ed", ex.exp1_end_dt);
                cmd.Parameters.AddWithValue("@exp1des", ex.exp1_description);
                cmd.Parameters.AddWithValue("@exp2sd", ex.exp2_start_dt);
                cmd.Parameters.AddWithValue("@exp2ed", ex.exp2_end_dt);
                cmd.Parameters.AddWithValue("@exp2des", ex.exp2_description);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["SuccessmsgAExp"] = "Data Uploaded Successfully!";
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditEX(int id)
        {
            if (Session["userRegid"] != null)
            {
                try
                {
                    int userID = Convert.ToInt32(Session["userid"]);
                    ExperienceModel ex = new ExperienceModel();
                    DataTable dt = new DataTable();
                    dbc = new DBConnection();
                    conn = new SqlConnection();
                    conn = dbc.ConnString();
                    conn.Open();
                    string query = "select exp1StartDate,exp1EndDate,exp1Des,exp2StartDate,exp2EndDate,exp2Des from userExperienceTable where userid=" + userID + "";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    sda.Fill(dt);
                    conn.Close();
                    if (dt.Rows.Count == 1)
                    {
                        ex.exp1_start_dt = dt.Rows[0][0].ToString();
                        ex.exp1_end_dt = dt.Rows[0][1].ToString();
                        ex.exp1_description = dt.Rows[0][2].ToString();
                        ex.exp2_start_dt = dt.Rows[0][3].ToString();
                        ex.exp2_end_dt = dt.Rows[0][4].ToString();
                        ex.exp2_description = dt.Rows[0][5].ToString();
                        return View(ex);

                    }
                    else
                    {
                        return RedirectToAction("getAllDetails", "ShowAllData");
                    }
                }
                catch
                {
                    return View("Error");
                }
            }
            return RedirectToAction("LoginView", "Login");

        }

        [HttpPost]
        public ActionResult EditEX(ExperienceModel ex)
        {
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);
                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update userExperienceTable set exp1StartDate=@exp1sd,exp1EndDate=@exp1ed,exp1Des=@exp1des,exp2StartDate=@exp2sd,exp2EndDate=@exp2ed,exp2Des=@exp2des where userid=" + userID + "";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@exp1sd", ex.exp1_start_dt);
                cmd.Parameters.AddWithValue("@exp1ed", ex.exp1_end_dt);
                cmd.Parameters.AddWithValue("@exp1des", ex.exp1_description);
                cmd.Parameters.AddWithValue("@exp2sd", ex.exp2_start_dt);
                cmd.Parameters.AddWithValue("@exp2ed", ex.exp2_end_dt);
                cmd.Parameters.AddWithValue("@exp2des", ex.exp2_description);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["UpdateEX"] = "Data Updated Successfully.";
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
    }
}