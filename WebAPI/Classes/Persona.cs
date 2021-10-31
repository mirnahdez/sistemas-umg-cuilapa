using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Classes
{
    public class Persona
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(300)]
        public string Direccion { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [StringLength(50)]
        public string Telefono { get; set; }
    }
}
