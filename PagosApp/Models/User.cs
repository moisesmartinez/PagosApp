using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace PagosApp.Models
{
    public class User 
    {
        [Required(ErrorMessage = "Nombre Requerido", AllowEmptyStrings = false)]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }
        public string Usuario { get; set; }

        public string rol { get; set; }

        public int id_rol { get; set; }
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Contrasena requerida", AllowEmptyStrings = false)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage = "Passwords deben de coincidir")]
        [Required(ErrorMessage = "Contrasena requerida", AllowEmptyStrings = false)]
        [DisplayName("ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
