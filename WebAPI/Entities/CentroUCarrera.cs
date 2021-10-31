using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class CentroUCarrera
    {
        [Key]
        public int CentroUCarreraId { get; set; }

        [Required]
        public CentroU CentroUniversitario { get; set; }

        [Required]
        public Carrera Carrera { get; set; }
    }
}
