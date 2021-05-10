using CorporationWebConnection.Constants;
using CorporationWebConnection.WebCommunication;
using CorporationWebConnection.WebCommunication.CorporationWebClasses;
using CorporationWebConnection.WebCommunication.Operations;
using CorporationWebConnection.WebCommunication.Operations.gets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporationWebConnection
{
    public class CorporationWebInterface
    {
        public static bool RegisterUser(string name)
        {
            var registerRequest = new WebRegister(name);
            var result = registerRequest.Request();
            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Status == WebRequestStatus.Ok;
        }

        public static bool LoginUser(string name)
        {
            var loginRequest = new WebLogin(name);
            var result = loginRequest.Request();
            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Status == WebRequestStatus.Ok;
        }

        public static bool SetBlueprint(string username, CorporationWebBlueprint bp)
        {
            var setBlueprintRequest = new WebBlueprints(WebRequestType.Set, username, bp);
            var result = setBlueprintRequest.Request();
            if (result == null)
                return false;

            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Status == WebRequestStatus.Ok;
        }

        public static List<WebBlueprint> GetBlueprints(string username)
        {
            var getBlueprintRequest = new WebBlueprints(WebRequestType.Get, username, null);
            var result = getBlueprintRequest.Request();
            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Blueprints;
        }

        public static bool CreateContract(string username, int id, int volume, decimal price, CorporationWebContractTypes contractType, bool materialIncluded, bool blueprintIncluded, string description, bool enablePartition)
        {
            var tmpCorpWebContract = new CorporationWebContract();
            tmpCorpWebContract.BlueprintId = id;
            tmpCorpWebContract.ContractType = contractType;
            tmpCorpWebContract.Volume = volume;
            tmpCorpWebContract.Price = price;

            if (contractType == CorporationWebContractTypes.Buy)
                tmpCorpWebContract.Client = username;
            else if (contractType == CorporationWebContractTypes.Sell)
                tmpCorpWebContract.Contractor = username;

            tmpCorpWebContract.State = CorporationWebContractStates.Pending;
            tmpCorpWebContract.MaterialIncluded = materialIncluded;
            tmpCorpWebContract.BlueprintIncluded = blueprintIncluded;
            tmpCorpWebContract.Destination = -1;
            tmpCorpWebContract.Description = description;
            tmpCorpWebContract.EnablePartition = enablePartition;

            var createContractRequest = new WebContracts(WebRequestType.Set, username, tmpCorpWebContract);
            var result = createContractRequest.Request();
            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Status == WebRequestStatus.Ok;
        }

        public static bool FinishContract(string username, long id)
        {
            var tmpCorpWebContract = new CorporationWebContract();
            tmpCorpWebContract.Id = id;

            var createContractRequest = new WebContracts(WebRequestType.Set, username, tmpCorpWebContract, false, false, true);
            var result = createContractRequest.Request();
            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Status == WebRequestStatus.Ok;
        }

        public static bool DeleteContract(string username, long id)
        {
            var tmpCorpWebContract = new CorporationWebContract();
            tmpCorpWebContract.Id = id;

            var createContractRequest = new WebContracts(WebRequestType.Set, username, tmpCorpWebContract, true);
            var result = createContractRequest.Request();
            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Status == WebRequestStatus.Ok;
        }

        public static bool AcceptContract(string username, long id, int selectedVolume)
        {
            var tmpCorpWebContract = new CorporationWebContract();
            tmpCorpWebContract.Id = id;
            tmpCorpWebContract.Volume = selectedVolume;

            var createContractRequest = new WebContracts(WebRequestType.Set, username, tmpCorpWebContract, false, true);
            var result = createContractRequest.Request();
            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Status == WebRequestStatus.Ok;
        }

        public static List<CorporationWebContract> GetContracts(string username)
        {
            var contracts = new List<CorporationWebContract>();
            var createContractRequest = new WebContracts(WebRequestType.Get, username, null);
            var result = createContractRequest.Request();
            if (result.Status == WebRequestStatus.Ok)
            {
                foreach (var contract in result.Contracts)
                    contracts.Add(new CorporationWebContract(contract));
            }
            else
            {

            }
            
            return contracts;
            
        }

        public static List<WebBlueprint> GetBlueprints(string username, string owner)
        {
            var getBlueprintRequest = new WebBlueprints(WebRequestType.Get, username, null, owner);
            var result = getBlueprintRequest.Request();
            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Blueprints;
        }

        public static bool DeleteBlueprint(string username, int id)
        {
            var bp = new CorporationWebBlueprint(id, null, 0, 0, false);
            var setBlueprintRequest = new WebBlueprints(WebRequestType.Set, username, bp, null, true);
            var result = setBlueprintRequest.Request();
            if (result.Status == WebRequestStatus.Failed)
            {

            }

            return result.Status == WebRequestStatus.Ok;
        }
    }
}
