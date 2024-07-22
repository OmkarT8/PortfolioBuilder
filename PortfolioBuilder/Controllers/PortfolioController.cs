using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfolioBuilder.Controllers
{
    public class PortfolioController : Controller
    {
        DBConnection dbc;
        SqlConnection conn;

        [HttpGet]
        public ActionResult Portfolio(int id)
        {
            try
            {
                Byte[] bytes = null;
                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                DataTable dt = new DataTable();
                conn.Open();
                string query = "SELECT userDetailsTable.uname,userDetailsTable.urole,userDetailsTable.uabout,userDetailsTable.uphoto,userDetailsTable.uemail,userDetailsTable.ucontact,userEducationTable.sscdate,userEducationTable.sscschool,userEducationTable.sscpercent,userEducationTable.hscdate,userEducationTable.hsccollege,userEducationTable.hscpercent,userEducationTable.graduni,userEducationTable.gradcgpa,userSkillsLangTable.proskills,userSkillsLangTable.frontskills,userSkillsLangTable.backskills,userSkillsLangTable.lang,userProjectTable.pro1name,userProjectTable.pro1des,userProjectTable.pro2name,userProjectTable.pro2des,userExperienceTable.exp1StartDate,userExperienceTable.exp1EndDate,userExperienceTable.exp1Des,userExperienceTable.exp2StartDate,userExperienceTable.exp2EndDate,userExperienceTable.exp2Des FROM userDetailsTable INNER JOIN userEducationTable ON userDetailsTable.userid=userEducationTable.userid INNER JOIN userSkillsLangTable ON userDetailsTable.userid=userSkillsLangTable.userid INNER JOIN userProjectTable ON userDetailsTable.userid=userProjectTable.userid INNER JOIN userExperienceTable ON userDetailsTable.userid=userExperienceTable.userid where userDetailsTable.userid=" + id + "";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                sda.Fill(dt);
                conn.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    bytes = (byte[])dt.Rows[0][3];
                    ViewBag.Image = ViewImage(bytes);
                    return View(dt);
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

        private string ViewImage(byte[] arrayImage)
        {
            string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);
            return "data:image/png;base64," + base64String;
        }
    }
}