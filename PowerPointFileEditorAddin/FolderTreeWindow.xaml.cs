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
using System.Configuration;
using PowerPointFileEditorAddin.Helpers;

namespace PowerPointFileEditorAddin
{
    /// <summary>
    /// Interaction logic for FolderTreeWindow.xaml
    /// </summary>
    public partial class FolderTreeWindow : Window
    {
        private string basePath;

        public FolderTreeWindow()
        {
            try
            {
                InitializeComponent();
                basePath = ConfigurationSettings.AppSettings["BaseFolderPath"] ?? ToString();
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                LoadFolderTree();
            }
            catch (Exception ex)
            {

                Logger.Log($"Error: {ex.Message}");
            }
        }

        private void LoadFolderTree()
        {
            FolderTree.Items.Clear();
            var rootItem = CreateDirectoryNode(new DirectoryInfo(basePath));
            FolderTree.Items.Add(rootItem);
            FolderTree.ContextMenu = new ContextMenu();

            MenuItem addSubfolder = new MenuItem { Header = "Add Subfolder" };
            addSubfolder.Click += AddSubfolder_Click; ;
            FolderTree.ContextMenu.Items.Add(addSubfolder);
        }

        private void AddSubfolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string newFolderName = "Subfolder";
                if (string.IsNullOrWhiteSpace(newFolderName)) return;

                if (FolderTree.SelectedItem is TreeViewItem selectedItem)
                {
                    string selectedPath = selectedItem.Tag.ToString();
                    string newFolderPath = System.IO.Path.Combine(selectedPath, newFolderName);

                    if (!Directory.Exists(newFolderPath))
                    {
                        Directory.CreateDirectory(newFolderPath);
                        selectedItem.Items.Add(CreateDirectoryNode(new DirectoryInfo(newFolderPath)));
                        selectedItem.IsExpanded = true;
                    }
                    else
                    {
                        MessageBox.Show("subfolder already exist.");
                        Logger.Log($"subfolder already exist.");
                    }

                }
                else
                {
                    MessageBox.Show("Please select a folder in the tree to add a subfolder.");
                    Logger.Log("Please select a folder in the tree to add a subfolder.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error: {ex.Message}");
            }
        }

        private TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var item = new TreeViewItem { Header = directoryInfo.Name, Tag = directoryInfo.FullName };
            foreach (var dir in directoryInfo.GetDirectories())
            {
                item.Items.Add(CreateDirectoryNode(dir));
            }
            return item;
        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            string newFolderName = FolderNameBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(newFolderName)) return;

            if (FolderTree.SelectedItem is TreeViewItem selectedItem)
            {
                string selectedPath = selectedItem.Tag.ToString();
                string newFolderPath = System.IO.Path.Combine(selectedPath, newFolderName);

                if (!Directory.Exists(newFolderPath))
                {
                    Directory.CreateDirectory(newFolderPath);
                    selectedItem.Items.Add(CreateDirectoryNode(new DirectoryInfo(newFolderPath)));
                    selectedItem.IsExpanded = true;
                }
            }
            else
            {
                MessageBox.Show("Please select a folder in the tree to add a subfolder.");
            }
        }
    }
}
