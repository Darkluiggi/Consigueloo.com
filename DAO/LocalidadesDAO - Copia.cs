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
    public class AnuncioTrabajoDAO
    {
        ApplicationDbContext db;
        Controller controller;
        private const string entidad = "AnuncioTrabajo";

        public AnuncioTrabajoDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            this.controller = controller;
        }


        public List<AnuncioTrabajoDTO> getList()
        {
            try
            {
                List<AnuncioTrabajo> AnuncioTrabajoModel = db.AnuncioTrabajo.Where(x => x.estado == true).ToList();
                List<AnuncioTrabajoDTO> responseList = new List<AnuncioTrabajoDTO>(); ;
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<AnuncioTrabajo, AnuncioTrabajoDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                AnuncioTrabajoModel.ForEach(x =>
                {
                    AnuncioTrabajoDTO response = mapper.Map<AnuncioTrabajo, AnuncioTrabajoDTO>(x);
                    responseList.Add(response);
                });
                return responseList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(AnuncioTrabajoDTO periodo)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AnuncioTrabajoDTO, AnuncioTrabajo>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase

                AnuncioTrabajo response = mapper.Map<AnuncioTrabajoDTO, AnuncioTrabajo>(periodo);
                db.AnuncioTrabajo.Add(response);
                db.SaveChanges();
            }

            catch (Exception)
            {

                throw;
            }
        }

        public AnuncioTrabajoDTO Find(int? id)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AnuncioTrabajo, AnuncioTrabajoDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                AnuncioTrabajo model = db.AnuncioTrabajo.Find(id);
                AnuncioTrabajoDTO response = mapper.Map<AnuncioTrabajo, AnuncioTrabajoDTO>(model);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AnuncioTrabajoDTO Update(AnuncioTrabajoDTO AnuncioTrabajo)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<AnuncioTrabajoDTO, AnuncioTrabajo>();
                });
                IMapper mapper = config.CreateMapper();
                AnuncioTrabajo AnuncioTrabajoModel = mapper.Map<AnuncioTrabajoDTO, AnuncioTrabajo>(AnuncioTrabajo);

                db.Entry(AnuncioTrabajoModel).State = EntityState.Modified;
                db.SaveChanges();

                AnuncioTrabajo = this.Find(AnuncioTrabajo.id);

                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return AnuncioTrabajo;

            }
            catch (Exception ex)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
                return AnuncioTrabajo;
            }
        }

        public void Remove(int id)
        {
            try
            {
                AnuncioTrabajo razonsocial = db.AnuncioTrabajo.Find(id);
                razonsocial.estado = false;
                db.Entry(razonsocial).State = EntityState.Modified;
                db.SaveChanges();
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.BorrarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
            }
            catch (Exception)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.BorrarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
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



