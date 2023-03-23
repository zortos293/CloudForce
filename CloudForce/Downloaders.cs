using CloudForce.Properties;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CloudForce.AppsJSON;
using static CloudForce.UtilitiesJSON;
using static CloudForce.LauncherJSON;

namespace CloudForce
{
    internal class Downloaders
    {
        #region Downloader

        private bool DownloadFinished;
        Guna2Button button1;
        public static string mainpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Cloudforce\\";
        public async Task ExtractZipFileAsync(string zipFilePath, string extractPath)
        {
            await Task.Run(() =>
            {
                ZipFile.ExtractToDirectory(zipFilePath, extractPath);
            });
        }
        public async void DownloadApp(int btnid, Guna2Button button)
        {
            button1 = button;
            if (File.Exists(mainpath + APPJson.Apps[btnid].AppExe))
            {
                Process.Start(mainpath + APPJson.Apps[btnid].AppExe);
            }
            else
            {
                File_Downloader(APPJson.Apps[btnid].AppDownloadUrl, mainpath + Uri.UnescapeDataString(APPJson.Apps[btnid].AppDownloadUrl.Split('/').Last()));
                if (Path.GetExtension(APPJson.Apps[btnid].AppDownloadUrl).EndsWith(".zip"))
                {

                    await ExtractZipFileAsync(mainpath + Uri.UnescapeDataString(APPJson.Apps[btnid].AppDownloadUrl.Split('/').Last()), mainpath);
                    Process.Start(mainpath + APPJson.Apps[btnid].AppExe);
                }
                else
                {
                    Process.Start(mainpath + APPJson.Apps[btnid].AppExe);
                }
            }

        }
        public void DownloadUtilities(int btnid, Guna2Button button)
        {
            button1 = button;
            if (File.Exists(mainpath + UtilitiesJson.Apps[btnid].AppExe))
            {
                Process.Start(mainpath + UtilitiesJson.Apps[btnid].AppExe);
            }
            else
            {
                File_Downloader(UtilitiesJson.Apps[btnid].AppDownloadUrl, mainpath + Uri.UnescapeDataString(UtilitiesJson.Apps[btnid].AppDownloadUrl.Split('/').Last()));
                if (Path.GetExtension(UtilitiesJson.Apps[btnid].AppDownloadUrl).EndsWith(".zip"))
                {

                    ExtractZipFileAsync(mainpath + Uri.UnescapeDataString(UtilitiesJson.Apps[btnid].AppDownloadUrl.Split('/').Last()), mainpath);
                    Process.Start(mainpath + UtilitiesJson.Apps[btnid].AppExe);
                }
                else
                {
                    Process.Start(mainpath + UtilitiesJson.Apps[btnid].AppExe);
                }
            }

        }
        public void DownloadLauncher(int btnid, Guna2Button button)
        {
            button1 = button;
            if (File.Exists(mainpath + LauncherJson.Apps[btnid].AppExe))
            {
                Process.Start(mainpath + LauncherJson.Apps[btnid].AppExe);
            }
            else
            {
                File_Downloader(LauncherJson.Apps[btnid].AppDownloadUrl, mainpath + Uri.UnescapeDataString(LauncherJson.Apps[btnid].AppDownloadUrl.Split('/').Last()));
                if (Path.GetExtension(LauncherJson.Apps[btnid].AppDownloadUrl).EndsWith(".zip"))
                {

                    ExtractZipFileAsync(mainpath + Uri.UnescapeDataString(LauncherJson.Apps[btnid].AppDownloadUrl.Split('/').Last()), mainpath);
                    Process.Start(mainpath + LauncherJson.Apps[btnid].AppExe);
                }
                else
                {
                    Process.Start(mainpath + LauncherJson.Apps[btnid].AppExe);
                }
            }

        }
        public void File_Downloader(string URL, string path)
        {
            DownloadFinished = false;
            WebClient client = new WebClient();
            button1.Invoke(new Action(() => button1.Image = null));
            button1.Invoke(new MethodInvoker(delegate { button1.Enabled = false; }));
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDownloadComplete);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadChanged);
            client.DownloadFileAsync(new Uri(URL), path);
            while (DownloadFinished == false)
                Application.DoEvents();
        }

        private async void FileDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                DownloadFinished = true;
                button1.Invoke(new Action(() => button1.Image = Resources.play_48px));
                button1.Invoke(new Action(() => button1.FillColor = Color.FromArgb(31, 138, 112)));
                button1.Invoke(new Action(() => button1.Text = ""));
                button1.Invoke(new Action(() => button1.Enabled = true));
                ((WebClient)sender).Dispose();
            }
            else
            {
               
                MessageBox.Show(e.Error.Message);
                button1.Invoke(new Action(() => button1.Enabled = true));
            }
        }

        private void DownloadChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            button1.Invoke(new Action(() => button1.Text = e.ProgressPercentage + "%"));
        }

        #endregion Downloader
    }
}
