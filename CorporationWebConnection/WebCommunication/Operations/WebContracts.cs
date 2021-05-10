using CorporationWebConnection.WebCommunication.CorporationWebClasses;
using CorporationWebConnection.WebCommunication.Operations.gets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorporationWebConnection.WebCommunication.Operations
{
    internal class WebContracts : Operation<WebContractRequest>
    {
        private string _operationPath = string.Empty;

        protected override string OperationPath
        {
            get => _operationPath;
            set => _operationPath = value;
        }

        protected override FormUrlEncodedContent Content { get; set; }
        public override string XmlRoot { get; set; }

        public WebContracts(WebRequestType type, string username, CorporationWebContract cwc, bool delete = false, bool accept = false, bool finish = false)
        {
            XmlRoot = "request";

            if (type == WebRequestType.Get)
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username)
                });

                _operationPath = @"getContracts.php";
            }
            else
            {
                if (delete)
                {
                    Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("id", cwc.Id.ToString()),
                        new KeyValuePair<string, string>("delete", "1"),
                    });
                }
                else if (accept)
                {
                    Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("id", cwc.Id.ToString()),
                        new KeyValuePair<string, string>("volume", cwc.Volume.ToString()),
                        new KeyValuePair<string, string>("accept", "1"),
                    });
                }
                else if (finish)
                {
                    Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("id", cwc.Id.ToString()),
                        new KeyValuePair<string, string>("finish", "1"),
                    });
                }
                else
                {
                    Content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("parentId", cwc.Parent.ToString()),
                        new KeyValuePair<string, string>("blueprintId", cwc.BlueprintId.ToString()),
                        new KeyValuePair<string, string>("contractType", ((int)cwc.ContractType).ToString()),
                        new KeyValuePair<string, string>("volume", cwc.Volume.ToString()),
                        new KeyValuePair<string, string>("price", cwc.Price.ToString()),
                        new KeyValuePair<string, string>("contractor", cwc.Contractor),
                        new KeyValuePair<string, string>("client", cwc.Client),
                        new KeyValuePair<string, string>("state", ((int)cwc.State).ToString()),
                        new KeyValuePair<string, string>("blueprintIncluded", cwc.BlueprintIncluded ? "1" : "0"),
                        new KeyValuePair<string, string>("materialIncluded", cwc.MaterialIncluded ? "1" : "0"),
                        new KeyValuePair<string, string>("destination", cwc.Destination.ToString()),
                        new KeyValuePair<string, string>("description", cwc.Description),
                        new KeyValuePair<string, string>("enablePartition", cwc.EnablePartition ? "1" : "0"),
                    });
                }

                _operationPath = @"setContract.php";
            }
        }
    }
}
