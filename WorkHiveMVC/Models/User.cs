using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string ProfileImage { get; set; }
        public Profile? Profile { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Review> FreelancerReviews { get; set; }
        public virtual ICollection<Review> ClientReviews { get; set; }
        public virtual ICollection<Job> Jobs { get; set; } =
        new List<Job>();
    }
}