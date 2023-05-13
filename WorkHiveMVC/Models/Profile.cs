using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string? Designation { get; set; }
        public string? Description { get; set; }
        public int? Experience { get; set; }
        public double? HourlyRate { get; set; }
        public string? Skills { get; set; }
        public string? LocationCordinates { get; set; }
        public int? Rating { get; set; }
        public bool ReceiveJobNotifications { get; set; }

    }
}
