using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiraHandler.Models
{
    public class SubtaskViewModel
    {
        public string ParentId { get; set; } //without key
        public string Summary { get; set; } //story summery
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string OrignalEstimates { get; set; }
        public string Assignee { get; set; }
        public string IssueType { get; set; }

        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }
        public string Description { get; set; }
    }

}
