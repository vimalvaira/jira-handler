using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JiraHandler.Models;
using JiraHandler.Service;
using JiraHandler.DataContracts;
using Newtonsoft.Json;



namespace JiraHandler.Controllers
{
    public class DirectoryController : Controller
    {

        JiraIntraction _jiraInteraction;

        public DirectoryController()
        {
            _jiraInteraction = new JiraIntraction();
        }


        // GET: Directory
        public ActionResult Index(Credentials credentials)
        {
            Session.Add("UserName", credentials.UserName);
            Session.Add("Password", credentials.Password);

            var projects = _jiraInteraction.GetProjectList();

            if (projects == null)
            {
                return RedirectToAction("GetCredentials", "Home");
            }


            return View(projects);

        }

        public bool SetDefaultKey(string Key)
        {
            Session.Add("Key", Key);
            return true;
        }


        public ActionResult Operations()
        {

            return View("OperationList");

        }

        public ActionResult CreateSubTaskPage()
        {
            return View("CreateSubTask");
        }


        public ActionResult GetCreateFormTemplate()
        {
            var formWrapper = new FormWrapperModel();
            formWrapper.IssueTypes = _jiraInteraction.GetIssuesTypes().FindAll(x => x.subtask == true);
            formWrapper.Users = _jiraInteraction.GetUsers(Session["Key"].ToString());
            formWrapper.Users[0] = new Users();
            return PartialView("_CreateFormTemplate", formWrapper);
        }


        [HttpGet]
        public JsonResult GetIssue(string issueNumber)
        {
            return Json(_jiraInteraction.GetIssue($"{Session["Key"]}-{issueNumber}"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateSubtast(IEnumerable<SubtaskViewModel> subtasks)
        {
            if (subtasks.Any())
            {
                var parentIssue = _jiraInteraction.GetIssue($"{Session["Key"].ToString()}-{subtasks.First().ParentId}");
                if (parentIssue != null)
                {
                    var subtasksDTO = new IssueUpdateDTO();
                    foreach (var subtask in subtasks)
                    {
                        var task = new IssueUpdate();
                        task.fields.project.id = parentIssue.fields.project.id;
                        task.fields.parent.key = parentIssue.key;
                        task.fields.assignee.name = subtask.Assignee;
                        task.fields.issuetype.id = subtask.IssueType;
                        task.fields.customfield_11400 = (subtask.StartDate != null) ? subtask.StartDate.Value.ToString("yyyy-MM-dd") : "";
                        task.fields.timetracking.originalEstimate = calulateTime(subtask).ToString(); 
                        task.fields.summary = GenerateSummary(subtask, parentIssue.fields.summary);  
                        task.fields.description = string.IsNullOrWhiteSpace(subtask.Description) ? parentIssue.fields.description : subtask.Summary;
                        subtasksDTO.issueUpdates.Add(task);
                    }
                    var response = _jiraInteraction.CreateBulkIssue(subtasksDTO);
                    return Json(response);

                }
            }
            return Json("500");
        }

        public int calulateTime(SubtaskViewModel subtaskVM)
        {
            var timeInMinutes = 0;

            timeInMinutes += subtaskVM.Day * 7 * 60;
            timeInMinutes += subtaskVM.Hour * 60;
            timeInMinutes += subtaskVM.Minute;

            return timeInMinutes;
        }

        public string GenerateSummary(SubtaskViewModel subtaskVM, string parentSummary)
        {
            string summary = string.Empty;

            if (!string.IsNullOrEmpty(subtaskVM.Summary) && subtaskVM.Summary != "p")
            {
                summary = subtaskVM.Summary;
            }
            else
            {   switch (subtaskVM.IssueType)
                {

                    case "10800": //Analysis
                        summary = "Analysis - " + parentSummary;
                        break;
                    case "10600": //Code/Dev Task
                        summary = "Dev - " + parentSummary;
                        break;
                    case "10011": //Code Review
                        summary = "CR - " + parentSummary;
                        break;
                    case "10201": //QA
                        summary = subtaskVM.Summary == "p" ? "PT - " + parentSummary : "QA - " + parentSummary;
                        break;
                    case "10400": // UT
                        summary = "UT - " + parentSummary;
                        break;
                    default:
                        summary = parentSummary;
                        break;

                }
            }
            return summary;
        }





    }




}
