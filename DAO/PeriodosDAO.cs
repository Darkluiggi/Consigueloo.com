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
    public class PeriodosDAO
    {
        ApplicationDbContext db;
        Controller controller;
        private const string entidad = "Periodos";

        public PeriodosDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            this.controller = controller;
        }
            

        public List<PeriodosDTO> getList()
        {
            try
            {
                List<Periodos> periodosModel = db.PeriodoAnuncios.Where(x => x.estado == true).ToList();
                List<PeriodosDTO> responseList = new List<PeriodosDTO>(); ;
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Periodos, PeriodosDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                periodosModel.ForEach(x =>
                {
                    PeriodosDTO response = mapper.Map<Periodos, PeriodosDTO>(x);
                    responseList.Add(response);
                });
                return responseList;         

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(PeriodosDTO periodo)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PeriodosDTO, Periodos>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase

                Periodos response = mapper.Map<PeriodosDTO, Periodos>(periodo);
                db.PeriodoAnuncios.Add(response);
                db.SaveChanges();
            }

            catch (Exception)
            {

                throw;
            }
        }

        public PeriodosDTO Find(int? id)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Periodos, PeriodosDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                Periodos model = db.PeriodoAnuncios.Find(id);
                PeriodosDTO response = mapper.Map<Periodos, PeriodosDTO>(model);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PeriodosDTO Update(PeriodosDTO periodos)
        {
            try
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<PeriodosDTO, Periodos>();
                });
                IMapper mapper = config.CreateMapper();
                Periodos periodosModel = mapper.Map<PeriodosDTO, Periodos>(periodos);

                db.Entry(periodosModel).State = EntityState.Modified;
                db.SaveChanges();

                periodos = this.Find(periodos.id);

                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return periodos;

            }
            catch (Exception)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
                return periodos;
            }
        }

        public void Remove(int id)
        {
            try
            {
                Periodos razonsocial = db.PeriodoAnuncios.Find(id);
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



