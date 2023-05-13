using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.ViewModel;

namespace WorkHiveServices.Interface
{
    public  interface IHomeService
    {
        public Task<VMDashboard> GetDashboardData();
        public  Task<IDictionary<string, int>> GetDashboardSummary();

    }
}
