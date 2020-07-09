using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Helpers.InfoMensajes
{
    public static class ViewInfoMensaje
    {
        public static void setMensaje(Controller controller, string message, string level)
        {

            controller.ViewBag.level = level;

            controller.ViewBag.message = message;
        }

        public static void setMensaje(Controller controller, string message, string level, bool esTempData)
        {
            if (!esTempData)
            {
                controller.TempData["message"] = message;
                controller.TempData["level"] = level;
            }
            else
            {
               
                controller.ViewBag.level = level;
                controller.ViewBag.message = message;
            }

        }

        public static void setMensaje(Controller controller, string message, string level, string component)
        {

            
            controller.ViewBag.level = level;
            controller.ViewBag.message = message;
            if (component.Contains("#"))
            {
                controller.ViewBag.component = component;
            }
            else
            {
                controller.ViewBag.component = "#" + component;
            }

        }

        public static void setMensajeSession(Controller controller, string message, string level)
        {
            controller.Session.Add("message", message);
            controller.Session.Add("level", level);
        }
    }
}
