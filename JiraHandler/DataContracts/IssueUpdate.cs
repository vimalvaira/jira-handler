using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiraHandler.DataContracts
{


    public class IssueUpdateDTO
    {
        public IssueUpdateDTO()
        {
            this.issueUpdates = new List<IssueUpdate>();
        }
        public List<IssueUpdate> issueUpdates { get; set; }
    }

    public class IssueUpdate //create bulk issue
    {
        public IssueUpdate()
        {
            this.fields = new Fields();
        }
        //  public Update update { get; set; }
        public Fields fields { get; set; }
    }

    public class Add
    {
        public string timeSpent { get; set; }
        public DateTime started { get; set; }
    }

    public class Worklog
    {
        public Add add { get; set; }
    }

    public class Update
    {
        public List<Worklog> worklog { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
    }

    public class Issuetype
    {
        public string id { get; set; }
    }

    public class Assignee
    {
        public string name { get; set; }
    }

    public class Reporter
    {
        public string name { get; set; }
    }

    public class Priority
    {
        public string id { get; set; }
    }

    public class Timetracking
    {
        public string originalEstimate { get; set; }
        public string remainingEstimate { get; set; }
    }

    public class Security
    {
        public string id { get; set; }
    }

    public class Version
    {
        public string id { get; set; }
    }

    public class FixVersion
    {
        public string id { get; set; }
    }

    public class Component
    {
        public string id { get; set; }
    }

    public class Customfield80000
    {
        public string value { get; set; }
    }

    public class Parent
    {
        public string key { get; set; }
    }

    public class Fields
    {
        public Fields()
        {
            this.project = new Project();
            this.parent = new Parent();
            this.assignee = new Assignee();
            this.issuetype = new Issuetype();
            this.timetracking = new  Timetracking();
        }


        public Parent parent { get; set; }
        public Project project { get; set; }
        public Assignee assignee { get; set; }
        public Issuetype issuetype { get; set; }
        public string description { get; set; }

        public string summary { get; set; }

        //public Reporter reporter { get; set; }
        //public Priority priority { get; set; }
        //public List<string> labels { get; set; }
        public Timetracking timetracking { get; set; }
        //public Security security { get; set; }
        //public List<Version> versions { get; set; }
        //public string environment { get; set; }

        //public string duedate { get; set; }
        //public List<FixVersion> fixVersions { get; set; }
        //public List<Component> components { get; set; }
        //public List<string> customfield_30000 { get; set; }
        //public Customfield80000 customfield_80000 { get; set; }
        //public string customfield_20000 { get; set; }
        //public string customfield_40000 { get; set; }
        //public List<string> customfield_70000 { get; set; }
        //public string customfield_60000 { get; set; }
        //public string customfield_50000 { get; set; }
        //public string customfield_10000 { get; set; }
        public string customfield_11400 { get; set; }//start date

        //public string customfield_10032 { get; set; }//due date

    }




}