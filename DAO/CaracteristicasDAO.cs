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
    public class CaracteristicasDAO
    {
        ApplicationDbContext db;
        Controller controller;
        private const string entidad = "Caracteristicas";

        public CaracteristicasDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            this.controller = controller;
        }


        public List<CaracteristicasDTO> getList()
        {
            try
            {
                List<Caracteristicas> caracteristicasModel = db.Caracteristicas.Where(x => x.estado == true).ToList();
                caracteristicasModel = caracteristicasModel.GroupBy(x => x.nombre).Select(x => x.FirstOrDefault()).ToList();
                List<CaracteristicasDTO> responseList = new List<CaracteristicasDTO>(); ;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Caracteristicas, CaracteristicasDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                caracteristicasModel.ForEach(x =>
                {
                    CaracteristicasDTO response = mapper.Map<Caracteristicas, CaracteristicasDTO>(x);
                    responseList.Add(response);
                });
                return responseList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(CaracteristicasDTO caracteristica)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CaracteristicasDTO, Caracteristicas>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase

                Caracteristicas response = mapper.Map<CaracteristicasDTO, Caracteristicas>(caracteristica);
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.CrearMsgSuccess(entidad), ConstantsLevels.SUCCESS);
                db.Caracteristicas.Add(response);
                db.SaveChanges();
            }

            catch (Exception)
            {

                throw;
            }
        }

        public CaracteristicasDTO Find(int? id)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Caracteristicas, CaracteristicasDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                Caracteristicas model = db.Caracteristicas.Find(id);
                CaracteristicasDTO response = mapper.Map<Caracteristicas, CaracteristicasDTO>(model);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CaracteristicasDTO Update(CaracteristicasDTO caracteristicas)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CaracteristicasDTO, Caracteristicas>();
                });
                IMapper mapper = config.CreateMapper();
                Caracteristicas caracteristicasModel = mapper.Map<CaracteristicasDTO, Caracteristicas>(caracteristicas);

                db.Entry(caracteristicasModel).State = EntityState.Modified;
                db.SaveChanges();

                caracteristicas = this.Find(caracteristicas.id);

                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return caracteristicas;

            }
            catch (Exception )
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
                return caracteristicas;
            }
        }

        public void Remove(int id)
        {
            try
            {
                Caracteristicas razonsocial = db.Caracteristicas.Find(id);
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



