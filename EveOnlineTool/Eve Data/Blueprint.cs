using EveOnlineIndustrialist.EveData;
using EveOnlineIndustrialist.Market_Data;
using EveOnlineTool;
using EveOnlineTool.Eve_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveOnlineIndustrialist.Eve_Data
{
    public class Blueprint : MarketData
    {
        RawBlueprint _rawBlueprint;
        RawTypeId _rawTypeId;
        private string _name;

        public bool Complex { get; set; }
        public bool Invalid { get; private set; }
        internal List<Product> Products { get; private set; }
        internal List<Material> Materials { get; private set; }

        public BlueprintCalculation BlueprintCalculation { get; set; }
        public string Name { get { return _name; } set { _name = value; } }
        
        public long? ProductSellVolume { get; set; }
        public long? ProductBuyVolume { get; set; }
        
        public int ME { get; set; }
        public int TE { get; set; }

        public int ManufacturingTime
        {
            get
            {
                if (Manufacturing != null)
                    return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Manufacturing.time * (1 - (TE * 0.01)))));
                return 0;
            }
        }

        public bool Owned { get; set; }

        public bool HasManufacturing
        {
            get
            {
                return _rawBlueprint?.activities?.manufacturing != null;
            }
        }
        public RawManufacture Manufacturing
        {
            get
            {
                if (HasManufacturing)
                    return _rawBlueprint.activities.manufacturing;
                return null;
            }
        }

        //public Blueprint(int typeId) : this(RawEveData.GetBlueprintById(typeId))
        //{
                       
        //}
        
        //public Blueprint(RawBlueprint blueprint) : base()
        //{
        //    if (blueprint == null)
        //    {
        //        Invalid = true;
        //        return;
        //    }
            
        //    _rawBlueprint = blueprint;
        //    _rawTypeId = RawEveData.GetTypeIdById(_rawBlueprint.blueprintTypeID.Value);
        //    this.Products = new List<Product>();
        //    this.Materials = new List<Material>();

        //    this.BlueprintCalculation = new BlueprintCalculation(this);

        //    if (_rawTypeId != null)
        //    {
        //        TypeId = _rawTypeId.typeID;
        //        Name = _rawTypeId.name["de"];
        //    }
                

        //    this.Region = Regions.Domain;

        //    ProcessProducts();
        //    ProcessMaterials();
        //}

        //internal void BuildBlueprintConnections()
        //{
        //    foreach (var material in this.Materials)
        //    {
        //        material.BuildBlueprintConnection();
        //    }
        //}

        //public void UpdateCalculation()
        //{
        //    BlueprintCalculation.UpdateCalculation();

        //    foreach (var product in Products)
        //    {
        //        this.ProductBuyVolume = product.BuyVolume;
        //        this.ProductSellVolume = product.SellVolume;
        //    }
        //}

        public IEnumerable<Material> GetMaterials()
        {
            return Materials;
        }

        //private void ProcessMaterials()
        //{
        //    this.Materials = new List<Material>();

        //    if (_rawBlueprint?.activities?.manufacturing?.materials != null)
        //    {
        //        foreach (var material in _rawBlueprint.activities.manufacturing.materials)
        //            this.Materials.Add(new Material(material));
        //    }
                
        //}

        //private void ProcessProducts()
        //{
        //    this.Products = new List<Product>();

        //    if (_rawBlueprint?.activities?.manufacturing?.products != null)
        //    {
        //        foreach (var product in _rawBlueprint.activities.manufacturing.products)
        //            this.Products.Add(new Product(product));
        //    }
        //}

        //protected override void OnMarketDataChanged()
        //{
        //    base.OnMarketDataChanged();

        //    UpdateCalculation();
        //}

        //public override async System.Threading.Tasks.Task RequestAsync()
        //{
        //    await base.RequestAsync();

        //    foreach (var product in this.Products)
        //        await product.RequestAsync();

        //    foreach (var material in this.Materials)
        //        await material.RequestAsync();
        //}

        public override string ToString()
        {
            if (_rawTypeId != null)
                return Name;

            return base.ToString();
        }

        //internal void RequestProductsMarketHistory()
        //{
        //    foreach (var product in this.Products)
        //    { 
        //        if (product.RawTypeID != null && product.RawTypeID.published)
        //            product.RequestMarketHistory();
        //    }
        //}

        //internal void ClearProductsMarketHistory()
        //{
        //    foreach (var product in this.Products)
        //    {
        //        product.ClearMarketHistory();
        //    }
        //}
    }
    
}
