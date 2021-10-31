using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Contexts;
using WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.Classes;
using System.Data;
using Microsoft.Data.SqlClient;

namespace WebAPI.Controllers
{
    [ApiController]
    //[Route("api/[Controller]")] //api/estudiantes
    [Route("api/estudiantes")] //api/estudiantes
    public class EstudiantesController: ControllerBase
    {
        private readonly AppDBContext context;
        public EstudiantesController(AppDBContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public ActionResult<List<Estudiante>> Get() //action result para retornar 
        {
            return this.context.Estudiante.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Estudiante> Get(int id)
        {
            var estudiante = context.Estudiante.Find(id);

            if (estudiante == null)
                return NotFound();
            
            try
            {
                var idUsuario = from a in context.Estudiante where a.EstudianteId == id select a.Usuario.UsuarioId;
                estudiante.Usuario = new Usuario() { UsuarioId = idUsuario.FirstOrDefault() };
            }
            catch
            {
                //si no existe el id, se envia el objeto de catedratico sin informacion de usuario
            }

            return estudiante;          
        }

        [HttpGet("{id}/cursos")]
        public ActionResult<List<Curso>> GetCursos(int id)
        {

            List<Curso> cursos = new List<Curso>() { };
            //buscamos el estudiante en la base de datos
            var estudiante = context.Estudiante.Find(id);

            if (estudiante == null) //si no existe
                return NotFound();

            try
            {
                //buscamos los cursos asignados a ese estudiante
                var estudianteCursos = from a in context.EstudianteCurso where a.Estudiante.EstudianteId == estudiante.EstudianteId select a;
                
                if(estudianteCursos != null) //si hay cursos
                {
                    //por cada curso asignado encontrado en esa tabla
                    foreach(EstudianteCurso estudianteCurso in estudianteCursos)
                    {
                        //si tiene informacion
                        if(estudianteCurso != null)
                        {
                            //si su id es mayor a 0
                            if(estudianteCurso.EstudianteCursoId > 0)
                            {
                                var idCurso = context.EstudianteCurso.Find(estudianteCurso.EstudianteCursoId);

                                try
                                {
                                    if(idCurso.CursoId > 0)
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

        [HttpPost]
        public ActionResult Post(Estudiante estudiante)
        {
            if (Utils.TieneDatos(null, null, null, null, null, null, estudiante))
            {
                context.Estudiante.Add(estudiante);
                context.SaveChanges();
                return NoContent(); //para mandar codigo 200 o segun la respuesta
                //Usuario usuarioActual = context.Usuario.Find(estudiante.Usuario.UsuarioId);

                //if (Utils.TieneDatos(usuarioActual))
                //{
                //    //estudiante.Usuario = usuarioActual;
                //    context.Estudiante.Add(estudiante);
                //    context.SaveChanges();
                //    return NoContent(); //para mandar codigo 200 o segun la respuesta
                //}
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Estudiante estudiante)
        {
            if (Utils.TieneDatos(null, null, null, null, null, null, estudiante))
            {
                Usuario usuarioActual = context.Usuario.Find(estudiante.Usuario.UsuarioId);

                if (id == estudiante.EstudianteId)
                {
                    if (Utils.TieneDatos(usuarioActual))
                    {
                        estudiante.Usuario = usuarioActual;
                        context.Entry(estudiante).State = EntityState.Modified;
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
            var estudiante = context.Estudiante.Find(id);
            if (estudiante == null)
                return NotFound();

            context.Estudiante.Remove(estudiante);
            context.SaveChanges();
            return NoContent();
        }
    }
}
