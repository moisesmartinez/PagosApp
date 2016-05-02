using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PagosApp.Controllers
{
    public class AccountController : Controller
    {
        PagosAppDBEntities myEntities = new PagosAppDBEntities();

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
                Usuario usuario = myEntities.Usuarios.Where(
                                        userLinq => userLinq.nombre.Equals(mLogin.Username) && 
                                        userLinq.clave.Equals(mLogin.Password)
                                    ).FirstOrDefault();

                //Si la instancia de usuario es nulo, entonces no encontré el usuario en la db 
                if (usuario != null)
                {
                    //guardar informacion del usuario en la Sesion del navegador
                    FormsAuthentication.SetAuthCookie(usuario.nombre, mLogin.RememberMe);
                    Session["UserSession"] = usuario.id_usuario;

                    //Luego de guardar informacion en la sesion, redireccionar al menu
                    return RedirectToAction("Index", "Home");    
                }
                else
                {
                    ViewData["Error"] = " Usuario/Contraseña incorrectos!";
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = "Error al buscar buscar usuario";
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
