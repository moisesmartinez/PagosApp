using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.IO;
using System.Data.OleDb;

namespace PagosApp.Controllers
{
    public class HomeController : Controller
    {
        PagosAppDBEntities myEntities = new PagosAppDBEntities();
        string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["LocalConnection"].ToString();


        [Authorize]
        public ActionResult Index()
        {
            if (Session["UserSession"] != null)
            {
                try
                {
                    //Get the UserId and then retrieve his email credentials.
                    int userId = Convert.ToInt32(Session["UserSession"]);
                    //  PagosApp.Usuario user = myEntities.Usuarios.Where(userLinq => userLinq.id_usuario == (userId)).FirstOrDefault();


                    



                    // OleDbConnection cn = new OleDbConnection("Provider=SQLOLEDB; Server=" + servername + "; Database=" + serverdatabasename + "; UID=" + serverusername + "; PWD=" + serverpassword);
                    OleDbConnection cn = new OleDbConnection(cadena);
                    cn.Open();
                    string query = "select nombre from usuarios where id =" + Session["UserSession"];
                    OleDbCommand cmd = new OleDbCommand(query, cn);
                    OleDbDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        ViewData["Username"] = reader.GetValue(0).ToString();
                    }



                    reader.Close();
                    int id_rol;
                   

                    string query2 = "exec get_rol_usuario " + Session["UserSession"];
                    OleDbCommand cmd2 = new OleDbCommand(query2, cn);
                    OleDbDataReader reader2 = cmd2.ExecuteReader();

                    if (reader2.HasRows)
                    {

                        while (reader2.Read())
                        {
                            id_rol = reader2.GetInt32(0);
                            ViewBag.id_rol = id_rol;
                        }

                    }
                    
                    cn.Close();






                    //

                    //if (user.Gmails.Count > 0)
                    //{
                    //    //Finally, with the email credentials, search how many unread mails the User has.
                    //    var imap = new ImapClient("imap.gmail.com", user.Gmails.ElementAt(0).GmailAddress, user.Gmails.ElementAt(0).GmailPassword, AuthMethods.Login, 993, true);
                    //    ViewData["NewMessagesCount"] = imap.SearchMessages(SearchCondition.Unseen(), true).Count();
                    //    ViewData["GmailAddress"] = true;
                    //    imap.Logout();
                    //    imap.Disconnect();
                    //}
                    //else
                    //{
                    //    ViewData["Error"] = "No Gmail account found.";
                    //}
                }
                catch (Exception ex)
                {
                    ViewData["Error"] = "Gmail credentials failed...";
                }
            }

            return View();
        }

        [Authorize(Roles = "1")]
        public ActionResult AdministrarUsuarios()
        {



            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();
            String queryString = "exec sp_getusuarios";
            OleDbCommand cmd = new OleDbCommand(queryString, cn);
            OleDbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Close();


            if (Session["UserSession"] != null)
            {
                string query = "select nombre from usuarios where id =" + Session["UserSession"];
                OleDbCommand cmd2 = new OleDbCommand(query, cn);
                reader = cmd2.ExecuteReader();
            

            while (reader.Read())
            {
                ViewData["Username"] = reader.GetValue(0).ToString();
            }


            }
            reader.Close();
            cn.Close();


            return View(dt);
        }

        [Authorize]

        public ActionResult AdministrarRoles()
        {

            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();
            String queryString = "exec sp_getroles";
            OleDbCommand cmd = new OleDbCommand(queryString, cn);
            OleDbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Close();


            if (Session["UserSession"] != null)
            {
                string query = "select nombre from usuarios where id =" + Session["UserSession"];
                OleDbCommand cmd2 = new OleDbCommand(query, cn);
                reader = cmd2.ExecuteReader();


                while (reader.Read())
                {
                    ViewData["Username"] = reader.GetValue(0).ToString();
                }


            }
            reader.Close();
            cn.Close();


            return View(dt);
        }

        
    }
}
