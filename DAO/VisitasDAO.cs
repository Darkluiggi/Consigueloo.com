using AutoMapper;
using Helpers.InfoMensajes;
using Helpers.Methods;
using Model.ConfiguracionPlataforma;
using Model.Usuarios;
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
    public class VisitasDAO
    {
        ApplicationDbContext db;
        private DateHelper dateHelper;
        private const string entidad = "Visitas";

        public VisitasDAO()
        {
            db = new ApplicationDbContext();
            dateHelper = new DateHelper();
        }

        private List<Visitas> getList()
        {
            return db.Visitas.ToList();
        }
       

        public async void Add(VisitasDTO visitas)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<VisitasDTO, Visitas>();
                });

                IMapper mapper = config.CreateMapper();
                //Mapeo de clase

                Visitas response = mapper.Map<VisitasDTO, Visitas>(visitas);
                Visitas ultVisita = db.Visitas.Where(v => v.IP == response.IP).OrderByDescending(v => v.id).FirstOrDefault();
                if (ultVisita != null)
                {
                    TimeSpan timeSinceLastVisit = response.visitTime - ultVisita.visitTime;
                    if (timeSinceLastVisit.TotalMinutes < 30)
                    {
                        ultVisita.visitTime = DateTime.Now;
                        ultVisita.isActive = true;
                        db.Entry(ultVisita).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Visitas.Add(response);
                    }
                }
                else
                {
                    db.Visitas.Add(response);
                }

               db.SaveChangesAsync();
            }

            catch (Exception ex)
            {

                throw;
            }
        }

       
        public async void EndVisit(VisitasDTO visita)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VisitasDTO, Visitas>();
            });

            IMapper mapper = config.CreateMapper();
            //Mapeo de clase

            Visitas response = mapper.Map<VisitasDTO, Visitas>(visita);
            List<Visitas> ultVisitas = db.Visitas.Where(v => v.IP == response.IP).OrderByDescending(v => v.id).ToList();
            if (ultVisitas != null && ultVisitas.Count > 0)
            {               
                foreach (Visitas ultVisita in ultVisitas)
                {
                    ultVisita.isActive = false;
                    db.Entry(ultVisita).State = EntityState.Modified;

                }
            }

            await db.SaveChangesAsync();

        }

        public void Remove(int id)
        {
            try
            {
                Roles razonsocial = db.Roles.Find(id);
                razonsocial.estado = false;
                db.Entry(razonsocial).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
        }

        public ChartDataDTO ChartAreaData()
        {
            try
            {
                var anuncios = getList();
                var today = DateTime.Today;

                ChartDataDTO result = new ChartDataDTO();
                int max = 0;
                for (int i = 15; i >= 0; i--)
                {

                    DateTime fechaResultado = today.AddDays(-i);
                    var model = anuncios.Where(x => x.visitTime.Day == fechaResultado.Day).ToList();
                    result.datos.Add(model.Count.ToString());
                    string fecha = construirFecha(fechaResultado.Month.ToString(), fechaResultado.Day.ToString());
                    result.periodos.Add(fecha);
                    max = model.Count > max ? model.Count : max;
                    result.max = max + 20;
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
                string month = dateHelper.stringToMonth(Mes);
                string result = month + " " + día;

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
                var anuncios = getList();
                var today = DateTime.Today;
                ChartDataDTO result = new ChartDataDTO();
                int max = 0;
                for (int i = 5; i >= 0; i--)
                {
                    DateTime fechaResultado = today.AddMonths(-i);
                    var model = anuncios.Where(x => x.visitTime.Month == fechaResultado.Month).ToList();
                    result.datos.Add(model.Count.ToString());
                    string Mes = dateHelper.stringToMonth(fechaResultado.Month.ToString());
                    result.periodos.Add(Mes);
                    max = model.Count > max ? model.Count : max;
                    result.max = max + 20;
                }

                return result;
            }
            catch (Exception)
            {

                throw;
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



