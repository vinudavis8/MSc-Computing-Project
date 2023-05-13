using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        public int ProfileId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string? ProfileImage { get; set; }
        public string? Designation { get; set; }
        public string? Description { get; set; }
        public int? Experience { get; set; }
        public double? HourlyRate { get; set; }
        public string? Skills { get; set; }
        public string? LocationCordinates { get; set; }
        public bool ReceiveJobNotifications { get; set; }

    }
}
