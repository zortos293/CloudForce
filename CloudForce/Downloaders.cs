using CloudForce.Properties;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CloudForce.AppsJSON;

namespace CloudForce
{
    internal class Downloaders
    {
        public static string mainpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Cloudforce\\";
        #region Downloader

        private bool DownloadFinished;
        Guna2Button button1;
        public void DownloadApp(int btnid, Guna2Button button)
        {
            button1 = button;
            
            File_Downloader(APPJson.Apps[btnid].AppDownloadUrl, mainpath + Uri.UnescapeDataString(APPJson.Apps[btnid].AppDownloadUrl.Split('/').Last()));
            if (Path.GetExtension(APPJson.Apps[btnid].AppDownloadUrl).EndsWith(".zip"))
            {

            }
            else
            {
                Process.Start(mainpath + APPJson.Apps[btnid].AppExe);
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
