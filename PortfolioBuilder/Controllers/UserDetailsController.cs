using PortfolioBuilder.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfolioBuilder.Controllers
{
    public class UserDetailsController : Controller
    {
        DBConnection dbc;
        SqlConnection conn;

        [HttpGet]
        public ActionResult CreateUser()
        {
            if(Session["userRegid"]!=null)
            {
                return View(new UserDetailsModel());
            }
            return RedirectToAction("LoginView", "Login");
        }
        [HttpPost]
        public ActionResult CreateUser(UserDetailsModel ud)
        {
            try
            {
                int userRegID = Convert.ToInt32(Session["userRegid"]);
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
                    cmd.CommandText = "Insert into userDetailsTable(uname,urole,uabout,uphoto,uemail,ucontact,userRegid) Values(@uname,@urole,@uabout,@uphoto,@uemail,@ucontact,@userregid)";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@uname", ud.uname);
                    cmd.Parameters.AddWithValue("@urole", ud.urole);
                    cmd.Parameters.AddWithValue("@uabout", ud.uabout);
                    cmd.Parameters.AddWithValue("@uphoto", bytes);
                    cmd.Parameters.AddWithValue("@uemail", ud.uemail);
                    cmd.Parameters.AddWithValue("@ucontact", ud.ucontact);
                    cmd.Parameters.AddWithValue("@userregid", userRegID);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    TempData["SuccessmsgCU"] = "Data Uploaded Successfully.";
                }
                return View();
            }
            catch
            {
                return View();
            }   
        }

        [HttpGet]
        public ActionResult EditUD(int id)
        {
            if (Session["userRegid"] != null)
            {
                try
                {
                    int userID = Convert.ToInt32(Session["userid"]);
                    UserDetailsModel ud = new UserDetailsModel();
                    DataTable dt = new DataTable();
                    dbc = new DBConnection();
                    conn = new SqlConnection();
                    conn = dbc.ConnString();
                    conn.Open();
                    string query = "select uname,urole,uabout,uemail,ucontact from userDetailsTable where userid=" + userID + "";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    sda.Fill(dt);
                    conn.Close();
                    if (dt.Rows.Count == 1)
                    {
                        ud.uname = dt.Rows[0][0].ToString();
                        ud.urole = dt.Rows[0][1].ToString();
                        ud.uabout = dt.Rows[0][2].ToString();
                        ud.uemail = dt.Rows[0][3].ToString();
                        ud.ucontact = dt.Rows[0][4].ToString();
                        return View(ud);

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
        public ActionResult EditUD(UserDetailsModel ud)
        {
            try
            {
                int userID = Convert.ToInt32(Session["userid"]);    
                dbc = new DBConnection();
                conn = new SqlConnection();
                conn = dbc.ConnString();
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Update userDetailsTable set uname=@uname,urole=@urole,uabout=@uabout,uemail=@uemail,ucontact=@ucontact where userid=" + userID + "";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@uname", ud.uname);
                cmd.Parameters.AddWithValue("@urole", ud.urole);
                cmd.Parameters.AddWithValue("@uabout", ud.uabout);
                cmd.Parameters.AddWithValue("@uemail", ud.uemail);
                cmd.Parameters.AddWithValue("@ucontact", ud.ucontact);
                cmd.ExecuteNonQuery();
                conn.Close();
                TempData["UpdateUD"] = "Data Updated Successfully.";
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
        public byte[] AsJpeg(byte[] data)
        {
            using (var inStream = new MemoryStream(data))
            using (var outStream = new MemoryStream())
            {
                var imageStream = Image.FromStream(inStream);
                imageStream.Save(outStream, ImageFormat.Jpeg);
                return outStream.ToArray();
            }
        }

        [HttpGet]
        public ActionResult ChangeDP(int id)
        {
            if (Session["userRegid"] != null)
            {
                try
                {
                    int userID = Convert.ToInt32(Session["userid"]);
                    UserDetailsModel ud = new UserDetailsModel();
                    DataTable dt = new DataTable();
                    dbc = new DBConnection();
                    conn = new SqlConnection();
                    conn = dbc.ConnString();
                    conn.Open();
                    string query = "select uname,urole,uabout,uemail,ucontact from userDetailsTable where userid=" + userID + "";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    sda.Fill(dt);
                    conn.Close();
                    if (dt.Rows.Count == 1)
                    {
                        ud.uname = dt.Rows[0][0].ToString();
                        ud.urole = dt.Rows[0][1].ToString();
                        ud.uabout = dt.Rows[0][2].ToString();
                        ud.uemail = dt.Rows[0][3].ToString();
                        ud.ucontact = dt.Rows[0][4].ToString();
                        return View(ud);
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
        public ActionResult ChangeDP(UserDetailsModel ud)
        {
            try
            {
                Byte[] bytes = null;
                int userID = Convert.ToInt32(Session["userid"]);
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
                    TempData["DPUpdated"] = "Data Updated Successfully.";
                }
                return View();
            }
            catch
            {
                return View("Error");
            }
        }
    }
}