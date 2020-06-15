using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JiraHandler.Utility;
using System.Web;
using static System.Web.HttpContext;

namespace JiraHandler.Service
{

    public class BaseIntraction
    {
         protected string apiUrlBase = "https://xxx.atlassian.net/rest/api/2/";
         // protected string apiUrlBase = "https://quovantislabs.atlassian.net/rest/api/2/";

        protected T GetObject<T>(string extendedUrl)
        {
            string jsonResult;
            string Url = apiUrlBase + extendedUrl;
            using (var client = new HttpClient())
            {
               
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " +Helper.GetEncodedCredentials(Current.Session["UserName"].ToString(), Current.Session["Password"].ToString()));
                var response = client.GetAsync(Url).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    jsonResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            return JsonConvert.DeserializeObject<T>("");
        }


        protected T DeleteObject<T>(string extendedUrl)
        {

            string jsonResult;
            string Url = apiUrlBase + extendedUrl;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " + Helper.GetEncodedCredentials(ConfigurationManager.AppSettings["JiraUser"], ConfigurationManager.AppSettings["JiraPassword"]));
                var response = client.DeleteAsync(Url).GetAwaiter().GetResult();

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }
                jsonResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            return JsonConvert.DeserializeObject<T>(jsonResult);
        }


        protected T PostObject<T>(string extendedUrl, object data) where T : class
        {
            string jsonResult;
            string Url = apiUrlBase + extendedUrl;

            // CommentNew newc = new CommentNew { body = data };

            // StringContent queryString = new StringContent(, Encoding.UTF8, "application/json");
            StringContent queryString = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                if (!extendedUrl.Equals("Account/Login", StringComparison.OrdinalIgnoreCase))
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " + Helper.GetEncodedCredentials(Current.Session["UserName"].ToString(), Current.Session["Password"].ToString()));
                var response = client.PostAsync(Url, queryString).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }

                else if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    jsonResult = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<T>(jsonResult);
                }
            }
            return null;
        }

        protected async Task<T> PostFile<T>(string extendedUrl, MultipartFormDataContent uploadContent) where T : class
        {
            string jsonResult;
            string Url = apiUrlBase + extendedUrl;

            using (var client = new HttpClient())
            {
                if (!extendedUrl.Equals("Account/Login", StringComparison.OrdinalIgnoreCase))

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " + Helper.GetEncodedCredentials(ConfigurationManager.AppSettings["JiraUser"], ConfigurationManager.AppSettings["JiraPassword"]));

                var response = await client.PostAsync(Url, uploadContent);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException();
                }

                else if (response.StatusCode == HttpStatusCode.OK)
                {
                    jsonResult = await response.Content.ReadAsStringAsync();
                    try
                    {
                        return JsonConvert.DeserializeObject<T>(jsonResult);
                    }
                    catch { }
                }
            }
            return null;
        }

    }
}