//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace PagosApp
{
    public partial class Role
    {
        public Role()
        {
            this.RolesUsuarios = new HashSet<RolesUsuario>();
        }
    
        public int id_rol { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
    
        public virtual ICollection<RolesUsuario> RolesUsuarios { get; set; }
    }
    
}
