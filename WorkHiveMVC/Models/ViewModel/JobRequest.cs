using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class JobRequest
    {
        public int JobId { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Budget { get; set; }
        public string SkillTags { get; set; }
        public string JobType { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DatePosted { get; set; }
    }
    public class UpdateJobRequest
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Budget { get; set; }
        public string SkillTags { get; set; }
        public string JobType { get; set; }
        public DateTime Deadline { get; set; }
    }
}
