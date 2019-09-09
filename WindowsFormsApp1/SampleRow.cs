using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class SampleRow
    {
        [DisplayName("Код")]
        [ColumnWeight(10)] //todo
        [AutoSizeMode(DataGridViewAutoSizeColumnMode.AllCells)]
        [TypesIService(false)]
        public string code { get; set; }

        [DisplayName("Наименование")]
        [ColumnWeight(10)] //todo
        [AutoSizeMode(DataGridViewAutoSizeColumnMode.AllCells)]
        [TypesIService(false)]
        public string name { get; set; } //обязательно нужно использовать get конструкцию

        [DisplayName("Единица измерения")]
        [ColumnWeight(10)] //todo
        [AutoSizeMode(DataGridViewAutoSizeColumnMode.AllCells)]
        [TypesIService(false)]
        public string edIzm { get; set; }

        [DisplayName("Услуга")]
        [ColumnWeight(10)] //todo
        [AutoSizeMode(DataGridViewAutoSizeColumnMode.AllCells)]
        [TypesIService(true)] 
        public bool isService { get; set; }

        [DisplayName("Цена")]
        [ColumnWeight(10)] //todo
        [AutoSizeMode(DataGridViewAutoSizeColumnMode.AllCells)]
        [TypesIService(false)]
        public string price { get; set; }

        [DisplayName("Код")]
        [ColumnWeight(10)] //todo
        [AutoSizeMode(DataGridViewAutoSizeColumnMode.AllCells)]
        [TypesIService(false)]
        public int count { get; set; }

        public string Hidden = ""; //Данное свойство не будет отображаться как колонка

        public SampleRow(string code, string name, string price, bool _isService, int count)
        {
            this.code = code;
            this.name = name;
            this.price = price;
            this.isService = _isService;
            this.count = count;
        }
    }
}
