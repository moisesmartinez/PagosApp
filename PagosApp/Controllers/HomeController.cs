using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PagosApp.Controllers
{
    public class HomeController : Controller
    {
        PagosAppDBEntities myEntities = new PagosAppDBEntities();

        [Authorize]
        public ActionResult Index()
        {
            if (Session["UserSession"] != null)
            {
                try
                {
                    //Get the UserId and then retrieve his email credentials.
                    int userId = Convert.ToInt32(Session["UserSession"]);
                    PagosApp.Usuario user = myEntities.Usuarios.Where(userLinq => userLinq.id_usuario == (userId)).FirstOrDefault();
                    ViewData["Username"] = user.nombre_completo;
                }
                catch (Exception ex)
                {
                    ViewData["Error"] = "Error al cargar la información del usuario";
                }
            }

            return View();
        }

        [Authorize]
        public ActionResult AdministrarUsuarios()
        {
            Models.UserList listaUsuarios = new Models.UserList();

            return View();
        }

    }
}
