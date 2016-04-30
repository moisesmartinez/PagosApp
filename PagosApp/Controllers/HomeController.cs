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

        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult AdministrarUsuarios()
        {
            Models.UserList lita = new Models.UserList();

            return View();
        }

    }
}
