using CorporationWebConnection.Constants;
using CorporationWebConnection.WebCommunication.CorporationWebClasses;
using EoiData.Constants;
using EoiData.EoiClasses;
using EoiData.FileSystemDataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.EoiDataClasses
{
    internal class EoiDataContract
    {
        private CorporationWebContract _coporationWebContract;
        private EoiDataBlueprint _blueprint;
        private EoiContract _eoiContract;

        public long Id { get; }
        public bool Synchronized { get; internal set; }

        public EoiDataContract(CorporationWebContract contract)
        {
            this.Id = contract.Id;

            _coporationWebContract = contract;
            _blueprint = EoiDataInterface.GetBlueprint(contract.BlueprintId);
            
        }

        internal void Dispose()
        {
            _blueprint = null;
            _coporationWebContract = null;
            _eoiContract = null;
        }

        internal EoiContract GetEoiContract()
        {
            if (_eoiContract == null)
            {
                _eoiContract = new EoiContract();

                InitializeEoiContract();
            }

            return _eoiContract;
        }

        private void InitializeEoiContract()
        {
            _eoiContract.Blueprint = _blueprint.GetName();
            _eoiContract.Volume = _coporationWebContract.Volume;
            _eoiContract.Price = _coporationWebContract.Price;
            _eoiContract.Client = _coporationWebContract.Client;
            _eoiContract.Contractor = _coporationWebContract.Contractor;
            _eoiContract.Description = _coporationWebContract.Description;
            _eoiContract.Destination = _coporationWebContract.Destination;
            _eoiContract.MaterialIncluded = _coporationWebContract.MaterialIncluded;
            _eoiContract.BlueprintIncluded = _coporationWebContract.BlueprintIncluded;
            _eoiContract.EnablePartition = _coporationWebContract.EnablePartition;

            switch (_coporationWebContract.State)
            {
                case CorporationWebContractStates.Pending:
                    _eoiContract.State = ContractStates.Pending;
                    break;
                case CorporationWebContractStates.Accepted:
                    _eoiContract.State = ContractStates.Accepted;
                    break;
                case CorporationWebContractStates.Finished:
                    _eoiContract.State = ContractStates.Finish;
                    break;
                default:
                    break;
            }

            if (_coporationWebContract.ContractType == CorporationWebContractTypes.Buy)
                _eoiContract.OrderType = ContractType.Buy;
            else if (_coporationWebContract.ContractType == CorporationWebContractTypes.Sell)
                _eoiContract.OrderType = ContractType.Sell;


        }

        internal EoiDataBlueprint GetEoiDataBlueprint()
        {
            return _blueprint;
        }


        internal bool Synchronize(CorporationWebContract corpContract)
        {
            var updated = false;

            if (_coporationWebContract == null || _eoiContract == null)
                throw new InvalidOperationException("Synchronizing a Corporation Contract failed");

            _coporationWebContract = corpContract;

            if (_coporationWebContract.Parent != _eoiContract.Parent)
            {
                _eoiContract.Parent = _coporationWebContract.Parent;
                updated = true;
            }
            if (_coporationWebContract.Volume != _eoiContract.Volume)
            {
                _eoiContract.Volume = _coporationWebContract.Volume;
                updated = true;
            }
            if (_coporationWebContract.Price != _eoiContract.Price)
            {
                _eoiContract.Price = _coporationWebContract.Price;
                updated = true;
            }
            if (_coporationWebContract.Contractor != _eoiContract.Contractor)
            {
                _eoiContract.Contractor = _coporationWebContract.Contractor;
                updated = true;
            }
            switch (_coporationWebContract.State)
            {
                case CorporationWebContractStates.Pending:
                    if (_eoiContract.State != ContractStates.Pending)
                    {
                        _eoiContract.State = ContractStates.Pending;
                        updated = true;
                    }
                    break;
                case CorporationWebContractStates.Accepted:
                    if (_eoiContract.State != ContractStates.Accepted)
                    {
                        _eoiContract.State = ContractStates.Accepted;
                        updated = true;
                    }
                    break;
                case CorporationWebContractStates.Finished:
                    if (_eoiContract.State != ContractStates.Finish)
                    {
                        _eoiContract.State = ContractStates.Finish;
                        updated = true;
                    }
                    break;
                default:
                    break;
            }
            
            if (_coporationWebContract.BlueprintIncluded != _eoiContract.BlueprintIncluded)
            {
                _eoiContract.BlueprintIncluded = _coporationWebContract.BlueprintIncluded;
                updated = true;
            }
            if (_coporationWebContract.MaterialIncluded != _eoiContract.MaterialIncluded)
            {
                _eoiContract.MaterialIncluded = _coporationWebContract.MaterialIncluded;
                updated = true;
            }
            if (_coporationWebContract.EnablePartition != _eoiContract.EnablePartition)
            {
                _eoiContract.EnablePartition = _coporationWebContract.EnablePartition;
                updated = true;
            }
            if (_coporationWebContract.Destination != _eoiContract.Destination)
            {
                _eoiContract.Destination = _coporationWebContract.Destination;
                updated = true;
            }
            if (_coporationWebContract.Description != _eoiContract.Description)
            {
                _eoiContract.Description = _coporationWebContract.Description;
                updated = true;
            }

            this.Synchronized = true;

            return updated;
        }

        
    }
}
