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
        [DisplayName("Nombre: ")]
        public string Nombre { get; set; }
        [DisplayName("Usuario: ")]
        public string Usuario { get; set; }

        public string rol { get; set; }

        public int id_rol { get; set; }
        public bool Estado { get; set; }

       
        [DisplayName("Password")]
        [StringLength(10, ErrorMessage = "Debe contener al menos 5 caracteres", MinimumLength = 5)]
        public string Password { get; set; }



        [Compare("Password", ErrorMessage = "Passwords deben de coincidir")]
        [DisplayName("ConfirmPassword")]
        public string ConfirmPassword { get; set; }
        public int id_empresa { get; set; }
    }
}
