using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Contexts
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        //que este nombre tenga una representacion en base de datos, 
        //tomando de referencia el modelo correspondiente
        public DbSet<Facultad> Facultad { get; set; }
        public DbSet<CentroU> CentroU { get; set; }
        public DbSet<FacultadCentroU> FacultadCentroU { get; set; }
        public DbSet<Carrera> Carrera { get; set; }
        public DbSet<CentroUCarrera> CentroUCarrera { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<EstudianteCurso> EstudianteCurso { get; set; }
        public DbSet<Catedratico> Catedratico { get; set; }
        public DbSet<CatedraticoCurso> CatedraticoCurso { get; set; }
        public DbSet<Auxiliar> Auxiliar { get; set; }
        public DbSet<GestorContenido> GestorContenido { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Enlace> Enlace { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<Publicacion> Publicacion { get; set; }
    }
}
