using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace PagosApp.Models
{
    public class Service
    {
        public int id_servicio { get; set; }
        public string Descripcion { get; set; }
        public string SubmitButton { get; set; }

        public string empresa { get; set; }

        public Service()
        {
            id_servicio = -1;
            Descripcion = "";
            empresa = "";
        }
    }
}