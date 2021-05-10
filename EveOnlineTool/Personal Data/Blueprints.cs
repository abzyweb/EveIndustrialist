using EveOnlineIndustrialist.Eve_Data;
using EveOnlineIndustrialist.EveData;
using EveOnlineIndustrialist.Market_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOnlineTool.Personal_Data
{
    public class EoiBlueprints
    {
        private List<Blueprint> _blueprints;

        //public List<Blueprint> Blueprints
        //{
        //    get
        //    {
        //        if (!_blueprints.Any())
        //            LoadBlueprints();

        //        return _blueprints;
        //    }
        //    set
        //    {
        //        _blueprints = value; OnBlueprintsChanged(value);
        //    }
        //}

        //private void LoadBlueprints()
        //{
        //    var allBps = RawEveData.GetAllBlueprints().ToList();

        //    for (int i = 0; i < allBps.Count; i++)
        //    {
        //        var bp = allBps[i];

        //        var type = RawEveData.GetTypeIdById(bp.Value.blueprintTypeID.Value);
        //        if (type != null && type.published)
        //        {
        //            var x = new Blueprint(bp.Value);
        //            _blueprints.Add(x);
        //        }
        //    }
        //}

        public EoiBlueprints()
        {
            _blueprints = new List<Blueprint>();
        }

        private void OnBlueprintsChanged(List<Blueprint> value)
        {
            // Nothing here ...
        }

        //internal Blueprint GetByProductId(int? typeId)
        //{
        //    return Blueprints.FirstOrDefault(x => x.Products.Any(y => y.TypeId == typeId));

        //}

        //public async Task RequestAllAsync()
        //{
        //    foreach (var bp in Blueprints)
        //        await bp.RequestAsync();

        //    await RawMarketData.RequestAllAsync();
        //}

        //public async Task UpdateAllProfits()
        //{
        //    foreach (var bp in Blueprints)
        //        bp.UpdateCalculation();
        //}

        //public List<Blueprint> GetAll()
        //{
        //    return Blueprints;
        //}

        //internal async Task BuildBlueprintConnections()
        //{
        //    await BuildBlueprintConnectionsAsync();
        //}

        //private async Task BuildBlueprintConnectionsAsync()
        //{
        //    foreach (var bp in Blueprints)
        //        bp.BuildBlueprintConnections();
        //}

        //internal void ClearMarketData()
        //{
        //    foreach (var bp in Blueprints)
        //        bp.ClearMarketData();
        //}

        //internal Blueprint GetByTypeId(int type_Id)
        //{
        //    return Blueprints.FirstOrDefault(x => x.TypeId == type_Id);
        //}
    }
}
