using Helper;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WorkHiveServices.Interface;

namespace WorkHiveServices
{
    public class CategoryService : ICategoryService
    {

        public async Task<List<Category>> GetCategory()
        {
            List<Category> list = new List<Category>();
            try
            {
                list = await ApiHelper.GetAsync<List<Category>>("api/Category/GetAll/");
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }
        public async Task<bool> CreateCategory(string categoryName)
        {
            try
            {
                var category = await ApiHelper.PostAsync<Category>("api/Category/", categoryName);
                if (category.CategoryId > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
