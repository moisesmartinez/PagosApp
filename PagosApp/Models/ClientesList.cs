using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagosApp.Models
{
    public class ClientesList
    {
        public List<Models.Cliente> ListOfClientes { get; set; }

        public ClientesList()
        {
            ListOfClientes = new List<Cliente>();
        }
    }
}