using EoiData.Constants;
using EoiData.EoiDataClasses;
using EoiData.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoiData.MarketerDataClasses
{
    internal static class MarketerDataInterface
    {
        internal static void RequestBlueprints()
        {
            MarketerDataReader.RequestAll();            
        }

        internal static List<MarketerDataRequest> GetById(int id)
        {
            return MarketerDataReader.CachedRequests.Where(x => x.Id == id).ToList();
        }

        internal static void ImportMarketerDataRequests(List<MarketerDataRequest> marketData)
        {
            if (marketData == null)
                return;

            MarketerDataReader.Import(marketData);
        }

        internal static List<MarketerDataRequest> ExportMarketerDataRequests()
        {
            return MarketerDataReader.CachedRequests;
        }
    }
}
