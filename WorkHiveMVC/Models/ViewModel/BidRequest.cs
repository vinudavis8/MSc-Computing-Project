using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class BidRequest
    {
        public int BidId { get; set; }
        public string UserId { get; set; }
        public int JobId { get; set; }
        public string Description { get; set; }
        public int BidAmount { get; set; }
        public DateTime ExpectedDate { get; set; }
        public string Status { get; set; }
    }
}
