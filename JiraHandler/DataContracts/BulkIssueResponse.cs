using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiraHandler.DataContracts
{
    public class BulkIssueResponse
    {
        public List<Issue> issues { get; set; }
        public List<object> errors { get; set; }
    }
    public class Issue
    {
        public string id { get; set; }
        public string key { get; set; }
        public string self { get; set; }
    }

  
}