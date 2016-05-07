using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace PagosApp.Models
{
    public class User2
    {
        [Required(ErrorMessage = "Nombre Requerido", AllowEmptyStrings = false)]
        [DisplayName("Nombre: ")]
        
        public string Nombre { get; set; }
        [DisplayName("Usuario: ")]
        [Required(ErrorMessage = "Usuario Requerido", AllowEmptyStrings = false)]
        public string Usuario { get; set; }

        public string rol { get; set; }

        public int id_rol { get; set; }
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Contrasena Requerida", AllowEmptyStrings = false)]
        [DisplayName("Password")]
        public string Password { get; set; }

        public int id_empresa { get; set; }


        [Required(ErrorMessage = "Confirmar Cotrasena Requerida", AllowEmptyStrings = false)]
        [Compare("Password", ErrorMessage = "Passwords deben de coincidir")]
        [DisplayName("ConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }
}
