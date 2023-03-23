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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Form1.KeyAuthApp.user_data.username))
            {
                if (guna2TextBox2.Text.StartsWith("http"))
                {
                    Form1.KeyAuthApp.setvar("AppJSON", guna2TextBox2.Text);
                }
                else
                {
                    MessageBox.Show("Invalid Input");
                }
               
            }
        }
    }
}
