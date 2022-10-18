namespace Persistence.Migrations
{
    using Model.ConfiguracionPlataforma;
    using Model.Usuarios;
    using System;
    using System.Collections.Generic;
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

            List<Caracteristicas> caracteristicas = new List<Caracteristicas>()
            {
                new Caracteristicas() { id = 1, nombre = "Título", estado = true },
                new Caracteristicas() { id = 2, nombre = "Información de contacto", estado = true },
                new Caracteristicas() { id = 3, nombre = "Descripción (Máx. 500 caracteres)", estado = true },
                new Caracteristicas() { id = 4, nombre = "Imagen", estado = true },
                new Caracteristicas() { id = 5, nombre = "Imagen diseñada por Consigueloo", estado = true },
                new Caracteristicas() { id = 6, nombre = "Catálogo", estado = true },
                new Caracteristicas() { id = 7, nombre = "Publicidad en redes sociales", estado = true }
            };
            caracteristicas.ForEach(c =>
            {
                context.Caracteristicas.AddOrUpdate(c);
            });

            List<NombreAnuncios> nombreAnuncios = new List<NombreAnuncios>()
            {
                new NombreAnuncios()
                {
                    id = 1,
                    nombre = "Aventurero",
                    caracteristicas = new List<NombreAnunciosCaracteristicas>()
                    {
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 1,
                            NombreAnuncioId = 1
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 2,
                            NombreAnuncioId = 1
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 3,
                            NombreAnuncioId = 1
                        }
                    },
                    noIncluidas = new List<NombreAnunciosCaracteristicas>()
                    {
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 4,
                            NombreAnuncioId = 2
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 5,
                            NombreAnuncioId = 2
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 6,
                            NombreAnuncioId = 2
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 7,
                            NombreAnuncioId = 2
                        }
                    },
                },
                new NombreAnuncios()
                {
                    id = 2,
                    nombre = "Emprendedor",
                    caracteristicas = new List<NombreAnunciosCaracteristicas>()
                    {
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 1,
                            NombreAnuncioId = 2
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 2,
                            NombreAnuncioId = 2
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 3,
                            NombreAnuncioId = 2
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 4,
                            NombreAnuncioId = 2
                        }
                    }, 
                    noIncluidas = new List<NombreAnunciosCaracteristicas>()
                    {
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 5,
                            NombreAnuncioId = 2
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 6,
                            NombreAnuncioId = 2
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 7,
                            NombreAnuncioId = 2
                        }
                    },
                },
                new NombreAnuncios()
                {
                    id = 3,
                    nombre = "Optimista",
                    caracteristicas = new List<NombreAnunciosCaracteristicas>()
                    {
                        new NombreAnunciosCaracteristicas() {
                            CaracteristicaId = 1,
                            NombreAnuncioId = 3
                        },
                        new NombreAnunciosCaracteristicas()
                         {
                            CaracteristicaId = 2,
                            NombreAnuncioId = 3
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 3,
                            NombreAnuncioId = 3
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 4,
                            NombreAnuncioId = 3
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 5,
                            NombreAnuncioId = 3
                        }
                    },
                    noIncluidas = new List<NombreAnunciosCaracteristicas>()
                    {
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 6,
                            NombreAnuncioId = 2
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 7,
                            NombreAnuncioId = 2
                        }
                    },
                },
                new NombreAnuncios()
                {
                    id = 4,
                    nombre = "Vencedor",
                    caracteristicas = new List<NombreAnunciosCaracteristicas>()
                    {
                        new NombreAnunciosCaracteristicas()
                        {
                        CaracteristicaId = 1,
                        NombreAnuncioId = 4
                    },
                    new NombreAnunciosCaracteristicas()
                    {
                        CaracteristicaId = 2,
                        NombreAnuncioId = 4
                    },
                    new NombreAnunciosCaracteristicas()
                    {
                        CaracteristicaId = 3,
                        NombreAnuncioId = 4
                    },
                    new NombreAnunciosCaracteristicas()
                    {
                        CaracteristicaId = 4,
                        NombreAnuncioId = 4
                    },
                    new NombreAnunciosCaracteristicas()
                    {
                        CaracteristicaId = 5,
                        NombreAnuncioId = 4
                    },
                    new NombreAnunciosCaracteristicas()
                    {
                        CaracteristicaId = 6,
                        NombreAnuncioId = 4
                    }
                },
                    noIncluidas = new List<NombreAnunciosCaracteristicas>()
                    {
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 7,
                            NombreAnuncioId = 2
                        }
                    },
                },
                new NombreAnuncios()
                {
                    id = 5,
                    nombre = "Exitoso",
                    caracteristicas = new List<NombreAnunciosCaracteristicas>()
                    {
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 1,
                            NombreAnuncioId = 5
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 2,
                            NombreAnuncioId = 5
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 3,
                            NombreAnuncioId = 5
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 4,
                            NombreAnuncioId = 5
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 5,
                            NombreAnuncioId = 5
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 6,
                            NombreAnuncioId = 5
                        },
                        new NombreAnunciosCaracteristicas()
                        {
                            CaracteristicaId = 7,
                            NombreAnuncioId = 5
                        }
                    }
                }
            };
            nombreAnuncios.ForEach(na =>
            {
                context.NombreAnuncios.AddOrUpdate(na);
            });

            List<Localidades> localidades = new List<Localidades>()
            {
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
            };
            localidades.ForEach(l =>
            {
                context.Localidades.AddOrUpdate(l);
            });

            List<Periodos> periodos = new List<Periodos>()
            {
              new Periodos() { id = 1, nombre = "Mensual", estado = true },
              new Periodos() { id = 2, nombre = "Trimestral", estado = true },
              new Periodos() { id = 3, nombre = "Anual", estado = true }
            };

            periodos.ForEach(pa =>
            {
                context.PeriodoAnuncios.AddOrUpdate(pa);
            });

            List<Roles> roles = new List<Roles>()
            {
              new Roles() { id = 1, nombre = "Administrador", estado = true },
              new Roles() { id = 2, nombre = "Cliente", estado = true },
              new Roles() { id = 3, nombre = "Empresa", estado = true }
            };

            roles.ForEach(r =>
            {
                context.Roles.AddOrUpdate(r);

            });

            List<TipoAnuncios> tipoAnuncios = new List<TipoAnuncios>()
            {
                new TipoAnuncios() { id = 1, nombreId =1, duracion = "Mensual", precio = "15000", estado = true },
                new TipoAnuncios() { id = 2, nombreId = 1, duracion = "Trimestral", precio = "42000", estado = true },
                new TipoAnuncios() { id = 3, nombreId = 1, duracion = "Anual", precio = "144000", estado = true },
                new TipoAnuncios() { id = 4, nombreId = 2, duracion = "Mensual", precio = "22000", estado = true },
                new TipoAnuncios() { id = 5, nombreId = 2, duracion = "Trimestral", precio = "61500", estado = true },
                new TipoAnuncios() { id = 6, nombreId = 2, duracion = "Anual", precio = "216000", estado = true },
                new TipoAnuncios() { id = 7, nombreId = 3, duracion = "Mensual", precio = "65000", estado = true },
                new TipoAnuncios() { id = 8, nombreId = 3, duracion = "Trimestral", precio = "156000", estado = true },
                new TipoAnuncios() { id = 9, nombreId = 3, duracion = "Anual", precio = "540000", estado = true },
                new TipoAnuncios() { id = 10, nombreId = 4, duracion = "Mensual", precio = "165000", estado = true },
                new TipoAnuncios() { id = 11, nombreId = 4, duracion = "Trimestral", precio = "420000", estado = true },
                new TipoAnuncios() { id = 12, nombreId = 4, duracion = "Anual", precio = "1380000", estado = true }
            };
            tipoAnuncios.ForEach(ta =>
            {
                context.TiposAnuncio.AddOrUpdate(ta);
            });
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
