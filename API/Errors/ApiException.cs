using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiException
    {
        public ApiException(int statusCode, string message=null, string details=null)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.details = details;
        }
         public int statusCode {set;get;}
         public string message {set;get;}
         public string details {set;get;}

      
    }
}