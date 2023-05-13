using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ReviewRequest
    {
        public string Description { get; set; }
        public int Rating { get; set; }
        public string FreelancerId { get; set; }
        public string ClientId { get; set; }
    }

}
