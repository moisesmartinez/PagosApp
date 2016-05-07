using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace PagosApp.Models
{

    public class Rol
    {
        [Required(ErrorMessage = "Descripcion Requerida", AllowEmptyStrings = false)]
        public string Descripcion_Rol { get; set; }

        public int id_rol { get; set;}

        public bool deleted { get; set; }

        public string idstring { get; set; }
    }





}