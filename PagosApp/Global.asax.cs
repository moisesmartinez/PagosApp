using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Data.OleDb;

namespace PagosApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {

        string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["LocalConnection"].ToString();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }



        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (System.Web.Security.FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //let us take out the username now                
                        string username = System.Web.Security.FormsAuthentication.Decrypt(Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        OleDbConnection cn = new OleDbConnection(cadena);
                        cn.Open();
                        string query = "exec get_rol_usuario_by_usuario '" + username + "'";
                        OleDbCommand cmd = new OleDbCommand(query, cn);
                        OleDbDataReader reader = cmd.ExecuteReader();
                        int id_rol = 0;

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                id_rol = reader.GetInt32(0);
                            }
                        }

                        string[] show = id_rol.ToString().Split(';');

                        //let us extract the roles from our own custom cookie


                        //Let us set the Pricipal with our user specific details
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                      new System.Security.Principal.GenericIdentity(username, "Forms"), show) ;
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }


    }
}