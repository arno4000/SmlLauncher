using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;


namespace SmlLauncher
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] vms;
        public string[] vmx;
        
        

        public MainWindow()
        {
            InitializeComponent();
            string vmdir = getSmlLetter() + "1_work";
            vms = Directory.GetDirectories(vmdir);
            string vmName;
            vmx = new string[vms.Length];
            int vmNumber = 0;

            foreach (string vm in vms)
            {
                vmName = new DirectoryInfo(vm).Name;
                if(File.Exists(vm+"\\"+vmName+".vmx"))
                {
                    cmbChooseVm.Items.Add(vmName);
                    vmx[vmNumber] = vm + "\\" + vmName + ".vmx";
                    vmNumber++;
                }
                

            }
           
            
        }


        private void btnStartVm_Click(object sender, RoutedEventArgs e)
        {
            
            string selectedVm = Convert.ToString(cmbChooseVm.SelectedItem);
            string vmToStart = Array.Find(vmx, element => element.Contains(selectedVm));
            
            
            if (string.IsNullOrEmpty(selectedVm))
            {
                MessageBox.Show("Please select a VM to start.", "Select a VM", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                
                startVm(vmToStart);
                
            }
            
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                string selectedVm = Convert.ToString(cmbChooseVm.SelectedItem);
                string vmToStart = Array.Find(vmx, element => element.Contains(selectedVm));
                startVm(vmToStart);
                
            }
        }
    
    public string getSmlLetter() { 
        
            bool foundSml = false;
            string smlLetter = "";

            DriveInfo[] allDrives = DriveInfo.GetDrives();

                foreach (DriveInfo d in allDrives)
                {
                    if(Directory.Exists(d.Name + "1_work"))
                    {
                    foundSml = true;
                    smlLetter = d.Name;
                    }
                }
            if (!foundSml)
            {
                MessageBox.Show("Please try again, no Smartlearn was found", "Error, no Smartlearn found", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
            return smlLetter;
        }
     private void startVm(string vm)
        {
            string vmware = "";
            if(Directory.Exists("C:\\Program Files (x86)\\VMware\\VMware Workstation"))
            {
                vmware = "C:\\Program Files (x86)\\VMware\\VMware Workstation\\vmware.exe";

            }
            else if(Directory.Exists("C:\\Program Files\\VMware\\VMware Workstation Player"))
            {
                vmware = "C:\\Program Files\\VMware\\VMware Workstation Player\\vmware.exe";
            }
            else if (Directory.Exists("C:\\Program Files\\VMware\\VMware Player"))
            {
                vmware = "C:\\Program Files\\VMware\\VMware Player\\vmware.exe";
            }
            else
            {
                MessageBox.Show("Vmware Workstation or Vmware Player was not found on your system.\nPlease install one of them and then try again.", "VmWare not found", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Process vmwareExe = new Process();
            vmwareExe.StartInfo.FileName = vmware;
            if(vmware == "C:\\Program Files (x86)\\VMware\\VMware Workstation\\vmware.exe")
            {
                vmwareExe.StartInfo.Arguments = "-x " + vm;
            }
            vmwareExe.Start();

        }

        public string vmToStart()
        {
            string selectedVm = Convert.ToString(cmbChooseVm.SelectedItem);
            string vmToStart = Array.Find(vmx, element => element.Contains(selectedVm));
            return vmToStart;
        }

        private void btnReplaceVM_Click(object sender, RoutedEventArgs e)
        {
            replace r = new replace();
            r.replaceVM(Convert.ToString(cmbChooseVm.SelectedItem));
        }

        private void btnRepairVM_Click(object sender, RoutedEventArgs e)
        {
            repair r = new repair();
            r.repairVM(Convert.ToString(cmbChooseVm.SelectedItem));

        }
    }

}
