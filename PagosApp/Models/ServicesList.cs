using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PagosApp.Models
{
    public class ServicesList
    {
        public List<Models.Service> ListOfServices { get; set; }

        public ServicesList()
        {
            ListOfServices = new List<Service>();
        }
    }
}