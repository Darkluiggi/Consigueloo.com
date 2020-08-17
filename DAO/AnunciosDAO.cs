
using Model.Anuncios;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Persistence;
using System.Web.Mvc;
using Helpers.Methods;
using Model.Usuarios;
using System.Security.Cryptography.X509Certificates;

namespace DAO
{
    public class AnunciosDAO
    {
        private ApplicationDbContext db;
        private ImageHelper imageHelper;
        private CategoriasDAO categoriasDAO;
        private LocalidadesDAO localidadesDAO;
        private DateHelper dateHelper;


        public AnunciosDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            imageHelper = new ImageHelper();
            categoriasDAO = new CategoriasDAO(controller);
            localidadesDAO = new LocalidadesDAO(controller);
            dateHelper = new DateHelper();
        }

        public void GuardarAnuncio(string user, AnuncioDTO anuncio, string categoria, string duracion, string localidad)
        {
            try
            {
                int categoriaId = Int32.Parse(categoria);
                CategoriasDTO cat = categoriasDAO.Find(categoriaId);
                anuncio.categoria = cat;
                anuncio.fechaActivacion = DateTime.Today;
                int localidadId = Int32.Parse(localidad);
                LocalidadesDTO loc = localidadesDAO.Find(localidadId);
                anuncio.localidad=loc;
                anuncio.fechaCancelacion = anuncio.fechaActivacion.AddDays(60);
                
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

                anuncioModel.categoria = db.Categorias.Find(anuncio.categoria.id);
                anuncioModel.localidad = db.Localidades.Find(anuncio.localidad.id);
                Usuarios usuario = db.Usuarios.FirstOrDefault(x => x.correo.Equals(user));
                usuario.anuncios.Add(anuncioModel);
                
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();

                }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<AnuncioDTO> ShowDestacados()
        {
            try
            {

                //Mapeo de clase
                var anuncio = db.Anuncios.Where(x => x.estado == true && x.actDestacado).ToList();
                List<AnuncioDTO> anuncios = new List<AnuncioDTO>();
                if (anuncio != null)
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

                throw ex;
            }
        }

        public List<AnuncioDTO> ListarAnuncios()
        {
            try
            {
                
                //Mapeo de clase
                var anuncio = db.Anuncios.Include(x=> x.catalogo).Include(x=> x.tipoAnuncio).Where(x=> x.estado==true).ToList();
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
                        if (anuncioModel.catalogo!=null)
                        {
                            anuncioModel.categoria = categoriasDAO.Find(item.categoriaId);
                        }

                        anuncioModel.categoria = categoriasDAO.Find(item.categoriaId);
                        anuncioModel.categoriaId = item.categoriaId;
                        anuncioModel.localidadId = item.localidadId;
                        anuncios.Add(anuncioModel);
                    }
                }
                      

            
                return anuncios;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AnuncioDTO> filter(string busqueda, int idLocalidad, int idcategoria)
        {
            try
            {

                List<AnuncioDTO> response = ListarAnuncios();
                if(idLocalidad==0 || idcategoria == 0 || string.IsNullOrEmpty(busqueda))
                {
                    if (idLocalidad == 0 && idcategoria==0 && string.IsNullOrEmpty(busqueda))
                    {
                    }
                    else if ((idLocalidad == 0 || idcategoria == 0) && string.IsNullOrEmpty(busqueda))
                    {
                        response = response.Where(x => x.categoriaId == idcategoria || x.localidadId == idLocalidad).ToList();
                                           
                    }
                    else
                    {

                        response = response.Where(x => (x.categoriaId == idcategoria || x.localidadId == idLocalidad) && (x.titulo.Trim().ToLower().Contains(busqueda.Trim().ToLower()) ||
                        x.nombreContacto.Trim().ToLower().Contains(busqueda.Trim().ToLower()) ||
                        x.categoria.nombre.Trim().ToLower().Contains(busqueda.Trim().ToLower()) ||
                        x.descripcion.Trim().ToLower().Contains(busqueda.Trim().ToLower()))).ToList();
                    }

                }
                else
                {
                    response = response.Where(x => x.categoriaId == idcategoria && x.localidadId == idLocalidad).ToList();
                    response = response.Where(x => x.titulo.Trim().ToLower().Contains(busqueda.Trim().ToLower()) || x.nombreContacto.Trim().ToLower().Contains(busqueda.Trim().ToLower())
                  || x.categoria.nombre.Trim().ToLower().Contains(busqueda.Trim().ToLower()) || x.descripcion.Trim().ToLower().Contains(busqueda.Trim().ToLower())).ToList();

                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GuardarCatalogo(CatalogoDTO catalogo)
        {
            try
            {
                Catalogo catalogoModel = new Catalogo();
                catalogo.imagen.ForEach(x =>
                {
                    CatalogoImagen modelImagen = new CatalogoImagen();
                    modelImagen.imagen = x.imagen;
                    catalogoModel.imagen.Add(modelImagen);

                });
                Anuncio model = db.Anuncios.FirstOrDefault(x => x.id==catalogo.anuncioId);
                model.catalogo = catalogoModel;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public AnuncioDTO getById(int id)
        {
            try
            {
                Anuncio model = db.Anuncios.Include(x=> x.catalogo).Include(x=> x.catalogo.imagen).FirstOrDefault(x => x.id == id && x.estado == true);
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
                if (model.catalogo!=null)
                {
                    anuncioModel.catalogo = GetCatalogo(model.catalogo);
                }
                return anuncioModel;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ChartDataDTO ChartAreaData()
        {
            try
            {
                var anuncios = ListarAnuncios();
                var today = DateTime.Today;

                ChartDataDTO result = new ChartDataDTO();

                for (int i = 15; i >= 0; i--)
                {

                    DateTime fechaResultado = today.AddDays(-i);
                    var model = anuncios.Where(x => x.fechaActivacion.Day == fechaResultado.Day).ToList();
                    result.datos.Add(model.Count.ToString());
                    string fecha = construirFecha(fechaResultado.Month.ToString(),fechaResultado.Day.ToString());
                    result.periodos.Add(fecha);
                }
                                   
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string construirFecha(string Mes, string día)
        {
            try
            {
                string month=dateHelper.stringToMonth(Mes); 
                string result= month + " " + día;

                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ChartDataDTO chartBarData()
        {
            try
            {
                var anuncios = ListarAnuncios();
                var today = DateTime.Today;
                ChartDataDTO result = new ChartDataDTO();

                for (int i = 5; i >= 0; i--)
                {

                    DateTime fechaResultado = today.AddMonths (-i);
                    var model = anuncios.Where(x => x.fechaActivacion.Month == fechaResultado.Month).ToList();
                    result.datos.Add(model.Count.ToString());
                    string Mes = dateHelper.stringToMonth(fechaResultado.Month.ToString()); 
                    result.periodos.Add(Mes);
                }


                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<AnuncioDTO> filterByLocalidadId(int id)
        {
            try
            {
                List<AnuncioDTO> response = ListarAnuncios();
                response = response.Where(x => x.localidadId == id).ToList();
                return response;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AnuncioDTO> filterByCategoriaId(int id)
        {
            try
            {
                List<AnuncioDTO> response = ListarAnuncios();
                response = response.Where(x => x.categoriaId == id).ToList();
                return response;

    }
            catch (Exception ex)
            {

                throw ex;
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

        public CatalogoDTO GetCatalogo(Catalogo catalogo)
        {
            CatalogoDTO response = new CatalogoDTO();
            response.id = catalogo.id;
          
            catalogo.imagen.ForEach(x =>
            {
                CatalogoImagenDTO model = new CatalogoImagenDTO();
                var path = imageHelper.GetImageFromByteArray(x.imagen);
                model.imagen = x.imagen;
                response.imagen.Add(model);
                response.paths.Add(path);
            });
            return response;
        }
    }

    public class ChartDataDTO
    {
        public ChartDataDTO()
        {
            periodos = new List<string>();
            datos = new List<string>();
        }
        public List<string> periodos { get; set; }
        public List<string> datos { get; set; }
    }
}
