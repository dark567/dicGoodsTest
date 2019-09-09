﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class AutoSizeMode : Attribute
    {
        public DataGridViewAutoSizeColumnMode SizeMode { get; set; }

        public AutoSizeMode(DataGridViewAutoSizeColumnMode sizeMode)
        {
            SizeMode = sizeMode;
        }
    }
}
