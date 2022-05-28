using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dbo)
        {
            var todate=DateTime.Now;
            var Age=todate.Year-dbo.Year;
            if(dbo.Date>todate.AddYears(-Age))Age--;
            return (Age);
        }
    }
}