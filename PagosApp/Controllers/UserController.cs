using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;

namespace PagosApp.Controllers
{
    public class UserController : Controller
    {
        string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["LocalConnection"].ToString();
        //
        // GET: /User/
        [Authorize]
        public ActionResult Edituser(string userid)
        {

            string usuario = Server.HtmlEncode(userid);
           
            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();
            string query = "exec sp_getinfo_usuario '"+ usuario +"'";
            var model = new PagosApp.Models.User();
            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();

            
            String nombre;
            String rol;
            String Status;


            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    nombre = reader.GetString(0);
                    Status = reader.GetString(1);
                    rol = reader.GetString(2);

                    model.Usuario = usuario;
                    model.Nombre = nombre;
                    model.rol = Status;

                }

            }


            return View(model);
        }

        [Authorize]
        public ActionResult Adduser()
        {
            return View();
        }


        [Authorize]
        public ActionResult Updateuser(Models.User user)
        {
            return View();
        }

    }
}
