using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WorkHiveServices.Interface
{
    public  interface ICategoryService
    {
        public Task<List<Category>> GetCategory();
        public  Task<bool> CreateCategory(string categoryName);

    }
}
