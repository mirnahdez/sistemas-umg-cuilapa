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
    //[Route("api/[Controller]")] //api/asignacioncursosestudiante
    [Route("api/asignacioncursosestudiante")] //api/asignacioncursosestudiante
    public class AsignacionCursoEstudianteController : ControllerBase
    {
        private readonly AppDBContext context;
        public AsignacionCursoEstudianteController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult Post(EstudianteCurso estudianteCurso)
        {
            if (Utils.TieneDatos(null, null, null, null, null, estudianteCurso))
            {
                Curso cursoActual = context.Curso.Find(estudianteCurso.Curso.CursoId);
                Estudiante estudianteActual = context.Estudiante.Find(estudianteCurso.Estudiante.EstudianteId);

                if (Utils.TieneDatos(null, null, null, null, cursoActual, null, estudianteActual))
                {
                    estudianteCurso.Estudiante = estudianteActual;
                    estudianteCurso.Curso = cursoActual;
                    context.EstudianteCurso.Add(estudianteCurso);
                    context.SaveChanges();
                    return NoContent(); //para mandar codigo 200 o segun la respuesta
                }
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, EstudianteCurso estudianteCurso)
        {
            if (Utils.TieneDatos(null, null, null, null, null, estudianteCurso))
            {
                Curso cursoActual = context.Curso.Find(estudianteCurso.Curso.CursoId);
                Estudiante estudianteActual = context.Estudiante.Find(estudianteCurso.Estudiante.EstudianteId);

                if (Utils.TieneDatos(null, null, null, null, cursoActual, null, estudianteActual))
                {
                    if (id != estudianteCurso.EstudianteCursoId)
                        return BadRequest();

                    estudianteCurso.Estudiante = estudianteActual;
                    estudianteCurso.Curso = cursoActual;
                    context.Entry(estudianteCurso).State = EntityState.Modified;
                    context.SaveChanges();
                    return NoContent(); //para mandar codigo 200 o segun la respuesta
                }
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var asignacion = context.EstudianteCurso.Find(id);
            if (asignacion == null)
                return NotFound();

            context.EstudianteCurso.Remove(asignacion);
            context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<EstudianteCurso>> Get() //action result para retornar 
        {
            return this.context.EstudianteCurso.ToList();
        }
    }
}