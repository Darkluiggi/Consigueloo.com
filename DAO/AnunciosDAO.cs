
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
using System.Web;
using Model.ConfiguracionPlataforma;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

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
                Anuncio anuncioInDB = db.Anuncios.Find(anuncio.id);
                int categoriaId = Int32.Parse(categoria);
                CategoriasDTO cat = categoriasDAO.Find(categoriaId);
                anuncio.categoria = cat;
                anuncio.fechaActivacion = DateTime.Today;
                int localidadId = Int32.Parse(localidad);
                LocalidadesDTO loc = localidadesDAO.Find(localidadId);
                anuncio.localidad=loc;
                if (duracion.Contains("Mensual"))
                {
                    anuncio.fechaCancelacion = anuncio.fechaActivacion.AddDays(30);
                }
                if (duracion.Contains("Trimestral"))
                {
                    anuncio.fechaCancelacion = anuncio.fechaActivacion.AddDays(90);
                }
                if (duracion.Contains("Anual"))
                {
                    anuncio.fechaCancelacion = anuncio.fechaActivacion.AddDays(360);
                }

                anuncioInDB.titulo = anuncio.titulo;
                anuncioInDB.imagen = anuncio.imagen;
                anuncioInDB.descripcion = anuncio.descripcion;
                anuncioInDB.actImagen = anuncio.actImagen;
                anuncioInDB.nombreContacto = anuncio.nombreContacto;
                anuncioInDB.telefono = anuncio.telefono;
                anuncioInDB.celularContacto = anuncio.celularContacto;
                anuncioInDB.actCatalogo = anuncio.actCatalogo;
                anuncioInDB.fechaActivacion = anuncio.fechaActivacion;
                anuncioInDB.fechaCancelacion = anuncio.fechaCancelacion;
                anuncioInDB.estado = true;

                anuncioInDB.categoria = db.Categorias.Find(anuncio.categoria.id);
                anuncioInDB.localidad = db.Localidades.Find(anuncio.localidad.id);
                
                db.Entry(anuncioInDB).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public object CreateOrUpdate(HttpFileCollectionBase files, AnuncioVueDTO anuncio)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            myEncoder = Encoder.Quality;
            // EncoderParameter object in the array.
            myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap as a JPEG file with quality level 25.
            myEncoderParameter = new EncoderParameter(myEncoder, 25L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            var result = new List<HttpPostedFileBase>();

            Anuncio anuncioModel = new Anuncio();
            for (int i = 0; i < files.AllKeys.Count(); i++)
            {
                if (files.AllKeys[i].Contains("catalogo"))
                {
                    result.Add(files[i]);
                }
            }
            anuncioModel.categoria = db.Categorias.Find(anuncio.categoriaId);
            anuncioModel.fechaActivacion = DateTime.Today;
            anuncioModel.localidad = db.Localidades.Find(anuncio.localidadId);
            TipoAnuncios tipoAnuncio = db.TiposAnuncio.Find(anuncio.tipoAnuncioId);
            if (tipoAnuncio.duracion.Contains("Mensual"))
            {
                anuncioModel.fechaCancelacion = anuncioModel.fechaActivacion.AddDays(30);
            }
            if (tipoAnuncio.duracion.Contains("Trimestral"))
            {
                anuncioModel.fechaCancelacion = anuncioModel.fechaActivacion.AddDays(90);
            }
            if (tipoAnuncio.duracion.Contains("Anual"))
            {
                anuncioModel.fechaCancelacion = anuncioModel.fechaActivacion.AddDays(360);
            }
            anuncioModel.descripcion = anuncio.descripcion;
            anuncioModel.actImagen = anuncio.actImagen;
            anuncioModel.nombreContacto = anuncio.nombreContacto;
            anuncioModel.telefono = anuncio.telefono;
            anuncioModel.celularContacto = anuncio.celularContacto;
            anuncioModel.actCatalogo = anuncio.actCatalogo;
            anuncioModel.tipoAnuncio = tipoAnuncio;

            anuncioModel.categoria = db.Categorias.Find(anuncio.categoriaId);
            anuncioModel.localidad = db.Localidades.Find(anuncio.localidadId);

            anuncioModel.titulo = anuncio.titulo;
            if(anuncio.imagen != null)
            {
                Image sourceimage = Image.FromStream(anuncio.imagen.InputStream);
                string serverPath = HttpContext.Current.Server.MapPath("~/anuncio/" + anuncioModel.titulo + "/");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                string strUploadPath = Path.Combine(serverPath, anuncio.imagen.FileName);

                sourceimage.Save(strUploadPath, myImageCodecInfo, myEncoderParameters);
                anuncioModel.imagePath = "../../anuncio/" + anuncioModel.titulo +"/" + anuncio.imagen.FileName;
            }

            db.Anuncios.Add(anuncioModel);
            db.SaveChanges();
            return null;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
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
                        anuncioModel.path = item.imagen != null ? imageHelper.GetImageFromByteArray(item.imagen) : item.imagePath;
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
                        anuncioModel.path = item.imagen != null ? imageHelper.GetImageFromByteArray(item.imagen): item.imagePath;
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

        public bool RegisterPayment(PaymentData paymentData, string user, TipoAnunciosDTO tipoAnuncio)
        {
            bool result = false;
            var payment = db.pagosAnuncios.Find(paymentData.id);
            if (payment != null)
            {
                return result = true;
            }
            
            try
            {
                Anuncio anuncio = new Anuncio();
                anuncio.estado = false;
                anuncio.tipoAnuncioId = tipoAnuncio.id;
                anuncio.pago = paymentData;
                anuncio.fechaActivacion = DateTime.Now;
                anuncio.fechaCancelacion = DateTime.Now.AddDays(30);
                Usuarios usuario = db.Usuarios.FirstOrDefault(x => x.correo.Equals(user));
                if (usuario == null)
                {
                    db.pagosAnuncios.Add(paymentData);
                    db.Anuncios.Add(anuncio);
                }
                else
                {
                    paymentData.merchant = db.datosComercio.Find(paymentData.merchant.legal_id);
                    usuario.pagosAnuncios.Add(paymentData);
                    usuario.anuncios.Add(anuncio);
                }
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public AnuncioDTO getById(int id)
        {
            try
            {
                Anuncio model = db.Anuncios.Include(x => x.catalogo).Include(x => x.catalogo.imagen).FirstOrDefault(x => x.id == id);
                AnuncioDTO anuncioModel = new AnuncioDTO();
                anuncioModel.titulo = model.titulo;
                anuncioModel.nombreContacto = model.nombreContacto;
                anuncioModel.telefono = model.telefono;
                anuncioModel.actCatalogo = model.actCatalogo;
                anuncioModel.actImagen = model.actImagen;
                anuncioModel.celularContacto = model.celularContacto;
                anuncioModel.descripcion = model.descripcion;
                anuncioModel.id = model.id;
                anuncioModel.path = model.imagen != null ? imageHelper.GetImageFromByteArray(model.imagen) : model.imagePath;
                anuncioModel.fechaActivacion = model.fechaActivacion;
                anuncioModel.fechaCancelacion = model.fechaCancelacion;
                anuncioModel.categoriaId = model.categoriaId;
                anuncioModel.categoria = categoriasDAO.Find(model.categoriaId);
                if (model.catalogo != null)
                {
                    anuncioModel.catalogo = GetCatalogo(model.catalogo);
                }
                if(model.pago != null)
                {
                    anuncioModel.pago = new PaymentDataDTO(model.pago);
                }
                anuncioModel.tipoAnuncioId = model.tipoAnuncioId;
                anuncioModel.tipoAnuncio = new TipoAnunciosDTO(model.tipoAnuncio);
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
                    anuncioModel.path = item.imagen != null ? imageHelper.GetImageFromByteArray(item.imagen) : item.imagePath;
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
