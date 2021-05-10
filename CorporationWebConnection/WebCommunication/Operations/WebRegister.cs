using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CorporationWebConnection.WebCommunication.Operations
{
    internal class WebRegister: Operation<WebRequest>
    {
        private string _operationPath = @"register.php";

        protected override string OperationPath
        {
            get => _operationPath;
            set => _operationPath = value;
        }

        protected override FormUrlEncodedContent Content { get; set; }

        public WebRegister(string username)
        {
            Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", username)
            });

            
        }
    }
}
