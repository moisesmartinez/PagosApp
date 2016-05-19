using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.IO;
using System.Collections;
using System.Text;

namespace PagosApp.Controllers
{
    public class ClienteController : Controller
    {

        string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["LocalConnection"].ToString();
        //
        // GET: /Cliente/

        [Authorize]

        public ActionResult Addcliente()
        {

            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();
            string query = "select nombre from usuarios where id =" + Session["UserSession"];
            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();
            List<SelectListItem> li = new List<SelectListItem>();

            while (reader.Read())
            {
                ViewData["Username"] = reader.GetValue(0).ToString();
            }

            reader.Close();

            query = "exec getservicios_combobox " + Session["UserSession"];
            cmd.Dispose();

            cmd = new OleDbCommand(query, cn);
            reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                li.Add(new SelectListItem { Text = reader.GetString(1), Value = reader.GetValue(0).ToString() });

            }

            ViewData["Servicios"] = li;

            cmd.Dispose();
            reader.Close();
             query = "select cod_empresa from usuarios where id =" + Session["UserSession"];

           cmd = new OleDbCommand(query, cn);
           reader = cmd.ExecuteReader();


            int cod_empresa = 0;

            while (reader.Read())
            {

                cod_empresa = reader.GetInt32(0);

            }

            cmd.Dispose();
            reader.Close();

           
           
            cn.Close();

            return View();

        }

        [Authorize]
        [HttpPost]
        public ActionResult Addcliente(Models.Cliente cliente)
        {

            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();

            string nombre = cliente.nombre;
            string direccion = cliente.direccion;
            string telefono = cliente.telefono;
            string id_cliente = cliente.id_cliente;
            int id_servicio = cliente.id_servicio;

            string query = "select cod_empresa from usuarios where id =" + Session["UserSession"];

            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();


            int cod_empresa = 0;

            while (reader.Read()){

                cod_empresa = reader.GetInt32(0);

            }

            cmd.Dispose();
            reader.Close();

            query = "exec sp_createcliente '" + nombre + "','" + direccion + "','" + telefono + "'," + id_servicio + "," + cod_empresa + ",'" + id_cliente + "'";

            cmd = new OleDbCommand(query, cn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ViewData["Error"] = reader.GetString(0);
            }



            query = "exec getservicios_combobox " + Session["UserSession"];
            cmd.Dispose();

            cmd = new OleDbCommand(query, cn);
            reader = cmd.ExecuteReader();

            List<SelectListItem> li = new List<SelectListItem>();
            while (reader.Read())
            {
                li.Add(new SelectListItem { Text = reader.GetString(1), Value = reader.GetValue(0).ToString() });

            }

            ViewData["Servicios"] = li;



            return View();

        }


        [Authorize]
        public ActionResult Editcliente(string id_cliente)
        {


            OleDbConnection cn = new OleDbConnection(cadena);
            Models.Cliente cliente = new Models.Cliente();
            cn.Open();
            string query = "select cod_empresa from usuarios where id =" + Session["UserSession"];

            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();


            int cod_empresa = 0;

            while (reader.Read())
            {

                cod_empresa = reader.GetInt32(0);

            }

            cmd.Dispose();
            reader.Close();

            query = "exec getservicios_editclientecombobox " + Session["UserSession"] + ",'" + id_cliente + "'";
            cmd.Dispose();

            cmd = new OleDbCommand(query, cn);
            reader = cmd.ExecuteReader();

            List<SelectListItem> li = new List<SelectListItem>();
            while (reader.Read())
            {
                li.Add(new SelectListItem { Text = reader.GetString(1), Value = reader.GetValue(0).ToString() });

            }

            ViewData["Servicios"] = li;
            cmd.Dispose();
            reader.Close();

            query = "exec getcliente " + Session["UserSession"] + ",'" + id_cliente + "'";
            cmd.Dispose();

            cmd = new OleDbCommand(query, cn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cliente.cod_cliente = reader.GetInt32(0);
                cliente.nombre = reader.GetString(1);
                cliente.direccion = reader.GetString(2);
                cliente.telefono = reader.GetString(3);
                cliente.id_cliente = id_cliente;
                cliente.id_servicio = reader.GetInt32(4);
                cliente.id_empresa = reader.GetInt32(5);

            }






            return View(cliente);

        }


        [Authorize]
        [HttpPost]
        public ActionResult Editcliente(Models.Cliente cliente)
        {

            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();
            string nombre = cliente.nombre;
            string direccion = cliente.direccion;
            string telefono = cliente.telefono;
            int id_servicio = cliente.id_servicio;
            int cod_empresa = cliente.id_empresa;
            string id_cliente = cliente.id_cliente;
            int cod_cliente = cliente.cod_cliente;

            string query = "exec sp_editcliente " + cod_cliente + ",'" + nombre + "','" + direccion + "','" + telefono + "'," + id_servicio + "," + cod_empresa + ",'" + id_cliente + "'";

            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ViewData["Error"] = reader.GetString(0);
            }

            query = "exec getservicios_editclientecombobox " + Session["UserSession"] + ",'" + id_cliente + "'";
            cmd.Dispose();

            cmd = new OleDbCommand(query, cn);
            reader = cmd.ExecuteReader();

            List<SelectListItem> li = new List<SelectListItem>();
            while (reader.Read())
            {
                li.Add(new SelectListItem { Text = reader.GetString(1), Value = reader.GetValue(0).ToString() });

            }

            ViewData["Servicios"] = li;

          return  View(cliente);
            
            
        }

        public FileResult Download(string ImageName)
        {
            return File("~/Content/assets/Templates/" + ImageName,
                System.Net.Mime.MediaTypeNames.Application.Octet);
        }


        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            int flag = 0;
            OleDbConnection cn = new OleDbConnection(cadena);
            cn.Open();
           string query = "select cod_empresa from usuarios where id =" + Session["UserSession"];

            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataReader reader = cmd.ExecuteReader();
            var filename = "";
            var path = "";
            int recibidos = 0;
            int cargados = 0;
            int no_cargados = 0;
            int cod_empresa = 0;

            while (reader.Read())
            {

                cod_empresa = reader.GetInt32(0);

            }

            cmd.Dispose();
            reader.Close();

            cn.Close();
            try
            {
                if(file.ContentLength > 0)
                {

                    

                     filename = System.IO.Path.GetFileName(file.FileName);


                    string comparepath = Server.MapPath("~/Content/assets/archivo_clientes/" + cod_empresa + "/");

                    path = Path.Combine(Server.MapPath("~/Content/assets/archivo_clientes/"+ cod_empresa + "/"), filename);

                    if (!Directory.Exists(comparepath))
                    {
                        Directory.CreateDirectory(comparepath);

                    }

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    string ext = Path.GetExtension(file.FileName);
                    if(String.IsNullOrEmpty( ext ) || !ext.Equals(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        flag = 1;
                        ViewBag.Message = "Archivo no es de extension .csv";
                    }
                    if (flag == 0)
                    {
                        file.SaveAs(path);
                        ViewBag.Message = "Archivo Cargado Exitosamente";
                    }
                }
               
            }
            catch
            {
                ViewBag.Message = "Archivo no pudo ser Cargado";
                return RedirectToAction("Addcliente");
            }

            Models.ClientesList listaClientes = new Models.ClientesList();
            Models.Cliente temp;

            if (flag == 1)
            {
                temp = new Models.Cliente();
                temp.error = "";
                temp.id_cliente = "";
                listaClientes.ListOfClientes.Add(temp);

            }

            else
            {


                var readercsv = new StreamReader(path);
                
                while (!readercsv.EndOfStream)
                {

                    temp = new Models.Cliente();
                    var line = "";
                    if (recibidos == 0)
                    {
                        line = readercsv.ReadLine();
                    }
                    line = readercsv.ReadLine();
                    var values = line.Split(',');



                    string cod_cliente = values[0];
                    string nombre = values[1];
                    string direccion = values[2];
                    string telefono = values[3];
                    string servicio = values[4];

                    int hay_servicio = 0;

                    int id_servicio = 0;

                    temp.id_cliente = cod_cliente;

                    cn.Close();

                    query = "exec getservicio_bynombre " + cod_empresa + ",'" + servicio + "'";
                    cn.Open();
                    cmd = new OleDbCommand(query, cn);
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        hay_servicio = 1;
                    }

                    while (reader.Read())
                    {
                        id_servicio = reader.GetInt32(0);
                    }

                    cmd.Dispose();
                    reader.Close();


                    query = "exec sp_createcliente '" + nombre + "','" + direccion + "','" + telefono + "'," + id_servicio + "," + cod_empresa + ",'" + cod_cliente + "'";



                    cmd = new OleDbCommand(query, cn);


                    try
                    {
                        reader = cmd.ExecuteReader();
                    }
                    catch
                    {
                        if (String.IsNullOrWhiteSpace(nombre) || String.IsNullOrWhiteSpace(direccion) || String.IsNullOrWhiteSpace(telefono) || String.IsNullOrWhiteSpace(id_servicio.ToString()) || String.IsNullOrWhiteSpace(cod_empresa.ToString()) || String.IsNullOrWhiteSpace(cod_cliente))
                            temp.error = "Existen campos en blanco";


                    }
                    string response = "";


                    while (reader.Read())
                    {
                        response = reader.GetString(0);
                    }

                    if (response.Equals("Cliente Ingresado Exitosamente"))
                    {
                        cargados++;
                    }

                    else
                    {

                        if (response.Equals("Codigo cliente ya existe"))
                        {
                            temp.error = "Codigo de cliente ya existe";
                        }

                        if (hay_servicio == 0)
                        {
                            temp.error = "Servicio no existe";
                        }


                    }

                    //MessageBox.Show(values[0].ToString());
                    //  MessageBox.Show(values[2].ToString());
                    recibidos++;

                    no_cargados = recibidos - cargados;
                    listaClientes.ListOfClientes.Add(temp);

                }

            }



            ViewBag.cargados = cargados;
            ViewBag.nocargados = no_cargados;
            ViewBag.recibidos = recibidos;

           




            return View(listaClientes);

        }


       



    }
}
