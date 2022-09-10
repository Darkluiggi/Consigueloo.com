using AutoMapper;
using Helpers.InfoMensajes;
using Model.ConfiguracionPlataforma;
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
                    TipoAnunciosDTO response = new TipoAnunciosDTO(x);
                    response.nombre = nombreAnunciosDAO.Find(x.nombreId);
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
                NombreAnuncios nombreAnuncio = db.NombreAnuncios.Find(idnombre);
                var periodo = periodosDAO.Find(idduracion);

                TipoAnuncios response = new TipoAnuncios();
                response.nombreId = Int32.Parse(nombre);
                response.precio = precio;
                response.duracion = periodo.nombre;
                response = db.TiposAnuncio.Add(response);
                db.SaveChanges();
                TipoAnunciosDTO tipoAnuncio = new TipoAnunciosDTO(response);
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.CrearMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return tipoAnuncio;
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        public TipoAnunciosDTO Find(int? id)
        {
            try
            {
                int id_ = (int)id;
                //Mapeo de clase
                TipoAnuncios model = new TipoAnuncios();
                model= db.TiposAnuncio.Include(x=> x.nombre).FirstOrDefault(x=> x.id==id_);
                TipoAnunciosDTO response = new TipoAnunciosDTO(model);
                response.nombre = nombreAnunciosDAO.Find(model.nombreId);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TipoAnunciosDTO FindInCents(int? id)
        {
            try
            {
                int id_ = (int)id;
                //Mapeo de clase
                TipoAnuncios model = new TipoAnuncios();
                model = db.TiposAnuncio.Include(x => x.nombre).FirstOrDefault(x => x.id == id_);
                TipoAnunciosDTO response = new TipoAnunciosDTO();
                response.id = model.id;
                response.duracion = model.duracion;
                response.nombre = nombreAnunciosDAO.Find(model.nombreId);
                response.precio = (Int32.Parse(model.precio)*100).ToString();
                response.nombreId = model.nombreId;
                response.reference = Guid.NewGuid().ToString();
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



