using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Contexts;
using WebAPI.Entities;
using WebAPI.Classes;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/publicaciones")]
    public class PublicacionesController: ControllerBase
    {
        private readonly AppDBContext context;
        public PublicacionesController(AppDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<List<Publicacion>> Get() //action result para retornar 
        {
            List<Publicacion> publicaciones = new List<Publicacion>() { };
            try
            {
                //listado de todas las publicaciones
                List<Publicacion> publicacionesTemp = context.Publicacion.ToList();
                if(publicacionesTemp != null)
                {
                    //si existen publicaciones
                    if(publicacionesTemp.Count > 0)
                    {
                        //por cada una
                        foreach(Publicacion publicacion in publicacionesTemp)
                        {
                            //inicializamos la publicacion temporal con los datos de la publicacion actual
                            Publicacion publicacionTemp = publicacion;
                            List<Imagen> imagenesPublicacion = new List<Imagen>() { };
                            List<Enlace> enlacesPublicacion = new List<Enlace>() { };
                            
                            //obtenemos el listado de imagenes y enlaces
                            IEnumerable<Imagen> imagenes = from a in context.Imagen where a.PublicacionId == publicacion.PublicacionId select a;
                            IEnumerable<Enlace> enlaces = from a in context.Enlace where a.PublicacionId == publicacion.PublicacionId select a;

                            if (imagenes != null)
                            {
                                foreach (Imagen imagen in imagenes)
                                {
                                    if (Utils.TieneDatos(null, null, null, null, null, null, null, imagen))
                                        imagenesPublicacion.Add(imagen);
                                }
                            }

                            if (enlaces != null)
                            {
                                foreach (Enlace enlace in enlaces)
                                {
                                    if (Utils.TieneDatos(null, null, null, null, null, null, null, null, enlace))
                                        enlacesPublicacion.Add(enlace);
                                }
                            }

                            publicacionTemp.Imagenes = imagenesPublicacion;
                            publicacionTemp.Enlaces = enlacesPublicacion;
                            publicaciones.Add(publicacionTemp);
                        }
                    }
                }
            }
            catch { }
            return publicaciones;
        }

        [HttpGet("{id}")]
        public ActionResult<Publicacion> Get(int id)
        {
            var publicacion = context.Publicacion.Find(id);

            if (publicacion == null)
                return NotFound();

            try
            {
                List<Imagen> imagenesPublicacion = new List<Imagen>() { };
                List<Enlace> enlacesPublicacion = new List<Enlace>() { };
                IEnumerable<Imagen> imagenes = from a in context.Imagen where a.PublicacionId == publicacion.PublicacionId select a;
                IEnumerable<Enlace> enlaces = from a in context.Enlace where a.PublicacionId == publicacion.PublicacionId select a;

                if (imagenes != null)
                {
                    foreach (Imagen imagen in imagenes)
                    {
                        if (Utils.TieneDatos(null, null, null, null, null, null, null, imagen))
                            imagenesPublicacion.Add(imagen);
                    }
                }

                if (enlaces != null)
                {
                    foreach (Enlace enlace in enlaces)
                    {
                        if (Utils.TieneDatos(null, null, null, null, null, null, null, null, enlace))
                            enlacesPublicacion.Add(enlace);
                    }
                }

                publicacion.Imagenes = imagenesPublicacion;
                publicacion.Enlaces = enlacesPublicacion;
            }
            catch { }

            return publicacion;
        }

        [HttpPost]
        public ActionResult Post(Publicacion publicacion)
        {
            if (Utils.TieneDatos(null, publicacion))
            {
                Usuario usuarioActual = context.Usuario.Find(publicacion.Usuario.UsuarioId);

                if (Utils.TieneDatos(usuarioActual))
                {
                    publicacion.Usuario = usuarioActual;
                    context.Publicacion.Add(publicacion);
                    context.SaveChanges();
                    return NoContent(); //para mandar codigo 200 o segun la respuesta
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Publicacion publicacion)
        {
            if (Utils.TieneDatos(null, publicacion))
            {
                if (id == publicacion.PublicacionId)
                {
                    Usuario usuarioActual = context.Usuario.Find(publicacion.Usuario.UsuarioId);

                    if (Utils.TieneDatos(usuarioActual))
                    {
                        publicacion.Usuario = usuarioActual;
                        context.Entry(publicacion).State = EntityState.Modified;
                        context.SaveChanges();

                        if (publicacion.Imagenes.Count > 0)
                            foreach (Imagen imagen in publicacion.Imagenes)
                            {
                                if (imagen.ImagenId > 0)
                                {
                                    try
                                    {
                                        context.Entry(imagen).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }
                                    catch { }
                                }
                                else
                                {
                                    if(Utils.TieneDatos(null, null, null, null, null, null, null, imagen))
                                    {
                                        imagen.PublicacionId = publicacion.PublicacionId;
                                        context.Imagen.Add(imagen);
                                        context.SaveChanges();
                                    }
                                }
                            }

                        if (publicacion.Enlaces.Count > 0)
                            foreach (Enlace enlace in publicacion.Enlaces)
                            {
                                if(enlace.EnlaceId > 0)
                                {
                                    try
                                    {
                                        context.Entry(enlace).State = EntityState.Modified;
                                        context.SaveChanges();
                                    }
                                    catch { }
                                }
                            }

                        return NoContent(); //para mandar codigo 200 o segun la respuesta
                    }
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var publicacion = context.Publicacion.Find(id);
            if (publicacion == null)
                return NotFound();

            context.Publicacion.Remove(publicacion);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("imagenes/{id}")]
        public ActionResult DeleteImagen(int id)
        {
            var imagen = context.Imagen.Find(id);
            if (imagen == null)
                return NotFound();

            context.Imagen.Remove(imagen);
            context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("enlaces/{id}")]
        public ActionResult DeleteEnlace(int id)
        {
            var enlace = context.Enlace.Find(id);
            if (enlace == null)
                return NotFound();

            context.Enlace.Remove(enlace);
            context.SaveChanges();
            return NoContent();
        }
    }
}
