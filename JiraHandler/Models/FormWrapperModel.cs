using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JiraHandler.DataContracts;

namespace JiraHandler.Models
{
    public class FormWrapperModel
    {
        public List<IssueType> IssueTypes { get; set; }
        public List<Users> Users { get; set; }


    }
}