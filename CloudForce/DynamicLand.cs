﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudForce.Properties;
using Guna.UI2.WinForms;
using Newtonsoft.Json;
using static CloudForce.AppsJSON;
using static CloudForce.UtilitiesJSON;
using static CloudForce.LauncherJSON;


namespace CloudForce
{
    internal class DynamicLand
    {
        Downloaders downloaders = new Downloaders();
        public static string mainpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Cloudforce\\"; // %appdata%\cloudforce\
        AppsJSON userjson;
        public async Task ClearFlowLayout(Form1 form)
        {
            await Task.Run(() =>
            {
                if (form.flowLayoutPanel1.InvokeRequired)
                {
                    form.flowLayoutPanel1.Invoke((MethodInvoker)delegate
                    {
                        form.flowLayoutPanel1.Controls.Clear();
                    });
                }
                else
                {
                    form.flowLayoutPanel1.Controls.Clear();
                }
            });
        }
        #region Apps


       
        public async Task AddAppsAsync(Form1 form)
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
                btn.Click += async (s, e) =>
                {
                    await Task.Run(() =>
                    {
                        var btnid = int.Parse(btn.Tag.ToString());
                        downloaders.DownloadApp(btnid, btn);

                        // update UI on UI thread
                        form.Invoke(new Action(() =>
                        {
                            foreach (Guna2Panel ctl in form.flowLayoutPanel1.Controls.OfType<Guna2Panel>()) // checks If apps exist then change their icons
                            {
                                foreach (Guna2Button ctla in ctl.Controls.OfType<Guna2Button>()) // checks If apps exist then change their icons
                                {
                                    var tag = ctla.Tag.ToString();
                                    if (File.Exists(mainpath + APPJson.Apps[int.Parse(tag)].AppExe))
                                    {
                                        ctla.Invoke(new Action(() => ctla.Image = Resources.play_48px));
                                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                                    }
                                    else
                                    {
                                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(57, 57, 57)));
                                        ctla.Invoke(new Action(() => ctla.Image = global::CloudForce.Properties.Resources.download_48px1));

                                    }
                                }
                            }
                        }));
                    });






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
                if (!form.IsDisposed && !form.Disposing && form.flowLayoutPanel1 != null)
                {
                    if (form.flowLayoutPanel1.InvokeRequired)
                    {
                        form.flowLayoutPanel1.Invoke((MethodInvoker)delegate
                        {
                            form.flowLayoutPanel1.Controls.Add(panel);
                        });
                    }
                    else
                    {
                        form.flowLayoutPanel1.Controls.Add(panel);
                    }
                }
            }


        }

        public async Task CheckApps(Form1 form)
        {
            foreach (Guna2Panel ctl in form.flowLayoutPanel1.Controls.OfType<Guna2Panel>())
            {
                foreach (Guna2Button ctla in ctl.Controls.OfType<Guna2Button>())
                {
                    var tag = ctla.Tag.ToString();
                    if (File.Exists(mainpath + APPJson.Apps[int.Parse(tag)].AppExe))
                    {
                        ctla.Invoke(new Action(() => ctla.Image = Resources.play_48px));
                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                    }
                    else
                    {
                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                        ctla.Invoke(new Action(() => ctla.Image = global::CloudForce.Properties.Resources.download_48px1));
                    }

                }
            }
        }


        #endregion
        #region User Apps
        public async Task AddUserApps(Form1 form)
        {
            AppsJSON.Root UserJSON = JsonConvert.DeserializeObject<AppsJSON.Root>(getAPPjson());
            for (int i = 0; i < APPJson.Apps.Count(); i++) 
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
                btn.FillColor = System.Drawing.Color.FromArgb(53, 53, 53);
                btn.Font = new System.Drawing.Font("Segoe UI", 9F);
                btn.ForeColor = System.Drawing.Color.White;
                btn.Image = global::CloudForce.Properties.Resources.download_48px1;
                btn.ImageSize = new System.Drawing.Size(25, 25);
                btn.Location = new System.Drawing.Point(363, 13);
                btn.Tag = i;
                btn.Name = UserJSON.Apps[i].AppName + "BTN"; // Appname + BTN
                btn.Size = new System.Drawing.Size(48, 40);
                btn.TabIndex = 4;
                btn.Click += async (s, e) =>
                {
                    await Task.Run(() =>
                    {
                        var btnid = int.Parse(btn.Tag.ToString());
                        downloaders.DownloadApp(btnid, btn);

                        // update UI on UI thread
                        form.Invoke(new Action(() =>
                        {
                            foreach (Guna2Panel ctl in form.flowLayoutPanel1.Controls.OfType<Guna2Panel>()) // checks If apps exist then change their icons
                            {
                                foreach (Guna2Button ctla in ctl.Controls.OfType<Guna2Button>()) // checks If apps exist then change their icons
                                {
                                    var tag = ctla.Tag.ToString();
                                    if (File.Exists(mainpath + UserJSON.Apps[int.Parse(tag)].AppExe))
                                    {
                                        ctla.Invoke(new Action(() => ctla.Image = Resources.play_48px));
                                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                                    }
                                    else
                                    {
                                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                                        ctla.Invoke(new Action(() => ctla.Image = global::CloudForce.Properties.Resources.download_48px1));

                                    }
                                }
                            }
                        }));
                    });






                };
                // ----------------------------------------------
                //                 AppName (Label)
                // ----------------------------------------------
                Appname.BackColor = System.Drawing.Color.Transparent;
                Appname.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Appname.ForeColor = System.Drawing.Color.White;
                Appname.Location = new System.Drawing.Point(95, 11);
                Appname.Name = UserJSON.Apps[i].AppName + "LBL"; // Appname + LBL
                Appname.Size = new System.Drawing.Size(52, 22);
                Appname.TabIndex = 2;
                Appname.Text = UserJSON.Apps[i].AppName; // Appname
                // ----------------------------------------------
                //                     Image
                // ----------------------------------------------
                AppIMG.FillColor = System.Drawing.Color.Transparent;
                AppIMG.Load(APPJson.Apps[i].AppImage); // APPIMG
                AppIMG.ImageRotate = 0F;
                AppIMG.Location = new System.Drawing.Point(14, 10);
                AppIMG.Name = UserJSON.Apps[i].AppName + "IMG"; // Appname + IMG
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
                panel.Name = UserJSON.Apps[i].AppName + "Panel"; // Appname + Panel
                panel.Size = new System.Drawing.Size(424, 66);
                panel.TabIndex = 0;
                if (!form.IsDisposed && !form.Disposing && form.flowLayoutPanel1 != null)
                {
                    if (form.flowLayoutPanel1.InvokeRequired)
                    {
                        form.flowLayoutPanel1.Invoke((MethodInvoker)delegate
                        {
                            form.flowLayoutPanel1.Controls.Add(panel);
                        });
                    }
                    else
                    {
                        form.flowLayoutPanel1.Controls.Add(panel);
                    }
                }
            }
        }
        #endregion
        #region Utilities
        public async Task CheckUtilities(Form1 form)
        {
            form.Invoke(new Action(() =>
            {
                foreach (Guna2Panel ctl in form.flowLayoutPanel1.Controls.OfType<Guna2Panel>()) // checks If apps exist then change their icons
                {
                    foreach (Guna2Button ctla in ctl.Controls.OfType<Guna2Button>()) // checks If apps exist then change their icons
                    {
                        var tag = ctla.Tag.ToString();
                        if (File.Exists(mainpath + UtilitiesJson.Apps[int.Parse(tag)].AppExe))
                        {
                            ctla.Invoke(new Action(() => ctla.Image = Resources.play_48px));
                            ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                        }
                        else
                        {
                            ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                            ctla.Invoke(new Action(() => ctla.Image = global::CloudForce.Properties.Resources.download_48px1));

                        }
                    }
                }
            }));
        }
        public async Task AddUtilitiesAsync(Form1 form)
        {
            for (int i = 0; i < UtilitiesJson.Apps.Count(); i++)
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
                btn.DisabledState.FillColor = System.Drawing.Color.FromArgb(53, 53, 53);
                btn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
                btn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
                btn.Font = new System.Drawing.Font("Segoe UI", 9F);
                btn.ForeColor = System.Drawing.Color.White;
                btn.Image = global::CloudForce.Properties.Resources.download_48px1;
                btn.ImageSize = new System.Drawing.Size(25, 25);
                btn.Location = new System.Drawing.Point(363, 13);
                btn.Tag = i;
                btn.Name = UtilitiesJson.Apps[i].AppName + "BTN"; // Appname + BTN
                btn.Size = new System.Drawing.Size(48, 40);
                btn.TabIndex = 4;
                btn.Click += async (s, e) =>
                {
                    await Task.Run(() =>
                    {
                        var btnid = int.Parse(btn.Tag.ToString());
                        downloaders.DownloadUtilities(btnid, btn);

                        // update UI on UI thread
                        form.Invoke(new Action(() =>
                        {
                            foreach (Guna2Panel ctl in form.flowLayoutPanel1.Controls.OfType<Guna2Panel>()) // checks If apps exist then change their icons
                            {
                                foreach (Guna2Button ctla in ctl.Controls.OfType<Guna2Button>()) // checks If apps exist then change their icons
                                {
                                    var tag = ctla.Tag.ToString();
                                    if (File.Exists(mainpath + UtilitiesJson.Apps[int.Parse(tag)].AppExe))
                                    {
                                        ctla.Invoke(new Action(() => ctla.Image = Resources.play_48px));
                                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                                    }
                                    else
                                    {
                                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                                        ctla.Invoke(new Action(() => ctla.Image = global::CloudForce.Properties.Resources.download_48px1));

                                    }
                                }
                            }
                        }));
                    });






                };
                // ----------------------------------------------
                //                 AppName (Label)
                // ----------------------------------------------
                Appname.BackColor = System.Drawing.Color.Transparent;
                Appname.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Appname.ForeColor = System.Drawing.Color.White;
                Appname.Location = new System.Drawing.Point(95, 11);
                Appname.Name = UtilitiesJson.Apps[i].AppName + "LBL"; // Appname + LBL
                Appname.Size = new System.Drawing.Size(52, 22);
                Appname.TabIndex = 2;
                Appname.Text = UtilitiesJson.Apps[i].AppName; // Appname
                // ----------------------------------------------
                //                     Image
                // ----------------------------------------------
                AppIMG.FillColor = System.Drawing.Color.Transparent;
                AppIMG.Load(UtilitiesJson.Apps[i].AppImage); // APPIMG
                AppIMG.ImageRotate = 0F;
                AppIMG.Location = new System.Drawing.Point(14, 10);
                AppIMG.Name = UtilitiesJson.Apps[i].AppName + "IMG"; // Appname + IMG
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
                panel.Name = UtilitiesJson.Apps[i].AppName + "Panel"; // Appname + Panel
                panel.Size = new System.Drawing.Size(424, 66);
                panel.TabIndex = 0;
                if (!form.IsDisposed && !form.Disposing && form.flowLayoutPanel1 != null)
                {
                    if (form.flowLayoutPanel1.InvokeRequired)
                    {
                        form.flowLayoutPanel1.Invoke((MethodInvoker)delegate
                        {
                            form.flowLayoutPanel1.Controls.Add(panel);
                        });
                    }
                    else
                    {
                        form.flowLayoutPanel1.Controls.Add(panel);
                    }
                }
            }
        }
        #endregion
        #region Launchers
        public async Task CheckLaunchers(Form1 form)
        {
            form.Invoke(new Action(() =>
            {
                foreach (Guna2Panel ctl in form.flowLayoutPanel1.Controls.OfType<Guna2Panel>()) // checks If apps exist then change their icons
                {
                    foreach (Guna2Button ctla in ctl.Controls.OfType<Guna2Button>()) // checks If apps exist then change their icons
                    {
                        var tag = ctla.Tag.ToString();
                        if (File.Exists(mainpath + LauncherJson.Apps[int.Parse(tag)].AppExe))
                        {
                            ctla.Invoke(new Action(() => ctla.Image = Resources.play_48px));
                            ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                        }
                        else
                        {
                            ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                            ctla.Invoke(new Action(() => ctla.Image = global::CloudForce.Properties.Resources.download_48px1));

                        }
                    }
                }
            }));
        }
        public async Task AddLaunchersAsync(Form1 form)
        {
            for (int i = 0; i < LauncherJson.Apps.Count(); i++)
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
                btn.DisabledState.FillColor = System.Drawing.Color.FromArgb(53, 53, 53);
                btn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
                btn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
                btn.Font = new System.Drawing.Font("Segoe UI", 9F);
                btn.ForeColor = System.Drawing.Color.White;
                btn.Image = global::CloudForce.Properties.Resources.download_48px1;
                btn.ImageSize = new System.Drawing.Size(25, 25);
                btn.Location = new System.Drawing.Point(363, 13);
                btn.Tag = i;
                btn.Name = LauncherJson.Apps[i].AppName + "BTN"; // Appname + BTN
                btn.Size = new System.Drawing.Size(48, 40);
                btn.TabIndex = 4;
                btn.Click += async (s, e) =>
                {
                    await Task.Run(() =>
                    {
                        var btnid = int.Parse(btn.Tag.ToString());
                        downloaders.DownloadLauncher(btnid, btn);

                        // update UI on UI thread
                        form.Invoke(new Action(() =>
                        {
                            foreach (Guna2Panel ctl in form.flowLayoutPanel1.Controls.OfType<Guna2Panel>()) // checks If apps exist then change their icons
                            {
                                foreach (Guna2Button ctla in ctl.Controls.OfType<Guna2Button>()) // checks If apps exist then change their icons
                                {
                                    var tag = ctla.Tag.ToString();
                                    if (File.Exists(mainpath + LauncherJson.Apps[int.Parse(tag)].AppExe))
                                    {
                                        ctla.Invoke(new Action(() => ctla.Image = Resources.play_48px));
                                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                                    }
                                    else
                                    {
                                        ctla.Invoke(new Action(() => ctla.FillColor = Color.FromArgb(53, 53, 53)));
                                        ctla.Invoke(new Action(() => ctla.Image = global::CloudForce.Properties.Resources.download_48px1));

                                    }
                                }
                            }
                        }));
                    });






                };
                // ----------------------------------------------
                //                 AppName (Label)
                // ----------------------------------------------
                Appname.BackColor = System.Drawing.Color.Transparent;
                Appname.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Appname.ForeColor = System.Drawing.Color.White;
                Appname.Location = new System.Drawing.Point(95, 11);
                Appname.Name = LauncherJson.Apps[i].AppName + "LBL"; // Appname + LBL
                Appname.Size = new System.Drawing.Size(52, 22);
                Appname.TabIndex = 2;
                Appname.Text = LauncherJson.Apps[i].AppName; // Appname
                // ----------------------------------------------
                //                     Image
                // ----------------------------------------------
                AppIMG.FillColor = System.Drawing.Color.Transparent;
                AppIMG.Load(LauncherJson.Apps[i].AppImage); // APPIMG
                AppIMG.ImageRotate = 0F;
                AppIMG.Location = new System.Drawing.Point(14, 10);
                AppIMG.Name = LauncherJson.Apps[i].AppName + "IMG"; // Appname + IMG
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
                panel.Name = LauncherJson.Apps[i].AppName + "Panel"; // Appname + Panel
                panel.Size = new System.Drawing.Size(424, 66);
                panel.TabIndex = 0;
                if (!form.IsDisposed && !form.Disposing && form.flowLayoutPanel1 != null)
                {
                    if (form.flowLayoutPanel1.InvokeRequired)
                    {
                        form.flowLayoutPanel1.Invoke((MethodInvoker)delegate
                        {
                            form.flowLayoutPanel1.Controls.Add(panel);
                        });
                    }
                    else
                    {
                        form.flowLayoutPanel1.Controls.Add(panel);
                    }
                }
            }
        }
        #endregion
    }
}
