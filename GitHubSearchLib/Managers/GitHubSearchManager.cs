using GitHubSearchLib.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GitHubSearchLib.Managers
{
    public static class GitHubSearchManager
    {
        public static GitHubSearchReponse Search(string searchString)
        {
            GitHubSearchReponse result = null;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                string requestUrl = string.Format(ConfigurationManager.AppSettings["GitHubSearchApiUrl"], HttpUtility.UrlEncode(searchString));

                using (var client = new WebClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

                    client.Headers.Clear();

                    client.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                    client.Headers.Add("Accept-Encoding", "deflate, br");
                    client.Headers.Add("Accept-Language", "en-US,en;q=0.9,ru;q=0.8,he;q=0.7");
                    client.Headers.Add("Cache-Control", "max-age=0");
                    client.Headers.Add("Host", "api.github.com");
                    client.Headers.Add("Upgrade-Insecure-Requests", "1");
                    client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.79 Safari/537.36");
                  
                    string responseString = Encoding.UTF8.GetString(client.DownloadData(requestUrl));

                    result = GitHubSearchReponse.FromJson(responseString);
                }
            }

            return result;
        }
    }
}
