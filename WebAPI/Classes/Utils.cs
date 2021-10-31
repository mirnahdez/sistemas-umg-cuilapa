using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Classes
{
    public static class Utils
    {
        public static bool TieneDatos(Usuario usuario = null, Publicacion publicacion = null, Catedratico catedratico = null,
            CatedraticoCurso catedraticoCurso = null, Curso curso = null, EstudianteCurso estudianteCurso = null, 
            Estudiante estudiante = null, Imagen imagen = null, Enlace enlace = null)
        {
            bool tieneDatos = false;

            try
            {
                if (usuario != null)
                    if (!string.IsNullOrEmpty(usuario.CorreoElectronico) &&
                        !string.IsNullOrEmpty(usuario.Password))
                        tieneDatos = true;

                if (publicacion != null)
                    if (!string.IsNullOrEmpty(publicacion.Titulo))
                        if (publicacion.Usuario != null)
                            if (publicacion.Usuario.UsuarioId > 0)
                                tieneDatos = true;

                if (catedratico != null)
                    if (!string.IsNullOrEmpty(catedratico.Carnet))
                        tieneDatos = true;

                if (catedraticoCurso != null)
                    if (catedraticoCurso.Catedratico != null && catedraticoCurso.Curso != null)
                        if (catedraticoCurso.Catedratico.CatedraticoId != 0 && catedraticoCurso.Curso.CursoId != 0)
                            tieneDatos = true;

                if (curso != null)
                    if (!string.IsNullOrEmpty(curso.Nombre))
                        tieneDatos = true;

                if (estudianteCurso != null)
                    if (estudianteCurso.Curso != null && estudianteCurso.Estudiante != null)
                        if (!string.IsNullOrEmpty(estudianteCurso.Curso.Nombre) && !string.IsNullOrEmpty(estudianteCurso.Estudiante.Nombre))
                            tieneDatos = true;

                if (estudiante != null)
                    if (!string.IsNullOrEmpty(estudiante.Nombre))
                        tieneDatos = true;

                if (imagen != null)
                    if (!string.IsNullOrEmpty(imagen.Src))
                        tieneDatos = true;

                if (enlace != null)
                    if (!string.IsNullOrEmpty(enlace.URL))
                        tieneDatos = true;
            }
            catch
            {
                throw;
            }

            return tieneDatos;
        }
    }
}
