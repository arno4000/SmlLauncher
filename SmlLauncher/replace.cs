using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace SmlLauncher
{
    class replace
    {
        public void replaceVM(string vm)
        {
            string smlLetter = getSmlLetter();
            string sZipExe = smlLetter + "7_smlLauncher\\7za.exe";
            string vmDir = smlLetter + "1_work\\" + vm;
            string vmBackupFile = smlLetter + "3_archive\\" + vm + ".7z.001";
            string workDir = smlLetter + "1_work";

            if (string.IsNullOrEmpty(vm))
            {
                MessageBox.Show("Please select a VM, that you want to replace", "Select a VM", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }


            if (!File.Exists(sZipExe))
            {
                MessageBox.Show("7Zip could not be found on your Smartlearn\nPlease make sure that the executeable 7za.exe can be found under the following path: "+sZipExe, "7Zip not found", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            

            MessageBoxResult m = MessageBox.Show("Do you want to replace the VM?\nWarning, the VM will be reseted!", "Reset VM", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (m == MessageBoxResult.Yes)
            {
                if (!File.Exists(vmBackupFile))
                {
                    MessageBox.Show("The backup file " + vmBackupFile + " does not exist! Please make sure that it exist and then try it again.\nThe file must have the same name as the VM", "File not found", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Directory.Exists(vmDir))
                {
                    try
                    {
                        Directory.Move(vmDir, vmDir + "_bak");
                    }
                    catch
                    {
                        MessageBox.Show("The Directory " + vmDir + "_bak does already exist. Please delete or rename it to proceed.", "Folder already exists", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Error, could not find virtual machine directory", "Directory not found", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;

                }

                
                Process sevenZip = new Process();
                sevenZip.StartInfo.FileName = sZipExe;
                sevenZip.StartInfo.Arguments = "x " + vmBackupFile + " -o" + workDir;
                sevenZip.Start();

            }
            else if(m == MessageBoxResult.No)
            {
                return;
            }




        }



       public string getSmlLetter()
        {

            bool foundSml = false;
            string smlLetter = "";

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (Directory.Exists(d.Name + "1_work"))
                {
                    foundSml = true;
                    smlLetter = d.Name;
                }
            }
            if (!foundSml)
            {
                MessageBox.Show("Please try again, no Sml was found");
                Environment.Exit(1);
            }
            return smlLetter;
        }
    }
}
