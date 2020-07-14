namespace Persistence.Migrations
{
    using Model.ConfiguraciónPlataforma;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            base.Seed(context);

            context.Caracteristicas.AddOrUpdate(
               x => x.id,
                new Caracteristicas() { id = 1, nombre = "Título", estado = true },
                new Caracteristicas() { id = 2, nombre = "Información de contacto", estado = true },
                new Caracteristicas() { id = 3, nombre = "Descripción (Máx. 500 caracteres)", estado = true },
                new Caracteristicas() { id = 4, nombre = "Imagen", estado = true },
                new Caracteristicas() { id = 5, nombre = "Imagen diseñada por Consigueloo", estado = true },
                new Caracteristicas() { id = 6, nombre = "Catálogo", estado = true },
                new Caracteristicas() { id = 7, nombre = "Publicidad en redes sociales", estado = true }
                ) ;
            context.NombreAnuncios.AddOrUpdate(
               x => x.id,
                new NombreAnuncios() { id = 1, nombre = "Aventurero", caracteristicas=context.Caracteristicas.Where(x=> x.id==1 || x.id==2 || x.id==3).ToList() },
                new NombreAnuncios() { id = 1, nombre = "Emprendedor", caracteristicas = context.Caracteristicas.Where(x => x.id == 1 || x.id == 2 || x.id == 3 || x.id == 4).ToList() },
                new NombreAnuncios() { id = 1, nombre = "Optimista", caracteristicas = context.Caracteristicas.Where(x => x.id == 1 || x.id == 2 || x.id == 3 || x.id == 4 || x.id == 5).ToList() },
                new NombreAnuncios() { id = 1, nombre = "Vencedor", caracteristicas = context.Caracteristicas.Where(x => x.id == 1 || x.id == 2 || x.id == 3 || x.id == 4 || x.id == 5 || x.id == 6).ToList() },
                new NombreAnuncios() { id = 1, nombre = "Exitoso", caracteristicas = context.Caracteristicas.Where(x => x.id == 1 || x.id == 2 || x.id == 3 || x.id == 4 || x.id == 5 || x.id == 6 || x.id == 7).ToList() }

                );

            context.Localidades.AddOrUpdate(
               x => x.id,
                new Localidades() { id = 1, nombre = "Usaquén", estado = true },
                new Localidades() { id = 2, nombre = "Chapinero", estado = true },
                new Localidades() { id = 3, nombre = "Santa Fe", estado = true },
                new Localidades() { id = 4, nombre = "San Cristobal", estado = true },
                new Localidades() { id = 5, nombre = "Usme", estado = true },
                new Localidades() { id = 6, nombre = "Tunjuelito", estado = true },
                new Localidades() { id = 7, nombre = "Bosa", estado = true },
                new Localidades() { id = 8, nombre = "Kennedy", estado = true },
                new Localidades() { id = 9, nombre = "Fontibón", estado = true },
                new Localidades() { id = 10, nombre = "Engativá", estado = true },
                new Localidades() { id = 11, nombre = "Suba", estado = true },
                new Localidades() { id = 12, nombre = "Barrios Unidos", estado = true },
                new Localidades() { id = 13, nombre = "Teusaquillo", estado = true },
                new Localidades() { id = 14, nombre = "Los Mártires", estado = true },
                new Localidades() { id = 15, nombre = "Antonio Nariño", estado = true },
                new Localidades() { id = 16, nombre = "Puente Aranda", estado = true },
                new Localidades() { id = 17, nombre = "	La Candelaria", estado = true },
                new Localidades() { id = 18, nombre = "	Rafael Uribe Uribe", estado = true },
                new Localidades() { id = 19, nombre = "Ciudad Bolívar", estado = true },
                new Localidades() { id = 20, nombre = "Sumapaz", estado = true }
                );
            context.PeriodoAnuncios.AddOrUpdate(
             x => x.id,
              new Periodos() { id = 1, nombre = "Mensual", estado = true },
              new Periodos() { id = 2, nombre = "Trimestral", estado = true },
              new Periodos() { id = 3, nombre = "Anual", estado = true }
              );

            SaveChanges(context);
        }
            /// <summary>
            /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
            /// </summary>
            /// <param name="context">The context.</param>
            public void SaveChanges(DbContext context)
            {
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    throw new DbEntityValidationException(
                        "Entity Validation Failed - errors follow:\n" +
                        sb.ToString(), ex
                    ); // Add the original exception as the innerException
                }
            }
        }
    
}
