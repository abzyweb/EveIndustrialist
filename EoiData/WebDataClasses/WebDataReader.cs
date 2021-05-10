using EoiData.FileSystemDataClasses;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace EoiData.WebDataClasses
{
    internal static class WebDataReader
    {
        internal static string GetAuthenticationCode(Guid guid)
        {
            var server = @"http://yourUrlHere.com/EveOnline/Authentication/?";

            using (var client = new HttpClient())
            {
                server += @"action=request&guid=";
                server += WebUtility.UrlEncode(guid.ToString());

                var result = client.GetAsync(server).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;

                TextReader input = new StringReader(resultContent);

                XmlSerializer serializer = new XmlSerializer(typeof(RawWebDataAuthentication), new XmlRootAttribute("request"));
                
                var webRequest = serializer.Deserialize(input) as RawWebDataAuthentication;
                input.Close();

                if (webRequest != null)
                    return webRequest.auth?.code;
            }

            return string.Empty;
        }

        internal static bool GetVersionValidation(string version)
        {
            
            var server = @"http://yourUrlHere.com/EveOnline/Authentication/versionCheck.php?";

            using (var client = new HttpClient())
            {
                server += @"version=";
                server += WebUtility.UrlEncode(version);

                var result = client.GetAsync(server).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;

                TextReader input = new StringReader(resultContent);

                XmlSerializer serializer = new XmlSerializer(typeof(RawWebDataAuthentication), new XmlRootAttribute("request"));

                var webRequest = serializer.Deserialize(input) as RawWebDataAuthentication;
                input.Close();

                if (webRequest != null)
                    return webRequest.status == "ok";
            }

            return false;
        }

        internal static RawAccessTokenResponse RefreshAccessToken(string refreshToken)
        {
            var server = @"https://login.eveonline.com/v2/oauth/token";

            // now we can use the authorization code to grab an access token.
            using (var client = new WebClient())
            {
                // build and apply the authorisation header.
                string clientid = "f2c8b1103d0b4bafb0eceddee3235691";
                //string secret = "Y3cyJlPlpEc94EzD2pEZjfGPFiJW6vdxdyQCmzZz";
                //string auth_header = $"{clientid}:{secret}";
                //string auth_header_64 = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(auth_header))}";
                // client.Headers[HttpRequestHeader.Authorization] = auth_header_64;
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                client.Headers[HttpRequestHeader.Host] = "login.eveonline.com";

                // build the post parameters.
                var post_params = new NameValueCollection();
                post_params.Add("grant_type", "refresh_token");
                post_params.Add("refresh_token", refreshToken);
                post_params.Add("client_id", clientid);

                // post info and grab the response.
                try
                {
                    byte[] response = client.UploadValues(server, "POST", post_params);
                    string actual_response = Encoding.UTF8.GetString(response);

                    var tokenResponse = new JavaScriptSerializer().Deserialize<RawAccessTokenResponse>(actual_response);

                    if (tokenResponse != null)
                    {
                        tokenResponse.Timestamp = DateTime.Now;
                        return tokenResponse;
                    }
                }
                catch (WebException e)
                {
                    return null;
                }
            }

            return null;
        }

        internal static RawAccessTokenResponse GetUserAccessToken(string code)
        {
            var server = @"https://login.eveonline.com/v2/oauth/token";

            // now we can use the authorization code to grab an access token.
            using (var client = new WebClient())
            {
                // build and apply the authorisation header.
                string clientid = "f2c8b1103d0b4bafb0eceddee3235691";
                string secret = "Y3cyJlPlpEc94EzD2pEZjfGPFiJW6vdxdyQCmzZz";
                string auth_header = $"{clientid}:{secret}";
                string auth_header_64 = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(auth_header))}";
                client.Headers[HttpRequestHeader.Authorization] = auth_header_64;

                // build the post parameters.
                var post_params = new NameValueCollection();
                post_params.Add("grant_type", "authorization_code");
                post_params.Add("code", code);

                // post info and grab the response.
                byte[] response = client.UploadValues(server, "POST", post_params);
                string actual_response = Encoding.UTF8.GetString(response);

                var tokenResponse = new JavaScriptSerializer().Deserialize<RawAccessTokenResponse>(actual_response);

                if (tokenResponse != null)
                {
                    tokenResponse.Timestamp = DateTime.Now;
                    return tokenResponse;
                }
                    
            }

            return null;
        }
    }

    public class RawAccessTokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class RawWebDataAuthentication
    {
        [XmlAttribute("status")]
        public string status { get; set; }
        [XmlAttribute("error")]
        public string error { get; set; }
        [XmlElement("auth")]
        public RawAuth auth { get; set; }
    }

    public class RawAuth
    {
        [XmlAttribute("code")]
        public string code { get; set; }
    }
}
