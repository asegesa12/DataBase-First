//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ResourcesFirst.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vacaciones
    {
        public int id_vacaciones { get; set; }
        public Nullable<System.DateTime> desde { get; set; }
        public Nullable<System.DateTime> hasta { get; set; }
        public string año { get; set; }
        public string comentario { get; set; }
        public int id_empleado { get; set; }
    
        public virtual empleados empleados { get; set; }
    }
}
