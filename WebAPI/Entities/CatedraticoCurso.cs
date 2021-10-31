using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class CatedraticoCurso
    {
        [Key]
        public int CatedraticoCursoId { get; set; }

        [Required]
        public Curso Curso { get; set; }

        [Required]
        public Catedratico Catedratico { get; set; }
        public int CursoId { get; set; }
    }
}
