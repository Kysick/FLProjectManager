
using MySongManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace SongManagerFL.Controllers
{
    internal class FileManager
    {
        private StorageFolder mainFolder;

 

        public void GenerateProjectsFolder()
        {
            //Generating folder for projects 
            mainFolder = KnownFolders.MusicLibrary;
            if (!Directory.Exists(mainFolder.Path + "/SongManagerProjects"))
                _ = mainFolder.CreateFolderAsync("SongManagerProjects");

        }



        public async Task DecomposeProjectsAsync()
        {
            StorageFolder songManagerFolder = await mainFolder.GetFolderAsync("SongManagerProjects");
            IReadOnlyList<StorageFile> files = await songManagerFolder.GetFilesAsync();

            foreach (StorageFile file in files)
            {
                string currentFileName = file.Name;
                if (file.Name.Contains("_demo") || file.Name.Contains("_final") )
                {
                    currentFileName = currentFileName.Remove(currentFileName.IndexOf('_'));
                }
                else
                {
                    currentFileName = file.DisplayName;
                }


                StorageFolder newProjectFolder;
                if (!Directory.Exists(songManagerFolder.Path + "\\" + currentFileName))
                    _ = songManagerFolder.CreateFolderAsync(currentFileName);

                newProjectFolder = await songManagerFolder.GetFolderAsync(currentFileName);
                await file.MoveAsync(newProjectFolder, file.Name);
            }
        }

        public async Task<ObservableCollection<MusicProject>> FillMusicProjectList()
        {
            ObservableCollection<MusicProject> list = new ObservableCollection<MusicProject>();
            StorageFolder projectsFolder = await KnownFolders.MusicLibrary.GetFolderAsync("SongManagerProjects");
            IReadOnlyList<StorageFolder> projects = await projectsFolder.GetFoldersAsync();
            foreach (StorageFolder folder in projects)
            {
                BasicProperties basic = await folder.GetBasicPropertiesAsync();

                list.Add(new MusicProject(folder.DisplayName, folder.Path, basic.DateModified.DateTime));
            }

            return list;
        }

        







    }
}
