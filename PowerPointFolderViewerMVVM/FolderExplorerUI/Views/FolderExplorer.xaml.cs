using System;
using System.Collections.Generic;
using System.IO;
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
using PowerPointFolderViewerMVVM.FolderExplorerUI.Helpers;
using PowerPointFolderViewerMVVM.FolderExplorerUI.Models;
using PowerPointFolderViewerMVVM.FolderExplorerUI.ViewModels;

namespace PowerPointFolderViewerMVVM.FolderExplorerUI.Views
{
    /// <summary>
    /// Interaction logic for FolderExplorer.xaml
    /// </summary>
    public partial class FolderExplorer : Window
    {
        public FolderExplorer()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item == null || !(item.DataContext is FolderItem folder)) return;

            try
            {
                if (folder.SubFolders.Count == 1 && folder.SubFolders[0] == null)
                {
                    folder.SubFolders.Clear();

                    var subDirs = Directory.GetDirectories(folder.FullPath);
                    foreach (var dir in subDirs)
                    {
                        var subFolder = new FolderItem(dir);
                        subFolder.SubFolders.Add(null); // Placeholder for lazy loading
                        folder.SubFolders.Add(subFolder);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to expand folder '{folder.FullPath}': {ex.Message}");
            }
        }

    }
}
