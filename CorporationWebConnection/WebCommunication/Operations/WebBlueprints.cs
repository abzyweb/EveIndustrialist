using CorporationWebConnection.WebCommunication.CorporationWebClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CorporationWebConnection.WebCommunication.Operations.gets
{
    public class WebBlueprints : Operation<WebBlueprintRequest>
    {
        private string _operationPath = string.Empty;

        protected override string OperationPath
        {
            get => _operationPath;
            set => _operationPath = value;
        }

        protected override FormUrlEncodedContent Content { get; set; }
        public override string XmlRoot { get; set; }

        public WebBlueprints(WebRequestType type, string username, CorporationWebBlueprint blueprint, string owner = null, bool delete = false)
        {
            XmlRoot = "request";

            if (type == WebRequestType.Get)
            {
                if (string.IsNullOrWhiteSpace(owner))
                {
                    Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", username)
                    });
                }
                else
                {
                    Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("owner", owner)
                    });
                }

                _operationPath = @"getBlueprint.php";
            }
            else
            {
                if (delete)
                {
                    Content = new FormUrlEncodedContent(new[]
                   {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("id", blueprint.Id),
                        new KeyValuePair<string, string>("delete", "1")
                    });
                }
                else
                {
                    Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("id", blueprint.Id),
                        new KeyValuePair<string, string>("name", blueprint.Name),
                        new KeyValuePair<string, string>("me", blueprint.ME),
                        new KeyValuePair<string, string>("te", blueprint.TE),
                        new KeyValuePair<string, string>("private", blueprint.Private ? "1" : "0")
                    });
                }

                _operationPath = @"setBlueprint.php";
            }
        }
    }
}
