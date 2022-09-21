using Model.ConfiguracionPlataforma;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;
using Model.Anuncios;
using Model.CatalogoEmpresa;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public ApplicationDbContext() : base("name=ConsiguelooConnectionString")
        {
        }
        public DbSet<Model.Usuarios.Roles> Roles { get; set; }
        public DbSet<Model.Usuarios.Usuarios> Usuarios { get; set; }
        public DbSet<Model.Anuncios.Anuncio> Anuncios { get; set; }
        public DbSet<Model.Anuncios.Notificacion> Notificaciones { get; set; }
        public DbSet<Model.Anuncios.Catalogo> Catalogos { get; set; }
        public DbSet<TipoAnuncios> TiposAnuncio { get; set; }
        public DbSet<Periodos> PeriodoAnuncios { get; set; }
        public DbSet<Localidades> Localidades { get; set; }
        public DbSet<Caracteristicas> Caracteristicas { get; set; }
        public DbSet<NombreAnuncios> NombreAnuncios { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<AnuncioTrabajo> AnuncioTrabajo { get; set; }

        public DbSet<PaymentData> pagosAnuncios { get; set; }
        public DbSet<PaymentMethod> metodosDePago { get; set; }

        public DbSet<Merchant> datosComercio { get; set; }
        public DbSet<Extra> datosTarjeta { get; set; }
        public DbSet<InfoImagen> InfoImagenes { get; set; }
        public DbSet<CatalogoEmpresa> CatalogoEmpresas { get; set; }


    }

}



