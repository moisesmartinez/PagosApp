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
                name: "Cliente/upload",
                url: "upload",
                defaults: new { controller = "Cliente", action = "Upload" }
                );

            routes.MapRoute(
               name: "Cliente/download",
               url: "download/{ImageName}",
               defaults: new { controller = "Cliente", action = "download" }
                );

            routes.MapRoute(
                name: "Cliente/Editcliente",
                url: "editcliente/{id_cliente}",
                defaults: new { controller = "Cliente", action = "Editcliente" }
                );

            routes.MapRoute(
                name: "Cliente/Addcliente",
                url: "AddCliente",
                defaults: new { controller = "Cliente", action = "Addcliente"}
                );

            routes.MapRoute(
                name: "Home/Administrarclientes",
                url: "Administrarclientes",
                defaults: new { controller = "Home", action = "AdministrarClientes"}
                );


            routes.MapRoute(
               name: "User/Saveuser",
               url: "Saveuser",
               defaults: new { controller = "User", action = "Saveuser" }
               );

            routes.MapRoute(
                name: "Rol/editRol",
                url: "editrol/{id_rol}",
                defaults: new { controller = "Rol", action = "EditRol" }
                );

            routes.MapRoute(
                name: "Rol/SaveUser",
                url: "UpdateRol",
                defaults: new { controller = "Rol", action = "UpdateRol" });

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

            routes.MapRoute(
               name: "Service/Index",
               url: "Services",
               defaults: new { controller = "Service", action = "Index" }
           );

            routes.MapRoute(
              name: "Service/Edit",
              url: "EditService/{ServiceId}",
              defaults: new { controller = "Service", action = "EditService", ServiceId = "" }
            );

            routes.MapRoute(
                name: "Service/Add",
                url: "AddService",
                defaults: new { controller = "Service", action = "AddService" }
            );





        }
    }
}