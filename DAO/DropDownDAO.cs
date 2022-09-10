using Model.ConfiguracionPlataforma;
using Model.Usuarios;
using Model.ViewModel;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAO
{
    public class DropDownDAO
    {
        private ApplicationDbContext db;
        

        public DropDownDAO()
        {
            db = new ApplicationDbContext();
        }

        public SelectList getPeriodosdd(int? id)
        {
            try
            {
                List<Periodos> periodosList;

                periodosList = db.PeriodoAnuncios
                    .Where(x => x.estado).ToList();

                SelectList lista;

                if (id != null)
                    lista = new SelectList(periodosList, "id", "nombre", id.Value);
                else
                {
                    periodosList.Insert(0, new Periodos { id = 0, nombre = "Seleccione un periodo" });
                    lista = new SelectList(periodosList, "id", "nombre");

                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SelectList getRolesdd(int? id)
        {
            try
            {
                List<Roles> RolesList;

                RolesList = db.Roles
                    .Where(x => x.estado).ToList();

                SelectList lista;

                if (id != null)
                    lista = new SelectList(RolesList, "id", "nombre", id.Value);
                else
                {
                    RolesList.Insert(0, new Roles { id = 0, nombre = "Seleccione un rol" });
                    lista = new SelectList(RolesList, "id", "nombre");
                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<string> getPeriodos()
        {
            try
            {
                string periodo;
                List<Periodos> periodos = db.PeriodoAnuncios.Where(x=> x.estado==true).ToList();
                List<string> response = new List<string>();
                periodos.ForEach(x =>                {

                     periodo = x.nombre;
                    response.Add(periodo);
                });
                return response;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public string getPeriodos(string nombre)
        {
            try
            {
                int id = Int32.Parse(nombre);
                Periodos periodos = db.PeriodoAnuncios.FirstOrDefault(x => x.id == id && x.estado);
                string periodo = periodos.nombre;
                return periodo;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public SelectList getNombreAnunciosdd(int? id)
        {
            try
            {
                List<NombreAnuncios> nombresList
                    ;

                nombresList = db.NombreAnuncios
                    .Where(x => x.estado).ToList();

                SelectList lista;

                if (id != null)
                    lista = new SelectList(nombresList, "id", "nombre", id.Value);
                else
                {
                    nombresList.Insert(0, new NombreAnuncios { id = 0, nombre = "Seleccione un nombre de anuncio" });
                    lista = new SelectList(nombresList, "id", "nombre");

                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string getNombreAnuncios(string nombre)
        {
            try
            {
                int id = Int32.Parse(nombre);
                NombreAnuncios nombreAnuncios = db.NombreAnuncios.FirstOrDefault(x => x.id == id && x.estado);
                string nombreAnuncio = nombreAnuncios.nombre;
                return nombreAnuncio;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<string> getNombresAnuncios()
        {
            try
            {
                string nombreAnuncios;
                List<NombreAnuncios> nombres = db.NombreAnuncios.Where(x => x.estado == true).ToList();
                List<string> response = new List<string>();
                nombres.ForEach(x => {

                    nombreAnuncios = x.nombre;
                    response.Add(nombreAnuncios);
                });
                return response;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public SelectList getLocalidadesdd(int? id)
        {
            try
            {
                List<Localidades> periodosList;

                periodosList = db.Localidades
                    .Where(x => x.estado).ToList();
                periodosList.Insert(0, new Localidades { id = 0, nombre = "Seleccione una localidad" });
                SelectList lista;

                if (id != null)
                    lista = new SelectList(periodosList, "id", "nombre", id.Value);
                else
                {
                    lista = new SelectList(periodosList, "id", "nombre");

                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string getLocalidad(string nombre)
        {
            try
            {
                int id = Int32.Parse(nombre);
                Localidades localidades = db.Localidades.FirstOrDefault(x => x.id == id && x.estado);
                string localidad = localidades.nombre;
                return localidad;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public SelectList getCategoriasdd(int? id)
        {
            try
            {
                List<Categorias> nombresList;

                nombresList = db.Categorias
                    .Where(x => x.estado).ToList();
                nombresList.Insert(0, new Categorias { id = 0, nombre = "Seleccione una categoría de anuncio" });
                SelectList lista;

                if (id != null)

                    lista = new SelectList(nombresList, "id", "nombre", id.Value);
                else
                {
                    
                    lista = new SelectList(nombresList, "id", "nombre");

                }

                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string getCategorias(string nombre)
        {
            try
            {
                int id = Int32.Parse(nombre);
                Categorias nombreAnuncios = db.Categorias.FirstOrDefault(x => x.id == id && x.estado);
                string nombreAnuncio = nombreAnuncios.nombre;
                return nombreAnuncio;
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
