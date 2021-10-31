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
    //[Route("api/[Controller]")] //api/asignacioncursoscatedratico
    [Route("api/asignacioncursoscatedratico")] //api/asignacioncursoscatedratico
    public class AsignacionCursoCatedraticoController : ControllerBase
    {
        private readonly AppDBContext context;
        public AsignacionCursoCatedraticoController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult Post(CatedraticoCurso catedraticoCurso)
        {
            if (Utils.TieneDatos(null, null, null, catedraticoCurso))
            {
                Curso cursoActual = context.Curso.Find(catedraticoCurso.Curso.CursoId);
                Catedratico catedraticoActual = context.Catedratico.Find(catedraticoCurso.Catedratico.CatedraticoId);

                if(Utils.TieneDatos(null, null, catedraticoActual, null, cursoActual))
                {
                    catedraticoCurso.Catedratico = catedraticoActual;
                    catedraticoCurso.Curso = cursoActual;
                    context.CatedraticoCurso.Add(catedraticoCurso);
                    context.SaveChanges();
                    return NoContent(); //para mandar codigo 200 o segun la respuesta
                }
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, CatedraticoCurso catedraticoCurso)
        {
            if (Utils.TieneDatos(null, null, null, catedraticoCurso))
            {
                Curso cursoActual = context.Curso.Find(catedraticoCurso.Curso.CursoId);
                Catedratico catedraticoActual = context.Catedratico.Find(catedraticoCurso.Catedratico.CatedraticoId);

                if (Utils.TieneDatos(null, null, catedraticoActual, null, cursoActual))
                {
                    if (id != catedraticoCurso.CatedraticoCursoId)
                        return BadRequest();

                    catedraticoCurso.Catedratico = catedraticoActual;
                    catedraticoCurso.Curso = cursoActual;
                    context.Entry(catedraticoCurso).State = EntityState.Modified;
                    context.SaveChanges();
                    return NoContent(); //para mandar codigo 200 o segun la respuesta
                }
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var asignacion = context.CatedraticoCurso.Find(id);
            if (asignacion == null)
                return NotFound();

            context.CatedraticoCurso.Remove(asignacion);
            context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<CatedraticoCurso>> Get() //action result para retornar 
        {
            return this.context.CatedraticoCurso.ToList();
        }

    }
}