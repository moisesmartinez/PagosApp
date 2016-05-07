using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PagosApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //Main page

            routes.MapRoute(
              name: "User/EditUser",
              url: "EditUser/{userid}",
              defaults: new { controller = "User", action = "EditUser", userid = "" }
              );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Home/Users",
                url: "AdminUsuarios",
                defaults: new { controller = "Home", action = "AdministrarUsuarios" }
            );


            //Account's Routes
            routes.MapRoute(
                name: "Account/Login",
                url: "Login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Account/Logout",
                url: "Logout",
                defaults: new { controller = "Account", action = "Logout" }
            );

          

            routes.MapRoute(
                name: "User/AddUser",
                url: "AddUser",
                defaults: new { controller = "User", action = "AddUser" }
                );

            routes.MapRoute(
                name: "User/Updateuser",
                url: "Updateuser",
                defaults: new { controller = "User", action = "Updateuser" }
                );

            
            

        }
    }
}