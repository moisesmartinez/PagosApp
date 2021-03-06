﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace PagosApp.Controllers
{
    public class ServiceController : Controller
    {
        string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["LocalConnection"].ToString();
        //string cadena = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\STUFF\\Clases de la Maestria\\Administracion de Proyectos de SW\\Iteracion2\\PagosApp\\PagosApp\\App_Data\\PagosAppDB.mdf\"; Integrated Security=True";
        //string cadena = "Data Source=.\\SQLEXPRESS;AttachDbFilename=\"C:\\Users\\Moises David\\Desktop\\DEV TOOLS\\Maestria\\Administracion de Proyectos SW\\Iteracion2\\PagosApp\\PagosApp\\App_Data\\PagosAppDB.mdf\";Integrated Security=True;User Instance=True";

        //LIST SERVICES
        [AllowAnonymous]
        [Authorize]
        public ActionResult Index()
        {
            Models.ServicesList listaServicios = new Models.ServicesList();
            try
            {
                OleDbConnection cn = new OleDbConnection(cadena);
                cn.Open();

                int userId = Convert.ToInt32(Session["UserSession"]);

                int id_rol = 0;
                string usuario = "";




                
                string query = "select nombre,usuario from usuarios where id =" + Session["UserSession"];
                OleDbCommand cmd = new OleDbCommand(query, cn);
                OleDbDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    ViewData["Username"] = reader.GetValue(0).ToString();
                    usuario = reader.GetString(1);
                }




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
                string query3;
                if (id_rol != 1)
                {

                    query3 = string.Format(@"Exec list_servicios '{0}'", usuario);
                }

                else
                {

                    query3 = string.Format(@"Exec list_servicios");
                }

                OleDbCommand cmd3 = new OleDbCommand(query3, cn);
                OleDbDataReader reader3= cmd3.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader3);

                foreach (DataRow rowX in dt.Rows)
                {
                    Models.Service tmp = new Models.Service();
                    tmp.id_servicio = Convert.ToInt32(rowX["id_servicio"]);
                    tmp.Descripcion = rowX["Descripcion"].ToString();
                    tmp.empresa = rowX["nombre_de_empresa"].ToString();
                    listaServicios.ListOfServices.Add(tmp);
                }

                cn.Close();
            }
            catch(Exception ex)
            {

            }

            return View(listaServicios);
        }

        //ADD SERVICE FUNCTION
        public ActionResult AddService()
        {
            Models.Service servicio = new Models.Service();

            return View(servicio);    
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddService(Models.Service servicio)
        {
            try
            {
                OleDbConnection cn = new OleDbConnection(cadena);
                cn.Open();

                //BUSCAR EL CODIGO DE EMPRESA DEL USUARIO
                int userId = Convert.ToInt32(Session["UserSession"]);
                string query_cod_empresa = "SELECT cod_empresa FROM usuarios WHERE id = " + userId;
                OleDbCommand cmdEmpresa = new OleDbCommand(query_cod_empresa, cn);
                OleDbDataReader readerEmpresa = cmdEmpresa.ExecuteReader();
                int cod_empresa = -1;
                if (readerEmpresa.HasRows)
                {
                    readerEmpresa.Read();
                    cod_empresa = Convert.ToInt32(readerEmpresa["cod_empresa"]);
                }
                else
                {
                    //Si no tiene una empresa, regresarlo al index principal
                    return RedirectToAction("Index", "Home");
                }

                //INSERTAR EL SERVICIO
                string query = string.Format("INSERT INTO Servicios (descripcion, cod_empresa) VALUES ('{0}', {1})", servicio.Descripcion, cod_empresa);

                OleDbCommand cmd = new OleDbCommand(query, cn);
                cmd.ExecuteNonQuery();

                cn.Close();

                ViewData["Success"] = "Se guardó el servicio correctamente.";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {

            }

            return View(servicio);
        }

        //UPDATE SERVICE FUNCTIONS
        [Authorize]
        public ActionResult EditService(string ServiceId)
        {
            Models.Service servicio = new Models.Service();

            try
            {
                OleDbConnection cn = new OleDbConnection(cadena);
                cn.Open();

                int userId = Convert.ToInt32(Session["UserSession"]);
                //int userId = 1;
                string query = string.Format("SELECT Descripcion, Id_Servicio FROM Servicios WHERE Id_Servicio = {0}", ServiceId);

                OleDbCommand cmd = new OleDbCommand(query, cn);
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    servicio.Descripcion = reader["Descripcion"].ToString();
                    servicio.id_servicio = Convert.ToInt32(ServiceId);
                }
                else
                {
                    ViewData["Error"] = "El servicio que se quizo editar no existe o no es permitido!";
                    return RedirectToAction("Index");
                }

                cn.Close();
            }
            catch (Exception ex)
            {

            }

            return View(servicio);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditService(Models.Service servicio)
        {
            try
            {
                OleDbConnection cn = new OleDbConnection(cadena);
                cn.Open();

                int userId = Convert.ToInt32(Session["UserSession"]);

                string query = "";
                if (servicio.SubmitButton == "Actualizar")
                {
                    //ACTUALIZAR EL SERVICIO
                    //int userId = 1;
                    query = string.Format("UPDATE Servicios SET descripcion = '{0}' where id_Servicio = {1}", servicio.Descripcion,servicio.id_servicio);
                }
                else if (servicio.SubmitButton == "Eliminar")
                {
                    //ELIMINAR EL SERVICIO
                    //int userId = 1;
                    query = string.Format("DELETE Servicios WHERE id_servicio = {0}", servicio.id_servicio);
                }
                else
                {
                    servicio.SubmitButton = "";
                    return View(servicio);
                }

                OleDbCommand cmd = new OleDbCommand(query, cn);
                cmd.ExecuteNonQuery();

                cn.Close();

                ViewData["Success"] = "Se actualizó el servicio correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }

            return View(servicio);
        }

    }
}
