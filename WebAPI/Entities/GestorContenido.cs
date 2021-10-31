using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Classes;

namespace WebAPI.Entities
{
    public class GestorContenido: Persona
    {
        [Key]
        public int GestorContenidoId { get; set; }

        [Required]
        [StringLength(20)]
        public string Carnet { get; set; }

        [Required]
        public Facultad Facultad { get; set; }

        [Required]
        public Usuario Usuario { get; set; }
    }
}
