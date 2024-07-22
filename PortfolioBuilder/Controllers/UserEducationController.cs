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
    public class UserEducationController : Controller
    {
        DBConnection dbc;
        SqlConnection conn;
        [HttpGet]
        public ActionResult AddEducation()
        {
            if(Session["userRegid"] != null)
            {
                return View(new UserEducationModel());
            }
            return RedirectToAction("LoginView", "Login");
        }

        [HttpPost]
        public ActionResult AddEducation(UserEducationModel ue)
        { 
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);

                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();

                string query = "Insert into userEducationTable(sscdate,sscschool,sscpercent,hscdate,hsccollege,hscpercent,graduni,gradcgpa,userid) Values(@sscdate,@sscschool,@sscpercent,@hscdate,@hsccollege,@hscpercent,@graduni,@gradcgpa,@userid)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sscdate", ue.sscDate);
                cmd.Parameters.AddWithValue("@sscschool", ue.sscSchool);
                cmd.Parameters.AddWithValue("@sscpercent", ue.sscPercent);
                cmd.Parameters.AddWithValue("@hscdate", ue.hscDate);
                cmd.Parameters.AddWithValue("@hsccollege", ue.hscCollege);
                cmd.Parameters.AddWithValue("@hscpercent", ue.hscPercent);
                cmd.Parameters.AddWithValue("@graduni", ue.gradUni);
                cmd.Parameters.AddWithValue("@gradcgpa", ue.gradCgpa);
                cmd.Parameters.AddWithValue("@userid", userID);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["SuccessmsgAE"] = "Data Uploaded Successfully!";
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditED(int id)
        {
            if (Session["userRegid"] != null)
            {
                try
                {
                    int userID = Convert.ToInt32(Session["userid"]);
                    UserEducationModel ue = new UserEducationModel();
                    DataTable dt = new DataTable();
                    dbc = new DBConnection();
                    conn = new SqlConnection();
                    conn = dbc.ConnString();
                    conn.Open();
                    string query = "select sscdate,sscschool,sscpercent,hscdate,hsccollege,hscpercent,graduni,gradcgpa from userEducationTable where userid=" + userID + "";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    sda.Fill(dt);
                    conn.Close();
                    if (dt.Rows.Count == 1)
                    {
                        ue.sscDate = dt.Rows[0][0].ToString();
                        ue.sscSchool = dt.Rows[0][1].ToString();
                        ue.sscPercent = (double)dt.Rows[0][2];
                        ue.hscDate = dt.Rows[0][3].ToString();
                        ue.hscCollege = dt.Rows[0][4].ToString();
                        ue.hscPercent = (double)dt.Rows[0][5];
                        ue.gradUni = dt.Rows[0][6].ToString();
                        ue.gradCgpa = (double)dt.Rows[0][7];
                        return View(ue);

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
        public ActionResult EditED(UserEducationModel ue)
        {
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);
                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update userEducationTable set sscdate=@sscdate,sscschool=@sscschool,sscpercent=@sscpercent,hscdate=@hscdate,hsccollege=@hsccollege,hscpercent=@hscpercent,graduni=@graduni,gradcgpa=@gradcgpa where userid=" + userID + "";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@sscdate", ue.sscDate);
                cmd.Parameters.AddWithValue("@sscschool", ue.sscSchool);
                cmd.Parameters.AddWithValue("@sscpercent", ue.sscPercent);
                cmd.Parameters.AddWithValue("@hscdate", ue.hscDate);
                cmd.Parameters.AddWithValue("@hsccollege", ue.hscCollege);
                cmd.Parameters.AddWithValue("@hscpercent", ue.hscPercent);
                cmd.Parameters.AddWithValue("@graduni", ue.gradUni);
                cmd.Parameters.AddWithValue("@gradcgpa", ue.gradCgpa);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["UpdateUE"] = "Data Updated Successfully.";
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
