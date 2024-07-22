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
    public class SkillLangController : Controller
    {
        DBConnection dbc;
        SqlConnection conn;

        [HttpGet]
        public ActionResult AddSkillLang()
        {
            if (Session["userRegid"] != null)
            {
                return View(new SkillLangModel());
            }
            return RedirectToAction("LoginView", "Login");
            
        }

        [HttpPost]
        public ActionResult AddSkillLang(SkillLangModel sl)
        {
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);

                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();

                string query = "Insert into userSkillsLangTable(userid,proskills,frontskills,backskills,lang) Values(@userid,@proskills,@frontskills,@backskills,@lang)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userid", userID);
                cmd.Parameters.AddWithValue("@proskills", sl.professionalSkills);
                cmd.Parameters.AddWithValue("@frontskills", sl.frontendSkills);
                cmd.Parameters.AddWithValue("@backskills", sl.backendSkills);
                cmd.Parameters.AddWithValue("@lang", sl.languages);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["SuccessmsgASL"] = "Data Uploaded Successfully!";
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditSLD(int id)
        {
            if (Session["userRegid"] != null)
            {
                try
                {
                    int userID = Convert.ToInt32(Session["userid"]);
                    SkillLangModel sk = new SkillLangModel();
                    DataTable dt = new DataTable();
                    dbc = new DBConnection();
                    conn = new SqlConnection();
                    conn = dbc.ConnString();
                    conn.Open();
                    string query = "select proskills,frontskills,backskills,lang from userSkillsLangTable where userid=" + userID + "";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    sda.Fill(dt);
                    conn.Close();
                    if (dt.Rows.Count == 1)
                    {
                        sk.professionalSkills = dt.Rows[0][0].ToString();
                        sk.frontendSkills = dt.Rows[0][1].ToString();
                        sk.backendSkills = dt.Rows[0][2].ToString();
                        sk.languages = dt.Rows[0][3].ToString();
                        return View(sk);

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
        public ActionResult EditSLD(SkillLangModel sk)
        {
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);
                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update userSkillsLangTable set proskills=@proskills,frontskills=@frontskills,backskills=@backskills,lang=@lang where userid=" + userID + "";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@proskills", sk.professionalSkills);
                cmd.Parameters.AddWithValue("@frontskills", sk.frontendSkills);
                cmd.Parameters.AddWithValue("@backskills", sk.backendSkills);
                cmd.Parameters.AddWithValue("@lang", sk.languages); ;
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["UpdateSLD"] = "Data Updated Successfully.";
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
    }
}