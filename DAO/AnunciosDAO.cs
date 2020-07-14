using AutoMapper;
using Model.Anuncios;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using System.Web.Mvc;

namespace DAO
{
    public class AnunciosDAO
    {
        private ApplicationDbContext db;
        private ImageHelper imageHelper;
        private CategoriasDAO categoriasDAO;
        public AnunciosDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            imageHelper = new ImageHelper();
            categoriasDAO = new CategoriasDAO(controller);
        }

        public void GuardarAnuncio(AnuncioDTO anuncio, string categoria)
        {
            try
            {
                int categoriaId = Int32.Parse(categoria);
                CategoriasDTO cat = categoriasDAO.Find(categoriaId);
                anuncio.categoria = cat;
                anuncio.fechaActivacion = DateTime.Today;
                anuncio.fechaCancelacion = anuncio.fechaActivacion.AddDays(15);
                
                Anuncio anuncioModel = new Anuncio();
                anuncioModel.titulo = anuncio.titulo;
                anuncioModel.imagen = anuncio.imagen;
                anuncioModel.descripcion = anuncio.descripcion;
                anuncioModel.actImagen = anuncio.actImagen;
                anuncioModel.nombreContacto = anuncio.nombreContacto;
                anuncioModel.telefono = anuncio.telefono;
                anuncioModel.celularContacto = anuncio.celularContacto;
                anuncioModel.actCatalogo = anuncio.actCatalogo;
                anuncioModel.fechaActivacion = anuncio.fechaActivacion;
                anuncioModel.fechaCancelacion = anuncio.fechaCancelacion;

                anuncioModel.actCatalogo = anuncio.actCatalogo;
                anuncioModel.categoria = db.Categorias.Find(anuncio.categoria.id);

                db.Anuncios.Add(anuncioModel);
                db.SaveChanges();

                }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<AnuncioDTO> ListarAnuncios()
        {
            try
            {
                
                //Mapeo de clase
                var anuncio = db.Anuncios.Where(x=> x.estado==true).ToList();
                List<AnuncioDTO> anuncios=new List<AnuncioDTO>();
                if (anuncio!=null)
                {
                    foreach (var item in anuncio)
                    {
                        AnuncioDTO anuncioModel = new AnuncioDTO();
                        anuncioModel.titulo = item.titulo;
                        anuncioModel.nombreContacto = item.nombreContacto;
                        anuncioModel.telefono = item.telefono;
                        anuncioModel.actCatalogo = item.actCatalogo;
                        anuncioModel.actImagen = item.actImagen;
                        anuncioModel.celularContacto = item.celularContacto;
                        anuncioModel.descripcion = item.descripcion;
                        anuncioModel.id = item.id;
                        anuncioModel.path = imageHelper.GetImageFromByteArray(item.imagen);
                        anuncioModel.fechaActivacion = item.fechaActivacion;
                        anuncioModel.fechaCancelacion = item.fechaCancelacion;
                        anuncioModel.categoria = categoriasDAO.Find(item.categoriaId);
                        anuncioModel.categoriaId = item.categoriaId;

                        anuncios.Add(anuncioModel);
                    }
                }
                      

            
                return anuncios;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public AnuncioDTO getById(int id)
        {
            try
            {
                Anuncio model = db.Anuncios.FirstOrDefault(x => x.id == id && x.estado == true);
                AnuncioDTO anuncioModel = new AnuncioDTO();
                anuncioModel.titulo = model.titulo;
                anuncioModel.nombreContacto = model.nombreContacto;
                anuncioModel.telefono = model.telefono;
                anuncioModel.actCatalogo = model.actCatalogo;
                anuncioModel.actImagen = model.actImagen;
                anuncioModel.celularContacto = model.celularContacto;
                anuncioModel.descripcion = model.descripcion;
                anuncioModel.id = model.id;
                anuncioModel.path = imageHelper.GetImageFromByteArray(model.imagen);
                anuncioModel.fechaActivacion = model.fechaActivacion;
                anuncioModel.fechaCancelacion = model.fechaCancelacion;
                anuncioModel.categoriaId = model.categoriaId;
                anuncioModel.categoria = categoriasDAO.Find(model.categoriaId);
                return anuncioModel;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AnuncioDTO> filterByCategoriaId(int id)
        {
            try
            {
                CategoriasDTO categoria = categoriasDAO.Find(id);
                List<AnuncioDTO> response = ListarAnuncios();
                response = response.Where(x => x.categoriaId == id).ToList();
                return response;

    }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AnuncioDTO> filterByCategoriaName(string nombre)
        {
            try
            {
                CategoriasDTO categoria = categoriasDAO.FindByName(nombre);
                List<AnuncioDTO> response = ListarAnuncios();
                response = response.Where(x => x.categoria.nombre.Equals(nombre)).ToList();
                //if (response == null)
                //{
                //    response = new List<AnuncioDTO>();
                //}
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AnuncioDTO> buscar(string busqueda)
        {
            try
            {
                List<Anuncio> modelList=db.Anuncios.Include(x => x.categoria).Where(x => x.titulo.Trim().ToLower().Contains(busqueda.Trim().ToLower()) || x.nombreContacto.Trim().ToLower().Contains(busqueda.Trim().ToLower())
                 || x.categoria.nombre.Trim().ToLower().Contains(busqueda.Trim().ToLower()) || x.descripcion.Trim().ToLower().Contains(busqueda.Trim().ToLower())).ToList();

                List < AnuncioDTO > responseList= new List<AnuncioDTO>();
                foreach (var item in modelList)
                {
                    AnuncioDTO anuncioModel = new AnuncioDTO();
                    anuncioModel.titulo = item.titulo;
                    anuncioModel.nombreContacto = item.nombreContacto;
                    anuncioModel.telefono = item.telefono;
                    anuncioModel.actCatalogo = item.actCatalogo;
                    anuncioModel.actImagen = item.actImagen;
                    anuncioModel.celularContacto = item.celularContacto;
                    anuncioModel.descripcion = item.descripcion;
                    anuncioModel.id = item.id;
                    anuncioModel.path = imageHelper.GetImageFromByteArray(item.imagen);
                    anuncioModel.fechaActivacion = item.fechaActivacion;
                    anuncioModel.fechaCancelacion = item.fechaCancelacion;
                    anuncioModel.categoria = categoriasDAO.Find(item.categoriaId);
                    anuncioModel.categoriaId = item.categoriaId;

                    responseList.Add(anuncioModel);
                }


                return responseList;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
