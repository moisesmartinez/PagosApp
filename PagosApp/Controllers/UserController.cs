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

            if (reader2.HasRows)
            {
                while (reader2.Read())
                {
                    li.Add(new SelectListItem { Text = reader2.GetString(1), Value = reader2.GetValue(0).ToString() });
                }

                ViewData["Rol"] = li;
            }

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
            reader.Close();
            cn.Close();
            cmd.Dispose();
            cmd2.Dispose();


            return View(model);
        }

     


        [Authorize]
        public ActionResult Adduser()
        {

            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();

            string querylist = "select * from roles";
            OleDbCommand cmd2 = new OleDbCommand(querylist, cn);
            OleDbDataReader reader2 = cmd2.ExecuteReader();
            List<SelectListItem> li = new List<SelectListItem>();
            Models.User2 u = new Models.User2();
            while (reader2.Read())
            {
                li.Add(new SelectListItem { Text = reader2.GetString(1), Value = reader2.GetValue(0).ToString() });
            }

            ViewData["Rol"] = li;

            reader2.Close();
            cmd2.Dispose();
            cn.Close();



            return View(u);
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


            //OleDbConnection cn = new OleDbConnection(cadena);
            //cn.Open();


            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();
            string name = user.Nombre;
            string password = user.Password;
            string rol = user.rol;
            bool deleted = user.Estado;
            string activo = "";
                if (deleted)
            {
                activo = "0";
            }
            else
            {
                activo = "1";
            }
            int id_rol = Convert.ToInt32(user.rol);
            
            ViewData["UserName"] = name;
            string query = "exec sp_editusuario '" + user.Usuario + "','" + name + "','" + password + "'," + activo + "," + id_rol;
            List<SelectListItem> li = new List<SelectListItem>();
            ViewData["error"] = "Customer Data Update successfully";
            string querylist = "select * from roles";
            OleDbCommand cmd2 = new OleDbCommand(querylist, cn);
            OleDbDataReader reader2 = cmd2.ExecuteReader();
            int result = 0;

            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }

            if(result == 1)
            {
                ViewData["error"] = "Usuario actualizado Satisfactoriamente";
            }
            else
            {
                ViewData["error"] = "Error al actualizar Usuario";
            }

            while (reader2.Read())
            {
                li.Add(new SelectListItem { Text = reader2.GetString(1), Value = reader2.GetValue(0).ToString() });
            }

            ViewData["Rol"] = li;

            reader2.Close();






            return View(user);
           
        }

        
       

    }
}
