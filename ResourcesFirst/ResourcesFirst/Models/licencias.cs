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
    using System.ComponentModel.DataAnnotations;

    public partial class licencias
    {
        [Required]
        public int id_licencia { get; set; }
        [Required]
        public Nullable<System.DateTime> desde { get; set; }
        [Required]
        public Nullable<System.DateTime> hasta { get; set; }
        [Required]
        public string motivo { get; set; }
        public string comentarios { get; set; }
        public int id_empleado { get; set; }
    
        public virtual empleados empleados { get; set; }
    }
}
