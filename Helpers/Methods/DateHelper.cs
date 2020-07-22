using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Methods
{
    public class DateHelper
    {
        public string stringToMonth(string Mes)
        {
            string Month="";
            if (Mes.Equals("1"))
            {
                Month = "Ene";
            }
            else if (Mes.Equals("2"))
            {
                Month = "Feb";
            }
            else if (Mes.Equals("3"))
            {
                Month = "Mar";
            }
            else if (Mes.Equals("4"))
            {
                Month = "Abr";
            }
            else if (Mes.Equals("5"))
            {
                Month = "May";
            }
            else if (Mes.Equals("6"))
            {
                Month = "Jun";
            }
            else if (Mes.Equals("7"))
            {
                Month = "Jul";
            }
            else if (Mes.Equals("8"))
            {
                Month = "Ago";
            }
            else if (Mes.Equals("9"))
            {
                Month = "Sep";
            }
            else if (Mes.Equals("10"))
            {
                Month = "Oct";
            }
            else if (Mes.Equals("11"))
            {
                Month = "Nov";
            }
            else if (Mes.Equals("12"))
            {
                Month = "Dic";
            }
            return Month;
        }
    }
}
