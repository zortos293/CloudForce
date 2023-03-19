using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guna.UI2.WinForms;
using static CloudForce.AppsJSON;


namespace CloudForce
{
    internal class DynamicLand
    {
        public void ClearApps(Form1 form)
        {
            form.flowLayoutPanel1.Controls.Clear();
        }
        Downloaders downloaders = new Downloaders();
        public void AddApps(Form1 form)
        {
           
            
            for (int i = 0; i < APPJson.Apps.Count(); i++) // Change to json
            {
                Guna2Panel panel = new Guna2Panel(); // Panel that includes the button and labels
                Guna2Button btn = new Guna2Button();
                Guna2HtmlLabel Appname = new Guna2HtmlLabel();
                Guna2PictureBox AppIMG = new Guna2PictureBox();
                // ----------------------------------------------
                //                    Button
                // ----------------------------------------------
                btn.BorderRadius = 4;
                btn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
                btn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
                btn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
                btn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
                btn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
                btn.Font = new System.Drawing.Font("Segoe UI", 9F);
                btn.ForeColor = System.Drawing.Color.White;
                btn.Image = global::CloudForce.Properties.Resources.download_48px1; 
                btn.ImageSize = new System.Drawing.Size(25, 25);
                btn.Location = new System.Drawing.Point(363, 13);
                btn.Tag = i;
                btn.Name = APPJson.Apps[i].AppName + "BTN"; // Appname + BTN
                btn.Size = new System.Drawing.Size(48, 40);
                btn.TabIndex = 4;
                btn.Click += (s, e) =>
                {
                    
                    //foreach (Guna2Button ctl in form.flowLayoutPanel1.Controls.OfType<Guna2Button>()) // checks If apps exist then change their icons
                    //{
                    //    var tag = ctl.Tag.ToString();
                    //    if (File.Exists(mainpath + APPJson.Apps[int.Parse(tag)].AppExe))
                    //    {
                    //        ctl.ForeColor = System.Drawing.Color.White;
                    //        ctl.CustomImages.Image = null;
                    //    }
                    //    else
                    //    {
                    //        ctl.ForeColor = System.Drawing.Color.Gray;
                    //        ctl.CustomImages.Image = Resources.downloading_updates_96px; ;
                    //    }
                    //}
                    var btnid = int.Parse(btn.Tag.ToString());
                    downloaders.DownloadApp(btnid,btn);



                };
                // ----------------------------------------------
                //                 AppName (Label)
                // ----------------------------------------------
                Appname.BackColor = System.Drawing.Color.Transparent;
                Appname.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Appname.ForeColor = System.Drawing.Color.White;
                Appname.Location = new System.Drawing.Point(95, 11);
                Appname.Name = APPJson.Apps[i].AppName + "LBL"; // Appname + LBL
                Appname.Size = new System.Drawing.Size(52, 22);
                Appname.TabIndex = 2;
                Appname.Text = APPJson.Apps[i].AppName; // Appname
                // ----------------------------------------------
                //                     Image
                // ----------------------------------------------
                AppIMG.FillColor = System.Drawing.Color.Transparent;
                AppIMG.Load(APPJson.Apps[i].AppImage); // APPIMG
                AppIMG.ImageRotate = 0F;
                AppIMG.Location = new System.Drawing.Point(14, 10);
                AppIMG.Name = APPJson.Apps[i].AppName + "IMG"; // Appname + IMG
                AppIMG.Size = new System.Drawing.Size(53, 46);
                AppIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                AppIMG.TabIndex = 1;
                AppIMG.TabStop = false;
                // ----------------------------------------------
                //                    Panel
                // ----------------------------------------------
                panel.BackColor = System.Drawing.Color.Transparent;
                panel.BorderRadius = 6;
                panel.Controls.Add(btn);
                panel.Controls.Add(Appname);
                panel.Controls.Add(AppIMG);
                panel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
                panel.Location = new System.Drawing.Point(3, 3);
                panel.Name = APPJson.Apps[i].AppName + "Panel"; // Appname + Panel
                panel.Size = new System.Drawing.Size(424, 66);
                panel.TabIndex = 0;
                form.flowLayoutPanel1.Controls.Add(panel);
            }
        }
    }
}
