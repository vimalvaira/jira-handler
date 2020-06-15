using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiraHandler.DataContracts
{
    public class IssueDTO
    {
        public string expand { get; set; }
        public int id { get; set; }
        public string  self { get; set; }
        public string key { get; set; }        
        public Fields fields { get; set; }  
       
    }

   

}