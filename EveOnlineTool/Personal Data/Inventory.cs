using EveOnlineIndustrialist.Eve_Data;
using EveOnlineIndustrialist.EveData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EveOnlineTool.Personal_Data
{
    public class Inventory
    {
        [XmlElement]
        public ObservableCollection<InventoryItem> Items { get; set; }

        public Inventory()
        {
            Items = new ObservableCollection<InventoryItem>();
        }

        internal void Add(RawTypeId type, int quantity, double price)
        {
            var item = Items.FirstOrDefault(x => x.TypeId == type.typeID && x.Price == price);
            if (item == null)
            {
                item = new InventoryItem();
                item.TypeId = type.typeID;
                item.Price = price;

                Items.Add(item);
            }

            item.Quantity += quantity;
        }

        internal List<InventoryItem> GetItemsByTypeId(int? typeId)
        {
            if (typeId == null)
                return new List<InventoryItem>();

            var result = Items.Where(x => x.TypeId == typeId);
            return result.ToList();
        }

        internal InventoryItem GetLowestPriceItemByTypeId(int? typeId)
        {
            if (typeId == null)
                return null;

            var itemsInInventory = PersonalData.Inventory.GetItemsByTypeId(typeId);
            if (itemsInInventory.Any())
            {
                return itemsInInventory.FirstOrDefault(x => x.Price == itemsInInventory.Min(y => y.Price));
            }

            return null;
        }
    }

    public class InventoryItem
    {
        [XmlAttribute]
        public int TypeId { get; set; }
        [XmlAttribute]
        public int Quantity { get; set; }
        [XmlAttribute]
        public double Price { get; set; }
    }
}
