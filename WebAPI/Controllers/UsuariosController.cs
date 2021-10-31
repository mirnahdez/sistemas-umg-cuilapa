using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Contexts;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDBContext context;
        public UsuariosController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> Get() //action result para retornar 
        {
            return this.context.Usuario.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {
            var usuario = context.Usuario.Find(id);

            if (usuario == null)
                return NotFound();

            return usuario;
        }

        [HttpPost]
        public ActionResult Post(Usuario usuario)
        {
            context.Usuario.Add(usuario);
            context.SaveChanges();
            return NoContent(); //para mandar codigo 200 o segun la respuesta
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
                return BadRequest();

            context.Entry(usuario).State = EntityState.Modified;
            context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var usuario = context.Usuario.Find(id);
            if (usuario == null)
                return NotFound();

            context.Usuario.Remove(usuario);
            context.SaveChanges();
            return NoContent();
        }
    }
}