using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;

namespace PagosApp.Controllers
{
    public class RolController : Controller
    {
        string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["LocalConnection"].ToString();
        //
        // GET: /Rol/
        [Authorize]
        public ActionResult EditRol(string id_rol)
        {

            string idrol = Server.HtmlEncode(id_rol);
            string test = id_rol;
            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();
            string query = "exec sp_getinfo_rol " + id_rol + "";
            var model = new PagosApp.Models.Rol();
            bool deleted;
            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();
            

            string descripcion_rol;

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    descripcion_rol = reader.GetString(1);
                    deleted = reader.GetBoolean(2);
                    model.Descripcion_Rol = descripcion_rol;
                    model.deleted = deleted;

                }


            }
            model.idstring = test;
            
            ViewData["id_rol"] = test;


            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateRol(Models.Rol rol)
        {
            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();


            string id_rol = rol.idstring;
            string descripcion = rol.Descripcion_Rol;
            bool activo = rol.deleted;

            string deleted = "";

            if (activo)
            {
                deleted = "1";
            }
            else
            {
                deleted = "0";
            }

            string query = "exec sp_editrol " + id_rol + ",'" + descripcion + "'," + deleted;

            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ViewData["Error"] = reader.GetString(0);
            }




            return View(rol);
        }
    }
}
