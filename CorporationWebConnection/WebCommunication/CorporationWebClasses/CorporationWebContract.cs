using CorporationWebConnection.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporationWebConnection.WebCommunication.CorporationWebClasses
{
    public class CorporationWebContract
    {
        private WebContract contract;

        public CorporationWebContract()
        {

        }

        public CorporationWebContract(WebContract contract)
        {
            this.Id = contract.Id;
            this.Parent = contract.ParentId;
            this.BlueprintId = contract.BlueprintId;
            this.ContractType = (CorporationWebContractTypes)contract.ContractType;
            this.Volume = contract.Volume;
            this.Price = contract.Price;
            this.Contractor = contract.Contractor;
            this.Client = contract.Client;
            this.State = (CorporationWebContractStates)contract.State;
            this.BlueprintIncluded = contract.BlueprintIncluded == 1 ? true : false;
            this.MaterialIncluded = contract.MaterialIncluded == 1 ? true : false;
            this.Destination = contract.Destination;
            this.Description = contract.Description;
            this.EnablePartition = contract.EnablePartition == 1 ? true : false;
        }

        public Int64 Id { get; set; }
        public Int64? Parent { get; set; }
        public int BlueprintId { get; set; }
        public CorporationWebContractTypes ContractType { get; set; }
        public int Volume { get; set; }
        public decimal Price { get; set; }
        public string Contractor { get; set; }
        public string Client { get; set; }
        public CorporationWebContractStates State { get; set; }
        public bool BlueprintIncluded { get; set; }
        public bool MaterialIncluded { get; set; }
        public int Destination { get; set; }
        public string Description { get; set; }
        public bool EnablePartition { get; set; }
    }
}
