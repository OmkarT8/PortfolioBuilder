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
    public class ProjectController : Controller
    {
        DBConnection dbc;
        SqlConnection conn;

        [HttpGet]
        public ActionResult AddProject()
        {
            if (Session["userRegid"] != null)
            {
                return View(new ProjectModel());
            }
            return RedirectToAction("LoginView", "Login");
            
        }
        [HttpPost]
        public ActionResult AddProject(ProjectModel pm)
        {
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);

                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();

                string query = "Insert into userProjectTable(pro1name,pro1des,pro2name,pro2des,userid) Values(@pro1name,@pro1des,@pro2name,@pro2des,@userid)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pro1name", pm.prj1_name);
                cmd.Parameters.AddWithValue("@pro1des", pm.prj1_description);
                cmd.Parameters.AddWithValue("@pro2name", pm.prj2_name);
                cmd.Parameters.AddWithValue("@pro2des", pm.prj2_description);
                cmd.Parameters.AddWithValue("@userid", userID);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["SuccessmsgAP"] = "Data Uploaded Successfully!";
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditPD(int id)
        {
            if (Session["userRegid"] != null)
            {
                try
                {
                    int userID = Convert.ToInt32(Session["userid"]);
                    ProjectModel pm = new ProjectModel();
                    DataTable dt = new DataTable();
                    dbc = new DBConnection();
                    conn = new SqlConnection();
                    conn = dbc.ConnString();
                    conn.Open();
                    string query = "select pro1name,pro1des,pro2name,pro2des from userProjectTable where userid=" + userID + "";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    sda.Fill(dt);
                    conn.Close();
                    if (dt.Rows.Count == 1)
                    {
                        pm.prj1_name = dt.Rows[0][0].ToString();
                        pm.prj1_description = dt.Rows[0][1].ToString();
                        pm.prj2_name = dt.Rows[0][2].ToString();
                        pm.prj2_description = dt.Rows[0][3].ToString();
                        return View(pm);

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
        public ActionResult EditPD(ProjectModel pm)
        {
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);
                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update userProjectTable set pro1name=@pro1name,pro1des=@pro1des,pro2name=@pro2name,pro2des=@pro2des where userid=" + userID + "";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@pro1name", pm.prj1_name);
                cmd.Parameters.AddWithValue("@pro1des", pm.prj1_description);
                cmd.Parameters.AddWithValue("@pro2name", pm.prj2_name);
                cmd.Parameters.AddWithValue("@pro2des", pm.prj2_description);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["UpdatePD"] = "Data Updated Successfully.";
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
    }
}