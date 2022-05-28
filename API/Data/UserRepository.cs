using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class UserRepository
    {
        private readonly DataContext _DataContext;

        public UserRepository(DataContext DataContext)
        {
            _DataContext=DataContext;

        }

        
        
    }
}