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
    [Route("api/carrera")] //api/asignacioncursoscatedratico
    public class CarrerasController : ControllerBase
    {
        private readonly AppDBContext context;
        public CarrerasController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<List<Carrera>> Get() //action result para retornar 
        {
            return this.context.Carrera.Include(x => x.Facultad).ToList();
        }

    }
}