using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PowerPointFolderViewerMVVM.FolderExplorerUI.Helpers;
using PowerPointFolderViewerMVVM.FolderExplorerUI.Services;
using PowerPointFolderViewerMVVM.FolderExplorerUI.Views;

namespace PowerPointFolderViewerMVVM.FolderExplorerUI.Models
{
    /// <summary>
    /// FolderItem show in tree node
    /// </summary>
    public class FolderItem
    {
        private readonly FolderService _service = new FolderService();
        public string FullPath { get; set; }
        public string Name => System.IO.Path.GetFileName(FullPath);
        public ObservableCollection<FolderItem> SubFolders { get; set; } = new ObservableCollection<FolderItem>();
        public bool IsLoaded { get; set; }
        /// <summary>
        /// FolderItem with Path
        /// </summary>
        /// <param name="path"></param>
        public FolderItem(string path)
        {
            FullPath = path;
            SubFolders = new ObservableCollection<FolderItem>();
            IsLoaded = true;
        }
        public ICommand AddFolderCommand => new RelayCommand(AddFolder);
        public ICommand RefreshFolderCommand => new RelayCommand(ExpandFolder);
       
        private void AddFolder(object parent)
        {
            if (parent is FolderItem viewItem)
            {
                string newFolderName = "NewFolder"; // Ideally, get from user input
                if (_service.CreateFolder(viewItem.FullPath, newFolderName))
                {
                    FolderItem objNewNode = new FolderItem(System.IO.Path.Combine(viewItem.FullPath, newFolderName));
                    objNewNode.SubFolders.Add(null);
                    viewItem.SubFolders.Add(objNewNode);
                }
            }
        }

        private void ExpandFolder(object item)
        {
            if (item is FolderItem viewItem)
            {
                if (!viewItem.IsLoaded)
                {
                    viewItem.SubFolders.Clear();
                    foreach (var sub in _service.GetSubFolders(viewItem.FullPath))
                        viewItem.SubFolders.Add(sub);
                    viewItem.IsLoaded = true;
                }
            }
        }


    }
}
