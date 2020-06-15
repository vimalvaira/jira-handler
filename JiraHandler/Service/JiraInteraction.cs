using System;
using System.Text.RegularExpressions;
using JiraHandler.Utility;
using JiraHandler.DataContracts;
using System.Linq;
using System.Collections.Generic;

namespace  JiraHandler.Service
{

    public class JiraIntraction : BaseIntraction
    {

        public List<ProjectIssue> GetProjectList()
        {
           

           try {
                var apiResponse = GetObject <List<ProjectIssue>>("project");
                return apiResponse;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public List<IssueType> GetIssuesTypes()
        {


            try
            {
                var apiResponse = GetObject<List<IssueType>>("issuetype");
                return apiResponse;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public List<Users> GetUsers(string projectKey)
        {
            try
            {
                var apiResponse = GetObject<List<Users>>($"user/assignable/multiProjectSearch?projectKeys={ projectKey }");
                return apiResponse;

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public IssueDTO GetIssue(string  KeyId)
        {
            try
            {
                var apiResponse = GetObject<IssueDTO>($"issue/{KeyId}");
                return apiResponse;

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public BulkIssueResponse CreateBulkIssue(IssueUpdateDTO issueUpdateDTO)
        {
        
            try
            {
                var apiResponse = PostObject<BulkIssueResponse>($"issue/bulk", issueUpdateDTO);
                return apiResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public string GetIssueList()
        //{
        //    try
        //    {
        //        var apiResponse = GetObject<MasterIssue>("search?jql=assignee=shanky&jain");
        //        if (apiResponse == null)
        //        {
        //            // return new HttpStatusCodeResult(500);
        //            return "Something Went Wrong!";
        //        }

        //        return CreateResponse.CreateIssueListDisciption(apiResponse);

        //        // StringBuilder sb = new StringBuilder();
        //        // foreach (var issue in apiResponse.issues)
        //        // {
        //        //     sb.Append(issue.key);
        //        //     sb.Append("\n");
        //        // }

        //        //return sb.ToString();  
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.InnerException.Message;
        //    }

        //}



        //public string GetIssueById(string key)
        //{
        //    try
        //    {
        //        var apiResponse = GetObject<Issue>($"issue/WELL-{key}");
        //        if (apiResponse == null)
        //        {
        //            // return new HttpStatusCodeResult(500);
        //            return "Something Went Wrong!";
        //        }



        //        return CreateIssueDisciption(apiResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.InnerException.Message;
        //    }

        //}


        //public string GetCommentsById(string key)
        //{
        //    try
        //    {
        //        var apiResponse = GetObject<IssueComments>($"issue/WELL-{key}/comment");
        //        if (apiResponse == null)
        //        {
        //            return "Something Went Wrong!";
        //        }
        //        return CreateIssueComments(apiResponse);

        //        //StringBuilder strResponse = new StringBuilder();
        //        //foreach (var comment in apiResponse.comments)
        //        //{
        //        //    strResponse.Append(comment.body);
        //        //}
        //        //return strResponse.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.InnerException.Message;
        //    }
        //    //  return string.Empty;
        //}

        //public string GetAcceptenceCriteria(string key)
        //{
        //    try
        //    {
        //        var apiResponse = GetObject<Issue>($"issue/WELL-{key}");
        //        if (apiResponse == null)
        //        {
        //            // return new HttpStatusCodeResult(500);
        //            return "Something Went Wrong!";
        //        }



        //        return CreateAcceptenceCriteria(apiResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.InnerException.Message;
        //    }



        //}


        //public string AddWorkLogById(string IssueId, string timelog)
        //{
        //    try
        //    {
        //        var PostTimeLog = new PostTimeLog();
        //        PostTimeLog.comment = "";
        //        PostTimeLog.started = $"{DateTime.Now.ToString("yyyy-MM-dd")}T03:55:39.499+0000";
        //        var resultString = Regex.Match(timelog, @"\d+").Value;
        //        var timeInt = Convert.ToInt32(resultString);
        //        PostTimeLog.timeSpentSeconds = Convert.ToString(timeInt * 3600);
        //        var apiresponse = PostObject<PostTimeLog>($"issue/well-{IssueId}/worklog", PostTimeLog);
        //        if (apiresponse != null)
        //        {
        //            return "Done :)";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message.ToString();
        //    }
        //    return "Something Went worng";
        //}


        //public string AddCommentById(LuisResult result)
        //{
        //    try
        //    {
        //        string ticketComment = string.Empty;
        //        string key = string.Empty;
        //        foreach (var entity in result.Entities)
        //        {
        //            if (entity.Type.ToUpper() == "COMMENTMESSAGE")
        //            {
        //                ticketComment = entity.Entity;
        //            }
        //            if (entity.Type.ToUpper() == "TICKETNUMBER")
        //            {
        //                key = entity.Entity;
        //            }
        //        }

        //        var apiresponse = PostObject<AddComment>($"issue/well-{key}/comment", ticketComment);
        //        if (apiresponse == null)
        //        {
        //            return "something went wrong!";
        //        }
        //        return apiresponse.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.InnerException.Message;
        //    }
        //}
    }
}