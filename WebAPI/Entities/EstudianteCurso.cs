using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class EstudianteCurso
    {
        [Key]
        public int EstudianteCursoId { get; set; }

        [Required]
        public Estudiante Estudiante { get; set; }

        [Required]
        public Curso Curso { get; set; }
        public int CursoId { get; set; }
    }
}
