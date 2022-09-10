using AutoMapper;
using Helpers.InfoMensajes;
using Model.ConfiguracionPlataforma;
using Model.Usuarios;
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
    public class RolesDAO
    {
        ApplicationDbContext db;
        Controller controller;
        private const string entidad = "Roles";

        public RolesDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            this.controller = controller;
        }


        public List<RolesDTO> getList()
        {
            try
            {
                List<Roles> caracteristicasModel = db.Roles.Where(x => x.estado == true).ToList();
                caracteristicasModel = caracteristicasModel.GroupBy(x => x.nombre).Select(x => x.FirstOrDefault()).ToList();
                List<RolesDTO> responseList = new List<RolesDTO>(); ;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Roles, RolesDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                caracteristicasModel.ForEach(x =>
                {
                    RolesDTO response = mapper.Map<Roles, RolesDTO>(x);
                    responseList.Add(response);
                });
                return responseList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(RolesDTO caracteristica)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RolesDTO, Roles>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase

                Roles response = mapper.Map<RolesDTO, Roles>(caracteristica);
                db.Roles.Add(response);
                db.SaveChanges();
            }

            catch (Exception)
            {

                throw;
            }
        }

        public RolesDTO Find(int? id)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Roles, RolesDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                Roles model = db.Roles.Find(id);
                RolesDTO response = mapper.Map<Roles, RolesDTO>(model);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RolesDTO Update(RolesDTO caracteristicas)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RolesDTO, Roles>();
                });
                IMapper mapper = config.CreateMapper();
                Roles caracteristicasModel = mapper.Map<RolesDTO, Roles>(caracteristicas);

                db.Entry(caracteristicasModel).State = EntityState.Modified;
                db.SaveChanges();

                caracteristicas = this.Find(caracteristicas.id);

                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return caracteristicas;

            }
            catch (Exception)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
                return caracteristicas;
            }
        }

        public void Remove(int id)
        {
            try
            {
                Roles razonsocial = db.Roles.Find(id);
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



