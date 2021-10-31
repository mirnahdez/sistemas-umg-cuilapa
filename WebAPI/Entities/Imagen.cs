using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Imagen
    {
        [Key]
        public int ImagenId { get; set; }

        [StringLength(200)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(250)]
        public string Src { get; set; }
        public int PublicacionId { get; set; }
    }
}
