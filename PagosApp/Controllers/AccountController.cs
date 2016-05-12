﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.OleDb;

namespace PagosApp.Controllers
{
    public class AccountController : Controller
    {
       // PagosAppDBEntities myEntities = new PagosAppDBEntities();
        string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["LocalConnection"].ToString();
        
        int id_rol;

        [AllowAnonymous]
        public ActionResult Login()
        {

         
            if (Request.IsAuthenticated) 
            {

            

                    return RedirectToAction("Index", "Home");
                
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Login mLogin)
        {
            try
            {
                //Buscar si hay un usuario con la misma usuario y clave en la base de datos.


                /*
                Usuario usuario = myEntities.Usuarios.Where(
                                        userLinq => userLinq.nombre.Equals(mLogin.Username) && 
                                        userLinq.clave.Equals(mLogin.Password)
                                    ).FirstOrDefault();
                                    */
                //Si la instancia de usuario es nulo, entonces no encontré el usuario en la db 

                OleDbConnection cn = new OleDbConnection(cadena);
                cn.Open();
                string query = "exec usplogin '" + mLogin.Username + "','" + mLogin.Password + "'";

                OleDbCommand cmd = new OleDbCommand(query, cn);
                OleDbDataReader reader = cmd.ExecuteReader();


                int login = 0;
                string nombre = "";
                string id = "";
                int id_rol = -1;
                int deleted = 0;

                while (reader.Read())
                {
                    login = reader.GetInt32(0);
                    nombre = reader.GetValue(2).ToString();
                    id = reader.GetValue(1).ToString();
                    id_rol = reader.GetInt32(3);
                    deleted = reader.GetInt32(4);
                }




                if ((login == 0) && (deleted == 0))
                {
                    //guardar informacion del usuario en la Sesion del navegador
                    FormsAuthentication.SetAuthCookie(nombre, mLogin.RememberMe);
                    Session["UserSession"] = id;
                    Session["Rol"] = id_rol;

                    //Luego de guardar informacion en la sesion, redireccionar al menu
                    return RedirectToAction("Index", "Home");    

                }
                else
                {
                    if (deleted == 0)
                    {
                        ViewData["Error"] = " Usuario/Contraseña incorrectos!";
                    }
                    else
                    {
                        ViewData["Error"] = " Usuario no Activo";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            //Esto es para limpiar el campo de contraseña, para que la vuelva a escribir
            ModelState.Remove("Password");
            return View();
        }


        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}
