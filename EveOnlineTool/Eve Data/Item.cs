using EveOnlineIndustrialist.EveData;

namespace EveOnlineIndustrialist.Eve_Data
{
    internal class Item : Market_Data.MarketData
    {
        private RawItem _rawItem;
        private RawTypeId _rawTypeId;

        //public Item(RawItem item) : base()
        //{
        //    if (item == null)
        //    {
        //        Invalid = true;
        //        return;
        //    }

        //    _rawItem = item;
        //    _rawTypeId = RawEveData.GetTypeIdById(_rawItem.typeID.Value);

        //    if (_rawTypeId != null)
        //        TypeId = _rawTypeId.typeID;
        //}

        public bool Invalid { get; private set; }
    }
}