using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Classes;

namespace WebAPI.Entities
{
    public class Estudiante: Persona
    {
        [Key]
        public int EstudianteId { get; set; }

        [Required]
        [StringLength(20)]
        public string Carnet { get; set; }

        [Required] //not null
        public Usuario Usuario { get; set; }
    }
}
