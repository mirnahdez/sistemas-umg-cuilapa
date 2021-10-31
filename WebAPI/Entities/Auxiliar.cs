using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Classes;

namespace WebAPI.Entities
{
    public class Auxiliar: Persona
    {
        [Key]
        public int AuxiliarId { get; set; }

        [Required]
        [StringLength(20)]
        public string Carnet { get; set; }

        [Required]
        public CentroU CentroUniversitario { get; set; }

        [Required]
        public Usuario Usuario { get; set; }
    }
}
