using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Redmine {
    /// <summary>
    /// Redmine client object to query Redmine server.
    /// </summary>
    public class RedmineClient {
        private static string szFormat = "{0,12} : {1}";
        private string hostname;
        private uint port;
        private string path;
        private bool useSSL;
        private string username;
        private string password;
        private string api_key;
        
        /// <summary>
        /// Creates a new instance of a Redmine Client object that can be used for querying
        /// an instance of a Redmine Server.
        /// </summary>
        /// <param name="hostname">The IP address of the Redmine Server</param>
        /// <param name="port">The port number the Redmine server is configured on</param>
        /// <param name="path">The path name for the Redmine server</param>
        /// <param name="useSSL">True if server is configured for https, else false</param>
        /// <param name="api_key">Authentication API key for Redmine Account</param>
        public RedmineClient(string hostname, uint port, string path, bool useSSL, string api_key) {
            this.hostname = hostname;
            this.port = port;
            this.path = path;
            this.useSSL = useSSL;
            this.api_key = api_key;
        }

        /// <summary>
        /// Sets the credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void setCredentials(string username, string password) {
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="includeTrackers">if set to <c>true</c>, includes trackers associated with the Project.</param>
        /// <returns></returns>
        public Projects getProjects(bool includeTrackers) {
            Projects projects = getResource<Projects>(ResourceUri.Projects(), null);
            if (includeTrackers) {
                // we need the individual fetches to retrieve the 'trackers' for the projects
                List<Project> detailedProjects = new List<Project>(projects.list.Count);
                foreach (Project o in projects.list) {
                    Project p = getProject(o.Id);
                    detailedProjects.Add(p);
                }
                projects.list = detailedProjects;
            }
            return projects;
        }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Project getProject(int id) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("include", "trackers");

            return getResource<Project>(ResourceUri.Project(id), parameters);
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        public Users getUsers() {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", "100");

            return getResource<Users>(ResourceUri.Users(), parameters);
        }

        /// <summary>
        /// Gets the issues.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="trackerID">The tracker to get the issues for, leave as 0 for all trackers</param>
        /// <param name="priorityID">The priority ID to get the issues for, leave as 0 for all issues</param>
        /// <returns></returns>
        public Issues getIssues(Project project, int trackerID, int priorityID) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("project_id", project.Id.ToString());
            parameters.Add("limit", "100");
            if (trackerID > 0) {
                parameters.Add("tracker_id", trackerID.ToString());
            }
            if (priorityID > 0) {
                parameters.Add("priority_id", priorityID.ToString());
            }
            return getResource<Issues>(ResourceUri.Issues(), parameters);
        }

        /// <summary>
        /// Gets the issue.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Issue getIssue(int id) {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("include", "journals");

            return getResource<Issue>(ResourceUri.Issue(id), parameters);
        }

        /// <summary>
        /// Creates the issue.
        /// </summary>
        /// <param name="newIssue">The new issue.</param>
        /// <returns></returns>
        public Issue createIssue(NewIssue newIssue) {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NewIssue));
            xmlSerializer.Serialize(streamWriter, newIssue, namespaces);

            return postResource<Issue>(ResourceUri.Issues(), null, memoryStream.ToArray());
        }

        /// <summary>
        /// Updates the issue.
        /// </summary>
        /// <param name="updateIssue">The update issue.</param>
        /// <returns></returns>
        public Issue updateIssue(UpdateIssue updateIssue) {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UpdateIssue));
            xmlSerializer.Serialize(streamWriter, updateIssue, namespaces);

            RedmineResponse redmineResponse = putResource(ResourceUri.Issue(updateIssue.id), null, memoryStream.ToArray());
            Console.WriteLine(szFormat, "HTTP Status Code", redmineResponse.StatusCode);
            Console.WriteLine(szFormat, "HTTP Status Description", redmineResponse.StatusDescription);
            Console.WriteLine(szFormat, "HTTP Response Text", redmineResponse.ResponseText);
            if (redmineResponse.StatusCode == HttpStatusCode.OK) {
                // fetch the issue with the updates
                return getIssue(updateIssue.id);
            }
            return null;
        }

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameteres">The parameteres.</param>
        /// <returns></returns>
        private T getResource<T>(string resource, Dictionary<string, string> parameteres) {
            string url = buildURL(resource, parameteres);
            Console.WriteLine("~~~ " + url);
            try {
                return (T)doGet(url, new XmlSerializer(typeof(T)));
            } catch (WebException exception) {
                StackTrace stackTrace = new StackTrace();
                string methodName = stackTrace.GetFrame(1).GetMethod().Name;
                string errorMessage = string.Format("{0} failed for {1} : {2}", methodName, resource, exception.Message);
                throw new RedmineException(errorMessage);
            }
        }

        /// <summary>
        /// Posts the resource.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resource">The resource.</param>
        /// <param name="parameteres">The parameteres.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private T postResource<T>(string resource, Dictionary<string, string> parameteres, byte[] data) {
            string url = buildURL(resource, parameteres);
            Console.WriteLine("~~~" + url);
            Console.WriteLine("~~~" + UTF8Encoding.UTF8.GetString(data));
            try {
                return (T)doPost(url, new XmlSerializer(typeof(T)), data);
            } catch (WebException exception) {
                StackTrace stackTrace = new StackTrace();
                string methodName = stackTrace.GetFrame(1).GetMethod().Name;
                string errorMessage = string.Format("{0} failed for {1} : {2}", methodName, resource, exception.Message);
                throw new RedmineException(errorMessage);
            }
        }

        /// <summary>
        /// Puts the resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameteres">The parameteres.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private RedmineResponse putResource(string resource, Dictionary<string, string> parameteres, byte[] data) {
            string url = buildURL(resource, parameteres);
            Console.WriteLine("~~~" + url);
            Console.WriteLine("~~~" + UTF8Encoding.UTF8.GetString(data));
            try {
                return doPut(url, data);
            } catch (WebException exception) {
                StackTrace stackTrace = new StackTrace();
                string methodName = stackTrace.GetFrame(1).GetMethod().Name;
                string errorMessage = string.Format("{0} failed for {1} : {2}", methodName, resource, exception.Message);

                HttpWebResponse webResponse = exception.Response as HttpWebResponse;
                if (webResponse != null && webResponse.StatusCode == HttpStatusCode.NotFound) {
                    throw new RedmineObjectNotFoundException(errorMessage);
                }

                throw new RedmineException(errorMessage);
            }
        }

        /// <summary>
        /// Does the get.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        private object doGet(string url, XmlSerializer serializer) {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (username != null && password != null) {
                request.Credentials = new NetworkCredential(username, password);
            }
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse) {
                return serializer.Deserialize(response.GetResponseStream());
            }
        }

        /// <summary>
        /// Does the post.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private object doPost(string url, XmlSerializer serializer, byte[] data) {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (username != null && password != null) {
                request.Credentials = new NetworkCredential(username, password);
            }
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = data.Length;

            using (Stream postStream = request.GetRequestStream()) {
                postStream.Write(data, 0, data.Length);
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse) {
                return serializer.Deserialize(response.GetResponseStream());
            }
        }

        /// <summary>
        /// Does the put.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private RedmineResponse doPut(string url, byte[] data) {
            RedmineResponse redmineResponse = new RedmineResponse();
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (username != null && password != null) {
                request.Credentials = new NetworkCredential(username, password);
            }
            request.Method = "PUT";
            request.ContentType = "application/xml";
            request.ContentLength = data.Length;

            using (Stream postStream = request.GetRequestStream()) {
                postStream.Write(data, 0, data.Length);
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse) {
                redmineResponse.StatusCode = response.StatusCode;
                redmineResponse.StatusDescription = response.StatusDescription;

                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                redmineResponse.ResponseText = streamReader.ReadToEnd();
            }
            return redmineResponse;
        }

        /// <summary>
        /// Builds the URL.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="parameteres">The parameteres.</param>
        /// <returns></returns>
        private string buildURL(string resource, Dictionary<string, string> parameteres) {
            string server_url = string.Format("{0}{1}:{2}/{3}/{4}", useSSL ? "https://" : "http://", hostname, port, path, resource);
            StringBuilder sb = new StringBuilder(server_url);
            sb.Append(string.Format("?key={0}", api_key));
            if (parameteres != null && parameteres.Count > 0) {
                foreach (string key in parameteres.Keys) {
                    sb.Append(string.Format("&{0}={1}", Uri.EscapeUriString(key), Uri.EscapeUriString(parameteres[key])));
                }
            }
            return sb.ToString();
        }

        private class RedmineResponse {
            internal HttpStatusCode StatusCode;
            internal string StatusDescription;
            internal string ResponseText;
        }

        /// <summary>
        /// Gets the issue URL.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public string getIssueURL(int id) {
            return string.Format("{0}{1}:{2}/{3}/issues/{4}", useSSL ? "https://" : "http://", hostname, port, path, id);
        }
    }
}
