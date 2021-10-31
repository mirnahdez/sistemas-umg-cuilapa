using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class FacultadCentroU
    {
        [Key]
        public int FacultadCentroUId { get; set; }

        [Required]
        public Facultad Facultad { get; set; }

        [Required]
        public CentroU CentroUniversitario { get; set; }
    }
}
