using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PagosApp.Models
{
    public class User 
    {
        public string Nombre { get; set; }
        public string Nombre_Completo { get; set; }
        public bool Estado { get; set; }
    }
}
