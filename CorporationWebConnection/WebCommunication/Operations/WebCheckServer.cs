using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorporationWebConnection.WebCommunication.Operations
{
    public class WebCheckServer : Operation<WebRequest>
    {
        private string _operationPath = @"checkServer.php";

        protected override string OperationPath
        {
            get => _operationPath;
            set => _operationPath = value;
        }

        protected override FormUrlEncodedContent Content { get; set; }
        public override string XmlRoot { get; set; }

        public WebCheckServer(Uri server)
        {
            this.BaseUrl = server.ToString();
            if (!this.BaseUrl.EndsWith("/"))
                this.BaseUrl += "/";

            XmlRoot = "request";
        }
    }
}
