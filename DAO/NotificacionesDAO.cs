using AutoMapper;
using Helpers.InfoMensajes;
using Model.Anuncios;
using Model.ConfiguraciónPlataforma;
using Model.ViewModel;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public NotificacionesDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            this.controller = controller;
        }
            

        public List<NotificacionDTO> getList()
        {
            try
            {
                List<Notificacion> notificacionsModel = db.Notificaciones.Where(x => x.estado == true).ToList();
                List<NotificacionDTO> responseList = new List<NotificacionDTO>(); ;
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Notificacion, NotificacionDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                notificacionsModel.ForEach(x =>
                {
                    NotificacionDTO response = mapper.Map<Notificacion, NotificacionDTO>(x);
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
    }
}



