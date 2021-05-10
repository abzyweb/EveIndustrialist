using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EoiData.EoiDataClasses;
using EoiData.StaticDataClasses.StaticDataReader;

namespace EoiData.StaticDataClasses
{
    public class StaticDataBlueprint
    {
        private RawBlueprint _blueprint;
        private StaticDataType _type;
        private List<StaticDataType> _products;
        private List<StaticDataType> _materials;
        private List<StaticDataType> _inventionProducts;
        private List<StaticDataType> _inventionMaterials;

        public int Id { get; internal set; }
        public bool Invalid { get; internal set; }

        public bool Published { get; internal set; }
        public string Name
        {
            get
            {
                if (_type == null)
                    return string.Empty;

                return _type.Name;
            }
        }

        public int ManufacturingTime { get; internal set; }

        public StaticDataBlueprint(KeyValuePair<int, RawBlueprint> data)
        {
            if (data.Value == null)
            {
                Invalid = true;
                return;
            }

            _blueprint = data.Value;

            if (_blueprint.blueprintTypeID.HasValue)
                this.Id = _blueprint.blueprintTypeID.Value;
            else
            {
                this.Invalid = true;
                return;
            }


            var type = StaticDataInterface.GetTypeById(this.Id);
            if (type == null)
            {
                this.Invalid = true;
                return;
            }

            _type = type;
            Published = type.Published;

            if (_blueprint?.activities?.manufacturing == null || _blueprint.activities.manufacturing.time == null)
            {
                this.Invalid = true;
                return;
            }
            this.ManufacturingTime = _blueprint.activities.manufacturing.time.Value;

            _products = new List<StaticDataType>();
            if (_blueprint?.activities?.manufacturing?.products == null)
            {
                this.Invalid = true;
                return;
            }
            foreach (var product in _blueprint.activities.manufacturing.products)
            {
                if (product.typeID == null)
                {
                    this.Invalid = true;
                    return;
                }
                type = StaticDataInterface.GetTypeById(product.typeID.Value);
                if (type == null)
                {
                    this.Invalid = true;
                    return;
                }
                _products.Add(type);
            }

            _materials = new List<StaticDataType>();
            if (_blueprint?.activities?.manufacturing?.materials == null)
            {
                this.Invalid = true;
                return;
            }
            foreach (var material in _blueprint.activities.manufacturing.materials)
            {
                if (material.typeID == null)
                {
                    this.Invalid = true;
                    return;
                }
                type = StaticDataInterface.GetTypeById(material.typeID.Value);
                if (type == null)
                {
                    this.Invalid = true;
                    return;
                }
                _materials.Add(type);
            }

            _inventionProducts = new List<StaticDataType>();
            if (_blueprint?.activities?.invention?.products != null)
            {
                foreach (var invention in _blueprint?.activities?.invention?.products)
                {
                    if (invention.typeID == null)
                        return;

                    type = StaticDataInterface.GetTypeById(invention.typeID.Value);
                    if (type == null)
                        return;

                    _inventionProducts.Add(type);
                }
            }

            _inventionMaterials = new List<StaticDataType>();
            if (_blueprint?.activities?.invention?.materials != null)
            {
                foreach (var invention in _blueprint?.activities?.invention?.materials)
                {
                    if (invention.typeID == null)
                        return;

                    type = StaticDataInterface.GetTypeById(invention.typeID.Value);
                    if (type == null)
                        return;

                    _inventionMaterials.Add(type);
                }
            }
        }

        internal List<StaticDataType> GetProducts()
        {
            return _products;
        }

        internal List<StaticDataType> GetMaterials()
        {
            return _materials;
        }

        internal List<StaticDataType> GetInventionProducts()
        {
            return _inventionProducts;
        }

        internal List<StaticDataType> GetInventionMaterials()
        {
            return _inventionMaterials;
        }

        internal int GetInventionMaterialQuantity(StaticDataType type)
        {
            if (_blueprint?.activities?.invention?.materials == null)
                return 0;

            var material = _blueprint?.activities?.invention?.materials.FirstOrDefault(x => x.typeID == type.Id);
            if (material?.quantity != null)
                return material.quantity.Value;

            return 0;
        }

        internal int GetIneventionProductQuantity(StaticDataType type)
        {
            if (_blueprint?.activities?.invention?.products == null)
                return 0;

            var product = _blueprint?.activities?.invention?.products.FirstOrDefault(x => x.typeID == type.Id);
            if (product?.quantity != null)
                return product.quantity.Value;

            return 0;
        }

        internal int GetMaterialQuantity(StaticDataType type)
        {
            if (_blueprint?.activities?.manufacturing?.materials == null)
                return 0;

            var material = _blueprint?.activities?.manufacturing?.materials.FirstOrDefault(x => x.typeID == type.Id);
            if (material?.quantity != null)
                return material.quantity.Value;

            return 0;
        }

        internal int GetProductQuantity(StaticDataType type)
        {
            if (_blueprint?.activities?.manufacturing?.products == null)
                return 0;

            var product = _blueprint?.activities?.manufacturing?.products.FirstOrDefault(x => x.typeID == type.Id);
            if (product?.quantity != null)
                return product.quantity.Value;

            return 0;
        }
    }
}
