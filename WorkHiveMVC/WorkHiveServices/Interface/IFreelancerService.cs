using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WorkHiveServices.Interface
{
    public  interface IFreelancerService
    {
        public Task<User> GetFreelancerDetails(string userId);
        public Task<bool> UpdateProfile(User user);
        public Task<List<Bid>> GetBids(string userId);

    }
}
