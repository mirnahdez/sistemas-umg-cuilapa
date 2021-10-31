using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }
        [Required]
        public Carrera Carrera { get; set; }
        public int CarreraId { get; set; }
    }
}
