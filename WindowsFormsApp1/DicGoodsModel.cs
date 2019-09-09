using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DicGoodsModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool IsService { get; set; }
        public double Price { get; set; }

        public bool IsSale { get; set; }

        public static List<DicGoodsModel> _dicGoodsModel;
        static DicGoodsModel()
        {
            _dicGoodsModel = new List<DicGoodsModel>();
        }

        public DicGoodsModel(string id, string name, bool isService, bool isSale, double price = 0.00)
        {
            this.ID = id;
            this.Name = name;
            this.IsService = isService;
            this.Price = price;
            this.IsSale = isSale;
        }

        public static DicGoodsModel[] GetDicGoodsModel
        {
            get
            {
                return DicGoodsModel._dicGoodsModel.ToArray();
            }
        }

        public static void AddDicGoodsModel(DicGoodsModel _dicGoodsModel)
        {
            DicGoodsModel._dicGoodsModel.Add(_dicGoodsModel);
        }

        public static void ClearDicGoodsModel()
        {
            DicGoodsModel._dicGoodsModel.Clear();
        }

    }
}
