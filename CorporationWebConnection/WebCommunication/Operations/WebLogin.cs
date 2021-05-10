using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorporationWebConnection.WebCommunication.Operations
{
    public class WebLogin : Operation<WebRequest>
    {
        private string _operationPath = @"login.php";

        protected override string OperationPath
        {
            get => _operationPath;
            set => _operationPath = value;
        }

        protected override FormUrlEncodedContent Content { get; set; }
        public override string XmlRoot { get; set; }

        public WebLogin(string username)
        {
            Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username)
            });

            XmlRoot = "request";
        }
    }
}
