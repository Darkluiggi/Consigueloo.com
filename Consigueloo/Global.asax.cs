using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Net.Http;

namespace Consigueloo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly HttpClient client = new HttpClient();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            string sessionId = Session.SessionID;
            HttpContext.Current.Response.Redirect("~/Home/SaveVisitor");
            //var response = client.GetAsync("https://www.Consigueloo.co/Home/SaveVisitor");
        }
        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            string sessionId = Session.SessionID;

            //HttpContext.Current.Response.Redirect("~/Home/LeaveVisitor");
            var response =  client.GetAsync("https://www.Consigueloo.co/Home/SaveVisitor");

        }

        void Application_Error(object sender, EventArgs e)
        {
            //Exception exc = Server.GetLastError();

            //if (exc is HttpUnhandledException)
            //{
            //    // Pass the error on to the error page.
            //    Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
            //}
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            int error = httpException != null ? httpException.GetHttpCode() : 0;


            var myMessage = new MailMessage();
            myMessage.To.Add("luaalvarezve@unal.edu.co");
            myMessage.From = new MailAddress("Administracion@consigueloo.co", "Consigueloo.co");
            myMessage.Subject = "Consigueloo.co error";
            string text = "Exception message: " +  exception.Message + " <br/> Inner exception:" + exception.InnerException + " <br/> Exception Data:" + exception.Data + " <br/> Stack trace:" + exception.StackTrace
                + " <br/> Exception source:" + exception.Source;
            string html = "Exception message: " + exception.Message + " <br/> Inner exception:" + exception.InnerException + " <br/> Exception Data:" + exception.Data + " <br/> Stack trace:" + exception.StackTrace
                + " <br/> Exception source:" + exception.Source;
            myMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            myMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));



            // Create a Web transport for sending email.
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Host = "mail.consigueloo.co";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                // The server requires user's credentials
                // not the default credentials
                smtpClient.UseDefaultCredentials = false;

                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("Administracion@consigueloo.co", "Consigueloo.2022");
                smtpClient.Credentials = credentials;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;


                // Send the email.
                smtpClient.Send(myMessage);
            }

            Server.ClearError();
            Response.Redirect(String.Format("~/Error/?error={0}", error, exception.Message));
        }
    }
}
