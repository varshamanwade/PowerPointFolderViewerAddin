using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerPointFolderViewerMVVM.FolderExplorerUI.Models;
using PowerPointFolderViewerMVVM.FolderExplorerUI.Helpers;
using System.Windows.Controls;

namespace PowerPointFolderViewerMVVM.FolderExplorerUI.Services
{
    public class FolderService
    {
        public IEnumerable<FolderItem> GetSubFolders(string path)
        {
            try
            {
                return Directory.GetDirectories(path)
                    .Select(p => { 
                        var subFolder = new FolderItem(p);
                        subFolder.SubFolders.Add(null);
                        return subFolder;
                    });
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to load subfolders: {ex.Message}");
                return Enumerable.Empty<FolderItem>();
            }
        }

        public bool CreateFolder(string parentPath, string folderName)
        {
            var newPath = Path.Combine(parentPath, folderName);
            try
            {
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                    return true;
                }
                else
                {
                    Logger.Log($"Debug: Folder Exist");
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to create folder: {ex.Message}");
                return false;
            }
        }
    }

}
