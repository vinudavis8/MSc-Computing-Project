using Helper;
using Models;
using Models.ViewModel;
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
    public class HomeService: IHomeService
    {
        public async Task<VMDashboard> GetDashboardData()
        {
            VMDashboard data = new VMDashboard();
            try
            {
                data = await ApiHelper.GetAsync<VMDashboard>("api/Dashboard");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public async Task<IDictionary<string, int>> GetDashboardSummary()
        {
            IDictionary<string, int> data;
            try
            {
                data = await ApiHelper.GetAsync<IDictionary<string, int>>("api/Dashboard/GetDashboardSummary");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

    }
}
