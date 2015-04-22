using ManagementFileConnections.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagementFileConnections
{
    public static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConvertFiles();
        }

        public static void ConvertFiles()
        {
            var resourceManager = new ResourceManager(typeof(Resource1));
            var rootDirPath = "";
            var dialog = new FolderBrowserDialog();
            dialog.Description = resourceManager.GetString("dialogDescrition");
            if (dialog.ShowDialog() == DialogResult.OK)  
            {
                rootDirPath = dialog.SelectedPath;
                var converter = new Converter();
                foreach (var fileName in 
                            Directory.GetFiles(rootDirPath)
                                    .Where(x => !x.ToLower().EndsWith(".tsv")))
                {
                    converter.BinToTSV(fileName);
                }

                MessageBox.Show("Average latency: " + converter.AverageLatency + " and total bandwidth: " + converter.TotalBandwidth);
            }

        }
    }
}
