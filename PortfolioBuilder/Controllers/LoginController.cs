using PortfolioBuilder.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfolioBuilder.Controllers
{
    public class LoginController : Controller
    {
        DBConnection dbc;
        SqlConnection conn;
        
        public ActionResult Logout()
        {
            Session["userRegid"] = null;
            Session["userid"] = null;
            Session.Abandon();
            return RedirectToAction("LoginView", "Login");
        }

        [HttpGet]
        public ActionResult LoginView()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult LoginView(LoginModel lm)
        {
            try
            {
                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();
                string query = "Select * from LoginTable where username=@username and password=@password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", lm.Username);
                cmd.Parameters.AddWithValue("@password", lm.Password);
                SqlDataReader sdr = cmd.ExecuteReader();
                if(sdr.Read())
                {
                    string userRegid = sdr["userRegid"].ToString();
                    TempData["userRegid"] = userRegid;
                    Session["userRegid"] = sdr["userRegid"].ToString();
                    if(Session["userRegid"]!=null)
                    {
                        return RedirectToAction("getAllDetails", "ShowAllData");
                    }
                }
                else
                {
                    ViewData["msg"] = "Invalid Username or Password !";
                }
                conn.Close();
                return View();
            }
            catch
            {
                return View();
            }
        }
        
    }
}