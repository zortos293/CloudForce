using KeyAuth;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form1.KeyAuthApp.login(guna2TextBox1.Text, guna2TextBox2.Text);
            if (!Form1.KeyAuthApp.response.success)
            {
                MessageBox.Show(Form1.KeyAuthApp.response.message);

            }
            else
            {
                this.Hide();
            }
        }
    }
}
