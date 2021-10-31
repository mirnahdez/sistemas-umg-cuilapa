using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(100)]
        public string CorreoElectronico { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        [Required]
        public int TipoUsuario { get; set; }
    }
}
