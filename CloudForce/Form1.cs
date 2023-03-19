using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CloudForce
{
    public partial class Form1 : Form
    {
        DynamicLand dynamicLand = new DynamicLand();
        public Form1()
        {
            InitializeComponent();
            dynamicLand.AddApps(this);
        }

        private void Utilities_BTN_Click(object sender, EventArgs e)
        {
            dynamicLand.ClearApps(this);
            dynamicLand.AddApps(this);
        }
    }
}
