using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Publicacion
    {
        [Key]
        public int PublicacionId { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public List<Enlace> Enlaces { get; set; }
        public List<Imagen> Imagenes { get; set; }

        [Required]
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
    }
}
