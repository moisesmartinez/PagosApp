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
            List<SelectListItem> li = new List<SelectListItem>();

            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();
            string query = "exec sp_getinfo_usuario '"+ usuario +"'";
            var model = new PagosApp.Models.User();
            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();
            string querylist = "select * from roles";
            OleDbCommand cmd2 = new OleDbCommand(querylist, cn);
            OleDbDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                li.Add(new SelectListItem { Text = reader2.GetString(1), Value = reader2.GetValue(0).ToString() });
            }

            ViewData["Rol"] = li;

            reader2.Close();
            
            String nombre;
            String rol;
            String Status;


            if (reader.HasRows)
            {

                

                while (reader.Read())
                {
                    nombre = reader.GetString(0);
                    ViewData["UserName"] = nombre;
                    Status = reader.GetString(1);
                    rol = reader.GetString(2);

                    model.Usuario = usuario;
                    model.Nombre = nombre;
                    model.rol = rol;
                    model.id_rol = reader.GetInt32(3);

                    if (Status.Equals("Activo"))
                    {
                        model.Estado = true;
                    }

                }

            }


            return View(model);
        }

        [Authorize]
        public ActionResult Adduser()
        {
            return View();
        }

        public ActionResult Updateuser()
        {

            OleDbConnection cn = new OleDbConnection(cadena);
            //cn.Open();

            RedirectToAction("AdministrarUsuarios", "Home");
            return View();
        }


        [HttpPost]
        public ActionResult Updateuser(Models.User user)
        {

            if (string.IsNullOrEmpty(user.Nombre))
            {
                ModelState.AddModelError("Nombre", "Ingrese Nombre");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("Password", "Ingrese la Contrasena");
            }
           
            if (string.IsNullOrEmpty(user.ConfirmPassword))
            {
                ModelState.AddModelError("ConfirmPassword", "Ingrese la Contrasena");
            }

            if (user.Password != user.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "No concuerda");
            }
           // OleDbConnection cn = new OleDbConnection(cadena);
            //cn.Open();

            String name = user.Nombre;

            

            return View();
           
        }

    }
}
