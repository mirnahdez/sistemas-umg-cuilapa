using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Classes;
using WebAPI.Contexts;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/validacionusuarios")] //api/validacionusuarios
    public class ValidarUsuariosController: ControllerBase
    {
        private readonly AppDBContext context;
        public ValidarUsuariosController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult<Usuario> Post(Usuario usuario)
        {
            try
            {
                //obtener los usuarios filtrados por tipo de usuario 
                IEnumerable<Usuario> usuariosPorTipoUsuario = from a in context.Usuario where a.TipoUsuario == usuario.TipoUsuario select a;

                if(usuariosPorTipoUsuario != null)
                {
                    //buscar si existe el correo en alguno de ellos
                    IEnumerable<Usuario> usuariosCorreoCoincide = from a in usuariosPorTipoUsuario where a.CorreoElectronico == usuario.CorreoElectronico select a;

                    //comparar la contrasenia con la proporcionada, de lo contrario enviar not found
                    if (usuariosCorreoCoincide != null)
                        if(usuariosCorreoCoincide.Count() > 0)
                        {
                            Usuario usuarioEncontrado = usuariosCorreoCoincide.FirstOrDefault();
                            if (usuarioEncontrado.Password == usuario.Password)
                            {
                                return usuarioEncontrado;
                                //return NoContent();
                            }
                        }
                }
            }
            catch { }
            
            return NotFound();
        }

        [HttpGet("catedratico/{id}")]
        public ActionResult<Catedratico> InformacionCatedratico(int id)
        {
            var catedratico = from a in context.Catedratico where a.UsuarioId == id select a;

            if (catedratico == null)
                return NotFound();

            try
            {
                return catedratico.FirstOrDefault();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
