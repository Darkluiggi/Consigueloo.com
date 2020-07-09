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
    public class TipoAnunciosDAO
    {
        ApplicationDbContext db;
        Controller controller;
        NombreAnunciosDAO nombreAnunciosDAO;
        PeriodosDAO periodosDAO;
        DropDownDAO dropDownDAO;
        private const string entidad = "Nombre de anuncio";

        public TipoAnunciosDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            dropDownDAO = new DropDownDAO();
            this.controller = controller;
            nombreAnunciosDAO = new NombreAnunciosDAO(controller);
            periodosDAO = new PeriodosDAO(controller);
        }


        public List<TipoAnunciosDTO> getList()
        {
            try
            {
                List<TipoAnuncios> tipoAnuncio = db.TiposAnuncio.Include(x => x.nombre)
                    .Include(x => x.nombre.noIncluidas).Where(x => x.estado == true).ToList();
                List<TipoAnunciosDTO> responseList = new List<TipoAnunciosDTO>(); 
               

                tipoAnuncio.ForEach(x =>
                {
                    TipoAnunciosDTO response = new TipoAnunciosDTO();
                    response.id = x.id;
                    response.duracion = x.duracion;
                    response.nombre = nombreAnunciosDAO.Find(x.nombreId);
                    response.precio = x.precio;
                    response.nombreId = x.nombreId;
                    responseList.Add(response);
                });
                return responseList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public TipoAnunciosDTO Add(string nombre, string duracion, string precio)
        {
            try
            {
                int? idnombre = Int32.Parse(nombre);
                int? idduracion = Int32.Parse(duracion);
                TipoAnunciosDTO tipoAnuncio = new TipoAnunciosDTO();
                tipoAnuncio.nombre = nombreAnunciosDAO.Find(idnombre);
                var periodo = periodosDAO.Find(idduracion);
                tipoAnuncio.duracion = periodo.nombre;
                tipoAnuncio.precio = precio;

                TipoAnuncios response = new TipoAnuncios();
                response.nombre = new NombreAnuncios();
                response.nombre.nombre=tipoAnuncio.nombre.nombre;
                response.precio = tipoAnuncio.precio;
                response.duracion = tipoAnuncio.duracion;
                response.nombre = db.NombreAnuncios.Include(x=> x.caracteristicas)
                    .Include(x=>x.noIncluidas).FirstOrDefault(x=>x.id==tipoAnuncio.nombre.id);
                db.TiposAnuncio.Add(response);
                db.SaveChanges();
                return tipoAnuncio;
            }

            catch (Exception)
            {

                throw;
            }
        }

        public TipoAnunciosDTO Find(int? id)
        {
            try
            {
                
                //Mapeo de clase
                TipoAnuncios model = db.TiposAnuncio.Find(id);
                TipoAnunciosDTO response = new TipoAnunciosDTO();
                response.id = model.id;
                response.duracion = model.duracion;
                response.nombre = nombreAnunciosDAO.Find(model.nombreId);
                response.precio = model.precio;
                response.nombreId = model.nombreId;
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public TipoAnunciosDTO Update(TipoAnunciosDTO tipoAnuncio)
        {
            try
            {
                TipoAnuncios response = new TipoAnuncios();
                response.id = tipoAnuncio.id;
                response.duracion = tipoAnuncio.duracion;
                response.nombre = db.NombreAnuncios.Find(tipoAnuncio.nombreId);
                response.precio = tipoAnuncio.precio;
                response.nombreId = tipoAnuncio.nombreId;

                db.Entry(tipoAnuncio).State = EntityState.Modified;
                db.SaveChanges();

                tipoAnuncio = this.Find(tipoAnuncio.id);

                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return tipoAnuncio;

            }
            catch (Exception ex)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
                return tipoAnuncio;
            }
        }

        public void Remove(int id)
        {
            try
            {
                TipoAnuncios tipoAnuncio = db.TiposAnuncio.Find(id);
                tipoAnuncio.estado = false;
                db.Entry(tipoAnuncio).State = EntityState.Modified;
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



