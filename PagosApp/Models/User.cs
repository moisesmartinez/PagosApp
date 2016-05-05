﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PagosApp.Models
{
    public class User 
    {
        public string Nombre { get; set; }
        public string Usuario { get; set; }

        public string rol { get; set; }
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Password Required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
