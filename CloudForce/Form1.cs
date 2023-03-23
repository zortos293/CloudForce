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
    public partial class Form1 : Form
    {
        DynamicLand dynamicLand = new DynamicLand();
        public Form1()
        {
            InitializeComponent();
            KeyAuthApp.init();
        }

        public static api KeyAuthApp = new api(
            name: "CF Early",
            ownerid: "0t0Sr0pLaB",
            secret: "c52ed8ebcefc829ffed9a73e9c85b73fd5a8e244abec5531ef1cf87628d181e0",
            version: "1.0"
        );

        private async void guna2Button4_Click(object sender, EventArgs e) //Utilities
        {
            flowLayoutPanel1.Visible = true;
            await dynamicLand.ClearFlowLayout(this);
            await dynamicLand.AddUtilitiesAsync(this);
            await dynamicLand.CheckUtilities(this);
        }

        private async void Apps_BTN_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            await dynamicLand.ClearFlowLayout(this);
            await dynamicLand.AddAppsAsync(this);
            //if (!string.IsNullOrEmpty(KeyAuthApp.getvar("AppJson")))
            //{
            //    await dynamicLand.AddUserApps(this);
            //}
            await dynamicLand.CheckApps(this);
        }

        private async void LaunchersBTN_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            await dynamicLand.ClearFlowLayout(this);
            await dynamicLand.AddLaunchersAsync(this);
            await dynamicLand.CheckLaunchers(this);
        }

        private async void Home_BTN_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false;
            await dynamicLand.ClearFlowLayout(this);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog(this);
        }

        private void LoginBTN_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog(this);
        }
    }
}
