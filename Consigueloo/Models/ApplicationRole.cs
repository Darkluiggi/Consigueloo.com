using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace Consigueloo.Models
{
    public class ApplicationRole: IdentityUserRole
    {
        public int id { get; set; }
        public string nombre { get; set; }

    }
}