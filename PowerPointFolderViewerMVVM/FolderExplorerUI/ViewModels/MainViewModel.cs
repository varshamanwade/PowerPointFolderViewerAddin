using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPointFolderViewerMVVM.FolderExplorerUI.Models;
using PowerPointFolderViewerMVVM.FolderExplorerUI.Services;

namespace PowerPointFolderViewerMVVM.FolderExplorerUI.ViewModels
{
    public class MainViewModel
    {
        private readonly FolderService _service = new FolderService();

        public ObservableCollection<FolderItem> Nodes { get; set; }

        public MainViewModel()
        {
            Nodes = new ObservableCollection<FolderItem>();
            var basePath = ConfigurationSettings.AppSettings["BaseFolderPath"];
            FolderItem obj = new FolderItem(basePath);
            
            //var basePath = ConfigurationSettings.AppSettings["BaseFolderPath"];
            foreach (var item in _service.GetSubFolders(basePath))
            {
                obj.SubFolders.Add(item);
            }
            Nodes.Add(obj);

        }
    }
}
