using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Helper
{
    public class PageList<T>:List<T>
    {
        public int CurrentPage {get;set;}
        public int TotalPage { get; set; }
        public int PageSize { get; set; }

        public int TotalCount { get; set; }    


        public PageList(IEnumerable<T> items,int Count,int PageNumber,int pageSize)
        {
            CurrentPage=PageNumber;
            TotalPage=(int)Math.Ceiling(Count/(double)pageSize);
            PageSize=pageSize;
            TotalCount=Count;
            AddRange(items);

        }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int PageNumber,int PageSize)
        {
            var Count=await source.CountAsync();
            var items= await source.Skip((PageNumber-1)*PageSize).Take(PageSize).ToListAsync();
            
            return new PageList<T>(items,Count,PageNumber,PageSize);

        }
    
    }
}