using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class Enlace
    {
        [Key]
        public int EnlaceId { get; set; }

        [Required]
        [StringLength(250)]
        public string URL { get; set; }
        public int PublicacionId { get; set; }
    }
}
