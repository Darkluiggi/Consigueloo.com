using AutoMapper;
using Helpers.InfoMensajes;
using Model.CatalogoEmpresa;
using Model.ConfiguracionPlataforma;
using Model.ViewModel;
using Persistence;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.IO;

namespace DAO
{
    public class CatalogoDAO
    {
        ApplicationDbContext db;
        Controller controller;
        private const string entidad = "Catalogo";

        public CatalogoDAO(Controller controller)
        {
            db = new ApplicationDbContext();
            this.controller = controller;
        }


        public List<CatalogoEmpresaDTO> getList()
        {
            try
            {
                List<CatalogoEmpresa> catalogoModel = db.CatalogoEmpresas.Where(x => x.estado == true).ToList();
                //catalogoModel = catalogoModel.GroupBy(x => x.pagina).Select(x => x.FirstOrDefault()).ToList();
                List<CatalogoEmpresaDTO> responseList = new List<CatalogoEmpresaDTO>(); ;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CatalogoEmpresa, CatalogoEmpresaDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                catalogoModel.ForEach(x =>
                {
                    CatalogoEmpresaDTO response = mapper.Map<CatalogoEmpresa, CatalogoEmpresaDTO>(x);
                    response.whatsappLink = x.whatsappLink;
                    responseList.Add(response);
                });
                return responseList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(CatalogoEmpresaDTO catalogo)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;


            // Get an ImageCodecInfo object that represents the JPEG codec.
            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID

            // for the Quality parameter category.
            myEncoder = Encoder.Quality;
            bool isNew = false;
            try
            {
                CatalogoEmpresa pagina = db.CatalogoEmpresas.Where(ce => ce.id == catalogo.id).FirstOrDefault();
                if (pagina == null)
                {
                    pagina = new CatalogoEmpresa();
                    isNew = true;
                }
                // EncoderParameter object in the array.
                myEncoderParameters = new EncoderParameters(1);

                // Save the bitmap as a JPEG file with quality level 25.
                myEncoderParameter = new EncoderParameter(myEncoder, 25L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                
                if (catalogo.imageFile != null)
                {
                    Image sourceimage = Image.FromStream(catalogo.imageFile.InputStream);
                    var path = System.Web.HttpContext.Current.Server.MapPath("../"+pagina.imagePath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    string serverPath = System.Web.HttpContext.Current.Server.MapPath("~/catalogo/");
                    if (!Directory.Exists(serverPath))
                    {
                        Directory.CreateDirectory(serverPath);
                    }
                    var nombreImagen = catalogo.nombreProducto.Replace('á', 'a');
                    nombreImagen = nombreImagen.Replace('é', 'e');
                    nombreImagen = nombreImagen.Replace('í', 'i');
                    nombreImagen = nombreImagen.Replace('ó', 'o');
                    nombreImagen = nombreImagen.Replace('ú', 'u');
                    nombreImagen = nombreImagen.Replace(' ', '_');
                    string strUploadPath = Path.Combine(serverPath, nombreImagen + ".jpeg");

                    sourceimage.Save(strUploadPath, myImageCodecInfo, myEncoderParameters);
                    catalogo.imagePath = "../../catalogo/" + nombreImagen + ".jpeg";
                }

                if(catalogo.nombreProducto != null)
                {
                    pagina.nombreProducto = catalogo.nombreProducto;
                }
                pagina.pagina = catalogo.pagina;
                if(catalogo.imagePath != null)
                {
                    pagina.imagePath = catalogo.imagePath;
                }

                pagina.whatsappLink = "https://wa.me/+573243934309?text=Hola vengo de Consigueloo.co y me interesa el producto " + catalogo.nombreProducto + " Link: "
                    + "https://Consigueloo.co/Catalogo/Productos/Producto?nombre=" + catalogo.nombreProducto;
                pagina.whatsappLink = pagina.whatsappLink.Replace(" ", "_");
                if (isNew)
                {
                    db.CatalogoEmpresas.Add(pagina);
                }
                else
                {
                    db.Entry(pagina).State = EntityState.Modified;
                }
                db.SaveChanges();
                ViewInfoMensaje.setMensaje(controller, MensajeBuilder.CrearMsgSuccess(entidad), ConstantsLevels.SUCCESS);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CatalogoEmpresaDTO Find(int? id)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CatalogoEmpresa, CatalogoEmpresaDTO>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase
                CatalogoEmpresa model = db.CatalogoEmpresas.Find(id);
                CatalogoEmpresaDTO response = mapper.Map<CatalogoEmpresa, CatalogoEmpresaDTO>(model);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CatalogoEmpresaDTO Update(CatalogoEmpresaDTO caracteristicas)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CatalogoEmpresaDTO, CatalogoEmpresa>();
                });
                IMapper mapper = config.CreateMapper();
                CatalogoEmpresa caracteristicasModel = mapper.Map<CatalogoEmpresaDTO, CatalogoEmpresa>(caracteristicas);

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

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        
    }
}



