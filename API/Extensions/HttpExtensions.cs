using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.Helper;
using System.Text.Json;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response,int CurrentPage,int ItemsPerPage,int TotalItems,int TotalPages)
        {
            var PaginationHeader=new PaginationHeader(CurrentPage,ItemsPerPage,TotalItems,TotalPages);
            var options=new JsonSerializerOptions
            {
                PropertyNamingPolicy=JsonNamingPolicy.CamelCase
            };
            response.Headers.Add("Pagination",JsonSerializer.Serialize(PaginationHeader,options));
            
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");
        }
    }
}