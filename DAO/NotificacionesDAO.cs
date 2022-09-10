using AutoMapper;
using Helpers.InfoMensajes;
using Model.Anuncios;
using Model.ConfiguracionPlataforma;
using Model.Usuarios;
using Model.ViewModel;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAO
{
    public class NotificacionesDAO
    {
        ApplicationDbContext db;
        Controller controller;
        private const string entidad = "Notificacion";
        AnunciosDAO anunciosDAO;
        private ImageHelper imageHelper;
        private CategoriasDAO categoriasDAO;

        public NotificacionesDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            this.controller = controller;
            anunciosDAO = new AnunciosDAO(controller);
            imageHelper = new ImageHelper();
            categoriasDAO = new CategoriasDAO(controller);
        }
            

        public List<NotificacionDTO> getList(string user)
        {
            try
            {

                Usuarios usuario = db.Usuarios.Include(x=> x.notificaciones).FirstOrDefault(x => x.correo.Equals(user));
                List<Notificacion> notificacionsModel = usuario.notificaciones;
                List<NotificacionDTO> responseList = new List<NotificacionDTO>();
                
                //Mapeo de clase
                notificacionsModel.ForEach(x =>
                {
                    NotificacionDTO response = new NotificacionDTO();
                    response.notificacion = x.notificacion;
                    response.anuncio = anunciosDAO.getById(x.anuncio.id);
                    response.id = x.id;
                    responseList.Add(response);
                });
                return responseList;         

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(NotificacionDTO notificacion)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<NotificacionDTO, Notificacion>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase

                Notificacion response = mapper.Map<NotificacionDTO, Notificacion>(notificacion);
                db.Notificaciones.Add(response);
                db.SaveChanges();
                
            }

            catch (Exception)
            {

                throw;
            }
        }

        public NotificacionDTO Find(int? id)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Notificacion, NotificacionDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                Notificacion model = db.Notificaciones.Find(id);
                NotificacionDTO response = mapper.Map<Notificacion, NotificacionDTO>(model);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotificacionDTO Update(NotificacionDTO notificacions)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<NotificacionDTO, Notificacion>();
                });
                IMapper mapper = config.CreateMapper();
                Notificacion notificacionsModel = mapper.Map<NotificacionDTO, Notificacion>(notificacions);

                db.Entry(notificacionsModel).State = EntityState.Modified;
                db.SaveChanges();

                notificacions = this.Find(notificacions.id);

                
                return notificacions;

            }
            catch (Exception)
            {
                
                return notificacions;
            }
        }

        public void Remove(int id)
        {
            try
            {
                Notificacion razonsocial = db.Notificaciones.Find(id);
                razonsocial.estado = false;
                db.Entry(razonsocial).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            catch (Exception)
            {
                
            }
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        public void crearNotificaciones(string user)
        {
            try
            {
                var userModel = FindUser(user);
                var today = DateTime.Today;
                NotificacionDTO notificacion = new NotificacionDTO();
                Notificacion respone = new Notificacion();
                userModel.anuncios.ForEach(x =>
                {
                    TimeSpan fechaResultado = (x.fechaCancelacion- today);
                    int totalDays =(int) fechaResultado.TotalDays;

                    if (totalDays == 3)
                    {
                        notificacion.notificacion = Helpers.Constants.Anuncios.notif3dias;
                        notificacion.anuncio = x;
                        notificacion.fecha = DateTime.Today;
                        notificacion.check = false;
                    }
                    else if (totalDays == 5)
                    {
                        notificacion.notificacion = Helpers.Constants.Anuncios.notif5dias;
                        notificacion.anuncio = x;
                        notificacion.fecha = DateTime.Today;
                        notificacion.check = false;
                    }
                    else if (totalDays <= 0)
                    {
                        notificacion.notificacion = Helpers.Constants.Anuncios.notif0dias; 
                        notificacion.anuncio = x;
                        notificacion.fecha = x.fechaCancelacion;
                        notificacion.check = false;

                    }
                    bool exists=false;
                    var notificaciones = getList(user);
                    if (!string.IsNullOrEmpty(notificacion.notificacion))
                    {
                        notificaciones.ForEach(y =>
                        {
                            if (notificacion.anuncio.id == y.anuncio.id && notificacion.notificacion.Equals(y.notificacion))
                            {
                                exists = true;
                            }
                        });

                        if (!exists)
                        {
                            Notificacion model = new Notificacion();
                            model.notificacion = notificacion.notificacion;
                            model.fecha = x.fechaCancelacion;
                            model.check=false;
                            model.anuncio = db.Anuncios.First(z => z.id == notificacion.anuncio.id);
                            Usuarios usuario = db.Usuarios.FirstOrDefault(w => w.correo.Equals(user));
                            usuario.notificaciones.Add(model);
                            db.Entry(usuario).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public UsuariosDTO FindUser(string user)
        {
            try
            {
                Usuarios model = db.Usuarios.Include(x=> x.anuncios).Include(x=>x.rol).Include(x=> x.notificaciones).FirstOrDefault(x => x.correo.Equals(user));
                UsuariosDTO response = new UsuariosDTO();
                response.id = model.id;
                response.nombre = model.nombre;
                response.apellido = model.apellido;
                response.ciudad = model.ciudad;
                response.correo = model.correo;
                response.password = model.password;
                response.anuncios = MapperAnuncios(model.anuncios);
                response.notificaciones = getList(user);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void checkNotificaciones(string user)
        {
            try
            {
                Usuarios usuario = db.Usuarios.Include(x=> x.notificaciones).FirstOrDefault(w => w.correo.Equals(user));
                var userModel = FindUser(user);
                NotificacionDTO notificacion = new NotificacionDTO();
                Notificacion respone = new Notificacion();
                usuario.notificaciones.ForEach(x =>
                {
                    x.check = true;
                });
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NotificacionDTO> verificarNotificaciones(string user)
        {

            Usuarios model = db.Usuarios.Include(x => x.notificaciones).FirstOrDefault(x => x.correo.Equals(user));
            List<NotificacionDTO> response = new List<NotificacionDTO>();
           
            model.notificaciones.ForEach(x =>
            {
                NotificacionDTO notificacionModel = new NotificacionDTO();
                notificacionModel.notificacion = x.notificacion;
                notificacionModel.id = x.id;
                notificacionModel.anuncio = anunciosDAO.getById(x.anuncio.id);
                notificacionModel.fecha = x.fecha;
                notificacionModel.check = x.check;
                response.Add(notificacionModel);
            });
            return response;
        }



        public List<AnuncioDTO> MapperAnuncios(List<Anuncio> anuncio)
        {
            try
            {

                //Mapeo de clase
                List<AnuncioDTO> anuncios = new List<AnuncioDTO>();
                if (anuncio != null)
                {
                    foreach (var item in anuncio)
                    {
                        AnuncioDTO anuncioModel = new AnuncioDTO();
                        anuncioModel.titulo = item.titulo;
                        anuncioModel.nombreContacto = item.nombreContacto;
                        anuncioModel.telefono = item.telefono;
                        anuncioModel.actCatalogo = item.actCatalogo;
                        anuncioModel.actImagen = item.actImagen;
                        anuncioModel.celularContacto = item.celularContacto;
                        anuncioModel.descripcion = item.descripcion;
                        anuncioModel.id = item.id;
                        anuncioModel.path = item.imagen != null ? imageHelper.GetImageFromByteArray(item.imagen) : null;
                        anuncioModel.fechaActivacion = item.fechaActivacion;
                        anuncioModel.fechaCancelacion = item.fechaCancelacion;
                        anuncioModel.categoria = categoriasDAO.Find(item.categoriaId);
                        anuncioModel.categoriaId = item.categoriaId;
                        anuncioModel.localidadId = item.localidadId;
                        anuncios.Add(anuncioModel);
                    }
                }



                return anuncios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }


}



