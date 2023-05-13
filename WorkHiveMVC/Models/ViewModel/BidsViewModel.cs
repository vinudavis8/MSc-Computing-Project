using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class BidsViewModel
    {
        public int BidId { get; set; }
        public int BidAmount { get; set; }
        public string JobName { get; set; }
        public string FreelancerName { get; set; }
        public string FreelancerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpectedDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
