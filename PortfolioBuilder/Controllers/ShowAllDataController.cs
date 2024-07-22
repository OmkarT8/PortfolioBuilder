using PortfolioBuilder.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PortfolioBuilder.Controllers
{
    public class ShowAllDataController : Controller
    {
        DBConnection dbc;
        SqlConnection conn;
       
        public string userid = null;

        public void GetUserId()
        {
            try
            {
                int userRegID = Convert.ToInt32(Session["userRegid"]);

                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();
                string query = "select userid from userDetailsTable where userRegid=" + userRegID + "";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    userid = sdr["userid"].ToString();
                }
                TempData["userid"] = userid;
                Session["userid"] = userid;
                conn.Close();
            }
            catch { }
        }
        [HttpGet]
        public ActionResult getAllDetails()
        {
            try
            {
                Byte[] bytes = null;
                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                //int userRegID = Convert.ToInt32(Session["userRegid"]);
                DataTable dt = new DataTable();
                GetUserId();
                int userID = Convert.ToInt32(Session["userid"]);
                conn.Open();
                string query = "SELECT userDetailsTable.uname,userDetailsTable.urole,userDetailsTable.uabout,userDetailsTable.uphoto,userDetailsTable.uemail,userDetailsTable.ucontact,userEducationTable.sscdate,userEducationTable.sscschool,userEducationTable.sscpercent,userEducationTable.hscdate,userEducationTable.hsccollege,userEducationTable.hscpercent,userEducationTable.graduni,userEducationTable.gradcgpa,userSkillsLangTable.proskills,userSkillsLangTable.frontskills,userSkillsLangTable.backskills,userSkillsLangTable.lang,userProjectTable.pro1name,userProjectTable.pro1des,userProjectTable.pro2name,userProjectTable.pro2des,userExperienceTable.exp1StartDate,userExperienceTable.exp1EndDate,userExperienceTable.exp1Des,userExperienceTable.exp2StartDate,userExperienceTable.exp2EndDate,userExperienceTable.exp2Des FROM userDetailsTable INNER JOIN userEducationTable ON userDetailsTable.userid=userEducationTable.userid INNER JOIN userSkillsLangTable ON userDetailsTable.userid=userSkillsLangTable.userid INNER JOIN userProjectTable ON userDetailsTable.userid=userProjectTable.userid INNER JOIN userExperienceTable ON userDetailsTable.userid=userExperienceTable.userid where userDetailsTable.userid="+userID+"";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                sda.Fill(dt);
                conn.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    bytes = (byte[])dt.Rows[0][3];
                    ViewBag.Image = ViewImage(bytes);
                    if (Session["userRegid"] != null)
                    {
                        return View(dt);
                    }
                    return RedirectToAction("LoginView", "Login");
                }
                else
                {
                    return RedirectToAction("CreateUser", "userDetails");
                }
            }
            catch
            {
                return View("Error");
            }    
        }

        private string ViewImage(byte[] arrayImage)
        {
            string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);
            return "data:image/png;base64," + base64String;
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        [HttpPost]
        public ActionResult ChangeDP(UserDetailsModel ud)
        {
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);
                Byte[] bytes = null;
                if (ud.Filepic.FileName != null)
                {
                    Stream fs = ud.Filepic.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    dbc = new DBConnection();
                    conn = new SqlConnection();
                    conn = dbc.ConnString();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Update userDetailsTable set uphoto=@uphoto where userid=" + userID + "";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@uphoto", bytes);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return RedirectToAction("getAllDetails","ShowAllData");
            }
            catch
            {
                return View("Error");
            }
        }

    }
}