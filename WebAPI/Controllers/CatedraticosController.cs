using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Contexts;
using WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.Classes;

namespace WebAPI.Controllers
{
    [ApiController]
    //[Route("api/[Controller]")] //api/catedraticos
    [Route("api/catedraticos")] //api/catedraticos
    public class CatedraticosController : ControllerBase
    {
        private readonly AppDBContext context;
        public CatedraticosController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult Post(Catedratico catedratico)
        {
            if(Utils.TieneDatos(null, null, catedratico))
            {
                //catedratico.Usuario = usuarioActual;
                context.Catedratico.Add(catedratico);
                context.SaveChanges();
                return NoContent(); //para mandar codigo 200 o segun la respuesta

                //Usuario usuarioActual = context.Usuario.Find(catedratico.Usuario.UsuarioId);
                //if (Utils.TieneDatos(usuarioActual))
                //{
                //}
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Catedratico catedratico)
        {
            if (Utils.TieneDatos(null, null, catedratico))
            {
                Usuario usuarioActual = context.Usuario.Find(catedratico.Usuario.UsuarioId);

                if (id == catedratico.CatedraticoId)
                {
                    if (Utils.TieneDatos(usuarioActual))
                    {
                        catedratico.Usuario = usuarioActual;
                        context.Entry(catedratico).State = EntityState.Modified;
                        context.SaveChanges();
                        return NoContent();
                    }
                }
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var catedratico = context.Catedratico.Find(id);
            if (catedratico == null)
                return NotFound();

            context.Catedratico.Remove(catedratico);
            context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<Catedratico>> Get() //action result para retornar 
        {
            return this.context.Catedratico.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Catedratico> Get(int id)
        {
            var catedratico = context.Catedratico.Find(id);

            if (catedratico == null)
                return NotFound();

            try
            {
                var idUsuario = from a in context.Catedratico where a.CatedraticoId == id select a.Usuario.UsuarioId;
                catedratico.Usuario = new Usuario() { UsuarioId = idUsuario.FirstOrDefault() };
            }
            catch
            {
                //si no existe el id, se envia el objeto de catedratico sin informacion de usuario
            }

            return catedratico;
        }

        [HttpGet("{id}/cursos")]
        public ActionResult<List<Curso>> GetCursos(int id)
        {

            List<Curso> cursos = new List<Curso>() { };
            //buscamos el estudiante en la base de datos
            var catedratico = context.Catedratico.Find(id);

            if (catedratico == null) //si no existe
                return NotFound();

            try
            {
                //buscamos los cursos asignados a ese estudiante
                var catedraticoCursos = from a in context.CatedraticoCurso where a.Catedratico.CatedraticoId == catedratico.CatedraticoId select a;

                if (catedraticoCursos != null) //si hay cursos
                {
                    //por cada curso asignado encontrado en esa tabla
                    foreach (CatedraticoCurso catedraticoCurso in catedraticoCursos)
                    {
                        //si tiene informacion
                        if (catedraticoCurso != null)
                        {
                            //si su id es mayor a 0
                            if (catedraticoCurso.CatedraticoCursoId > 0)
                            {
                                var idCurso = context.CatedraticoCurso.Find(catedraticoCurso.CatedraticoCursoId);

                                try
                                {
                                    if (idCurso.CursoId > 0)
                                    {
                                        var curso = context.Curso.Find(idCurso.CursoId);

                                        if (Utils.TieneDatos(null, null, null, null, curso))
                                            cursos.Add(curso);
                                    }

                                }
                                catch
                                {
                                    return NotFound();
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                //si no existe el id, se envia el objeto de catedratico sin informacion de usuario
            }

            return cursos;
        }
    }
}