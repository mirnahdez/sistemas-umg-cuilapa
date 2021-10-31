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
    [Route("api/Facultad")] //api/asignacioncursoscatedratico
    public class FacultadesController : ControllerBase
    {
        private readonly AppDBContext context;
        public FacultadesController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<List<Facultad>> Get() //action result para retornar 
        {
            return this.context.Facultad.ToList();
        }

    }
}