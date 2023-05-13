using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{

    public class VMDashboard
    {
        public IEnumerable<User> PopularFreelancers { get; set; }
        public IEnumerable<Job> RecentJobs { get; set; }
        public IEnumerable<Category> PopularCategories { get; set; }
    }
}
