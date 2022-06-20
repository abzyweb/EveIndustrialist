using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace CorporationWebConnection.WebCommunication.Operations
{
    public abstract class Operation<T>
    {
        // protected string BaseUrl { get; set; } = @"http://yourUrlHere.com";
        protected string BaseUrl { get; set; } = @"http://www.mobilies.at/EveOnline/";
        protected string OperationUri
        {
            get { return BaseUrl + OperationPath; }
        }

        protected abstract string OperationPath { get; set; }

        protected T Deserialize(string response)
        {
            TextReader input = new StringReader(response);

            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(XmlRoot));

            return (T)serializer.Deserialize(input);
        }

        protected abstract FormUrlEncodedContent Content { get; set; }
        public virtual string XmlRoot { get; set; } = "request";

        public T Request()
        {
            try
            {
                var client = new HttpClient();
                var response = client.PostAsync(OperationUri, Content).Result;
                if (!response.IsSuccessStatusCode)
                    return default(T);

                var responseString = response.Content.ReadAsStringAsync().Result;
                return Deserialize(responseString);
            }
            catch (Exception)
            {

            }

            return default(T);
        }

    }

    
}
