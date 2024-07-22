using PortfolioBuilder.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PortfolioBuilder.Controllers
{
    public class UserRegController : Controller
    {
        DBConnection dbc;
        SqlConnection conn;

        [HttpGet]
        public ActionResult UserRegView()
        {
            return View( new UserRegModel());
        }

        [HttpPost]
        public ActionResult UserRegView(UserRegModel ur)
        {
            try
            {
                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();
                string query = "Select * from LoginTable where username=@username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", ur.Username);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    //string uname = sdr["username"].ToString();
                    //Session["userRegid"] = sdr["userRegid"].ToString();
                    //if (string.IsNullOrEmpty(uname))
                    //{

                    //}
                    //else
                    //{
                    //    ViewData["nameexist"] = "Username already exist!";
                    //}
                    ViewData["nameexist"] = "Username already exist!";
                }
                else
                {
                    try
                    {
                        dbc = new DBConnection();
                        conn = new SqlConnection();
                        conn = dbc.ConnString();
                        conn.Open();
                        string query2 = "Insert into LoginTable(username,password) Values(@username,@password)";
                        SqlCommand cmd2 = new SqlCommand(query2, conn);
                        cmd2.Parameters.AddWithValue("@username", ur.Username);
                        cmd2.Parameters.AddWithValue("@password", ur.Password);
                        cmd2.ExecuteNonQuery();
                        conn.Close();
                        TempData["msg"] = "Registered Successfully.";
                        ViewData["msg"] = "Registered Successfully.";
                        //return RedirectToAction("LoginView", "Login");
                    }
                    catch { }
                }
                
            }
            catch
            {
                return View("UserRegView");
            }
            return View("UserRegView");
        }
    }
}