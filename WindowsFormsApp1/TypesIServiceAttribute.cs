using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class TypesIServiceAttribute : Attribute
    {
        public bool typesIServiceAttribute { get; set; }

        public TypesIServiceAttribute(bool types)
        {
            typesIServiceAttribute = types;
        }

     
    }
}