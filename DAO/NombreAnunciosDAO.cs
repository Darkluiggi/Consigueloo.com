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
    public class NombreAnunciosDAO
    {
        ApplicationDbContext db;
        Controller controller;
        CaracteristicasDAO caracteristicasDAO;
        private const string entidad = "NombreAnuncios";

        public NombreAnunciosDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            caracteristicasDAO = new CaracteristicasDAO(controller);
            this.controller = controller;
        }            

        public List<NombreAnunciosDTO> getList()
        {
            try
            {
                List<NombreAnuncios> periodosModel = db.NombreAnuncios.Where(x => x.estado == true).ToList();
                List<NombreAnunciosDTO> responseList = new List<NombreAnunciosDTO>(); ;
                
                periodosModel.ForEach(x =>
                {
                    NombreAnunciosDTO response = new NombreAnunciosDTO();
                    response.id = x.id;
                    response.nombre = x.nombre;
                    x.caracteristicas.ForEach(y =>
                    {
                        CaracteristicasDTO item = new CaracteristicasDTO();
                        item.nombre = x.nombre;
                        item.id = x.id;
                        response.caracteristicas.Add(item);
                    });
                    x.noIncluidas.ForEach(y =>
                    {
                        CaracteristicasDTO item = new CaracteristicasDTO();
                        item.nombre = x.nombre;
                        item.id = x.id;

                        response.noIncluidas.Add(item);
                    });
                    responseList.Add(response);
                });
                return responseList;         

            }
            catch (Exception)
            {

                throw;
            }
        }

        public NombreAnunciosDTO Add(string nombre, List<string> dataList)
        {
            try
            {
                NombreAnunciosDTO model = new NombreAnunciosDTO();
                model.nombre = nombre;
                List<CaracteristicasDTO> caracteristicas = caracteristicasDAO.getList();
                dataList.ForEach(x =>
                {
                    caracteristicas.ForEach(y =>
                    {
                        if (x == y.id.ToString())
                        {
                            model.caracteristicas.Add(y);
                        }
                    });
                });
                model.noIncluidas = caracteristicas.Except(model.caracteristicas).ToList();
                var remove = model.noIncluidas.FirstOrDefault(x => x.nombre.Equals("Imagen"));
                model.noIncluidas.Remove(remove);
                //Mapeo de clase

                NombreAnuncios response = new NombreAnuncios();
                response.nombre = model.nombre;
                model.caracteristicas.ForEach(x =>
                {
                    Caracteristicas item = new Caracteristicas();
                    item.nombre = x.nombre;
                    item.id = x.id;
                    response.caracteristicas.Add(item);
                });
                model.noIncluidas.ForEach(x =>
                {
                    Caracteristicas item = new Caracteristicas();
                    item.nombre = x.nombre;
                    item.id = x.id;

                    response.noIncluidas.Add(item);
                });
                db.NombreAnuncios.Add(response);
                db.SaveChanges();
                return model;
            }

            catch (Exception)
            {

                throw;
            }
        }

        public NombreAnunciosDTO Find(int? id)
        {
            try
            {
                //Mapeo de clase
                NombreAnuncios model = db.NombreAnuncios.Include(x=> x.caracteristicas).Include(x=>x.noIncluidas).FirstOrDefault(x=> x.id==id);
                NombreAnunciosDTO response = new NombreAnunciosDTO();
                response.id = model.id;
                response.nombre = model.nombre;
                model.caracteristicas.ForEach(x =>
                {
                    CaracteristicasDTO item = new CaracteristicasDTO();
                    item.nombre = x.nombre;
                    item.id = x.id;
                    response.caracteristicas.Add(item);
                });
                model.noIncluidas.ForEach(x =>
                {
                    CaracteristicasDTO item = new CaracteristicasDTO();
                    item.nombre = x.nombre;
                    item.id = x.id;

                    response.noIncluidas.Add(item);
                });

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NombreAnunciosDTO Update(int id, string nombre, List<string> dataList)
        {
            NombreAnunciosDTO model = new NombreAnunciosDTO();
            try
            {                
                model.nombre = nombre;
                List<CaracteristicasDTO> caracteristicas = caracteristicasDAO.getList();
                dataList.ForEach(x =>
                {
                    caracteristicas.ForEach(y =>
                    {
                        if (x == y.id.ToString())
                        {
                            model.caracteristicas.Add(y);
                        }
                    });
                });
                model.noIncluidas = caracteristicas.Except(model.caracteristicas).ToList();
                //Mapeo de clase

                NombreAnuncios response = db.NombreAnuncios.Include(x=>x.caracteristicas).Include(x=> x.noIncluidas).First(x=>x.id==id);
                response.nombre = model.nombre;
                if (response.caracteristicas.Count==0)
                {
                    model.caracteristicas.ForEach(y =>
                    {
                        Caracteristicas item = new Caracteristicas();
                        item.nombre = y.nombre;
                        item.id = y.id;
                        response.caracteristicas.Add(item);

                    });
                }
                else
                {
                    response.caracteristicas.Clear();
                    model.caracteristicas.ForEach(y =>
                    {
                        Caracteristicas item = new Caracteristicas();
                        item.nombre = y.nombre;
                        item.id = y.id;
                        response.caracteristicas.Add(item);
                    });
                }
                if (response.noIncluidas.Count==0)
                {
                    model.noIncluidas.ForEach(y =>
                    {
                        Caracteristicas item = new Caracteristicas();
                        item.nombre = y.nombre;
                        item.id = y.id;
                        response.noIncluidas.Add(item);

                    });
                }
                else
                {
                    response.noIncluidas.Clear();
                    model.noIncluidas.ForEach(x =>
                    {
                        Caracteristicas item = new Caracteristicas();
                        item.nombre = x.nombre;
                        item.id = x.id;
                        response.noIncluidas.Add(item);
                        
                    });
                }
                    
               

                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();

                model = Find(id);

                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgSuccess(entidad), Helpers.InfoMensajes.ConstantsLevels.SUCCESS);
                return model;

            }
            catch (Exception)
            {
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.EditarMsgError(entidad), Helpers.InfoMensajes.ConstantsLevels.ERROR);
                return model;
            }
        }

        public void Remove(int id)
        {
            try
            {
                NombreAnuncios nombreAnuncio = db.NombreAnuncios.Find(id);
                nombreAnuncio.estado = false;
                db.Entry(nombreAnuncio).State = EntityState.Modified;
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
        public List<CaracteristicasDTO> getCaracteristicasByID(int id)
        {
            try
            {
                NombreAnunciosDTO nombre = Find(id);
                List<CaracteristicasDTO> response = nombre.caracteristicas;
                return response;


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}



