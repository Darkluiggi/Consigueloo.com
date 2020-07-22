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
    public class CategoriasDAO
    {
        ApplicationDbContext db;
        private ImageHelper imageHelper;
        Controller controller;
        private const string entidad = "Categorias";

        public CategoriasDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            this.controller = controller;
            imageHelper = new ImageHelper();
        }


        public List<CategoriasDTO> getList()
        {
            try
            {
                List<Categorias> categoriasModel = db.Categorias.Where(x => x.estado == true).ToList();
                categoriasModel = categoriasModel.GroupBy(x => x.nombre).Select(x => x.FirstOrDefault()).ToList();
                List<CategoriasDTO> responseList = new List<CategoriasDTO>();

                    foreach (var item in categoriasModel)
                    {
                        CategoriasDTO anuncioModel = new CategoriasDTO();
                        anuncioModel.nombre = item.nombre;
                        anuncioModel.id = item.id;
                    if (item.icono != null)
                    {
                        anuncioModel.path = imageHelper.GetImageFromByteArray(item.icono);
                    }                      


                    responseList.Add(anuncioModel);
                    }


                    return responseList;
                }
            
            catch (Exception)
            {

                throw;
            }
        }
        public void Add(CategoriasDTO categoria)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CategoriasDTO, Categorias>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                Categorias anunciolModel = mapper.Map<CategoriasDTO, Categorias>(categoria);


                db.Categorias.Add(anunciolModel);
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CategoriasDTO Find(int? id)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Categorias, CategoriasDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                Categorias model = db.Categorias.Find(id);
                CategoriasDTO response = mapper.Map<Categorias, CategoriasDTO>(model);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CategoriasDTO Update(CategoriasDTO categorias)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CategoriasDTO, Categorias>();
                });
                IMapper mapper = config.CreateMapper();
                Categorias categoriasModel = mapper.Map<CategoriasDTO, Categorias>(categorias);

                db.Entry(categoriasModel).State = EntityState.Modified;
                db.SaveChanges();

                categorias = this.Find(categorias.id);

                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return categorias;

            }
            catch (Exception ex)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
                return categorias;
            }
        }

        public void Remove(int id)
        {
            try
            {
                Categorias razonsocial = db.Categorias.Find(id);
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

        internal CategoriasDTO FindByName(string name)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Categorias, CategoriasDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                Categorias model = db.Categorias.FirstOrDefault(x=> x.nombre.Equals(name));
                CategoriasDTO response = mapper.Map<Categorias, CategoriasDTO>(model);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}



