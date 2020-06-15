﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiraHandler.DataContracts
{
    public class IssueType
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public bool subtask { get; set; }
        public int avatarId { get; set; }

    }
}