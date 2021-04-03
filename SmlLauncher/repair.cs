using System;
using System.IO;
using System.Windows;

namespace SmlLauncher
{
    class repair
    {
        public void repairVM(string vm)
        {
            replace r = new replace();
            string smlLetter = r.getSmlLetter();
            string vmDir = smlLetter + "1_work\\" + vm;

            MessageBoxResult m = MessageBox.Show("Are you shure that you want to repair the selected VM?", "Repair VM", MessageBoxButton.YesNoCancel);
            if (m == MessageBoxResult.Yes)
            {
                string[] lockItems = Directory.GetDirectories(vmDir, "*.lck");
                foreach (var lck in lockItems)
                {
                    Directory.Delete(lck);
                }
               
            }
        }
    }
}