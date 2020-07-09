using AutoMapper;
using Helpers.InfoMensajes;
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
    public class LocalidadesDAO
    {
        ApplicationDbContext db;
        Controller controller;
        private const string entidad = "Localidades";

        public LocalidadesDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            this.controller = controller;
        }


        public List<LocalidadesDTO> getList()
        {
            try
            {
                List<Localidades> localidadesModel = db.Localidades.Where(x => x.estado == true).ToList();
                List<LocalidadesDTO> responseList = new List<LocalidadesDTO>(); ;
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Localidades, LocalidadesDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                localidadesModel.ForEach(x =>
                {
                    LocalidadesDTO response = mapper.Map<Localidades, LocalidadesDTO>(x);
                    responseList.Add(response);
                });
                return responseList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(LocalidadesDTO periodo)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LocalidadesDTO, Localidades>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase

                Localidades response = mapper.Map<LocalidadesDTO, Localidades>(periodo);
                db.Localidades.Add(response);
                db.SaveChanges();
            }

            catch (Exception)
            {

                throw;
            }
        }

        public LocalidadesDTO Find(int? id)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Localidades, LocalidadesDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                Localidades model = db.Localidades.Find(id);
                LocalidadesDTO response = mapper.Map<Localidades, LocalidadesDTO>(model);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public LocalidadesDTO Update(LocalidadesDTO localidades)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<LocalidadesDTO, Localidades>();
                });
                IMapper mapper = config.CreateMapper();
                Localidades localidadesModel = mapper.Map<LocalidadesDTO, Localidades>(localidades);

                db.Entry(localidadesModel).State = EntityState.Modified;
                db.SaveChanges();

                localidades = this.Find(localidades.id);

                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return localidades;

            }
            catch (Exception ex)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
                return localidades;
            }
        }

        public void Remove(int id)
        {
            try
            {
                Localidades razonsocial = db.Localidades.Find(id);
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



