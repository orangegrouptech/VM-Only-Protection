using Microsoft.Win32;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace VMOnlyProtection
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //VM-only protection
            using (var searcher = new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
            {
                using (var items = searcher.Get())
                {
                    foreach (var item in items)
                    {
                        string manufacturer = item["Manufacturer"].ToString().ToLower();
                        if ((manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
                            || manufacturer.ToLower().Contains("vmware")
                            || item["Model"].ToString().ToLower() == "virtualbox")
                        {
                            //Your main code here
                        }
                        else
                        {
                            MessageBox.Show("You are not using a Virtual Machine, so you may not proceed. If you have been infected on your host, contact orangemanagementcorpn@gmail.com to remove the malware.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                }
            }
        }
    }
}
