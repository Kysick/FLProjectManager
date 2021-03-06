
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
        private StorageFolder mainFolder = KnownFolders.MusicLibrary;



        public void GenerateProjectsFolder()
        {
            //Generating folder for projects 

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
                if (file.Name.Contains("_demo") || file.Name.Contains("_final"))
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

            //OrderBy date modified
            list = new ObservableCollection<MusicProject>(list.OrderByDescending(x => x.Date));

            return list;
        }

        public async Task<ObservableCollection<MusicProject>> FillMusicProjectDemosList(string folderName)
        {
            ObservableCollection<MusicProject> list = new ObservableCollection<MusicProject>();
            StorageFolder projectsFolder = await KnownFolders.MusicLibrary.GetFolderAsync("SongManagerProjects");
            StorageFolder demos = await projectsFolder.GetFolderAsync(folderName);
            IReadOnlyList<StorageFile> projects = await demos.GetFilesAsync();
            foreach (StorageFile demo in projects)
            {
                if (!demo.Name.Contains(".flp"))
                {
                    BasicProperties basic = await demo.GetBasicPropertiesAsync();
                    list.Add(new MusicProject(demo.DisplayName, demo.Path, basic.DateModified.DateTime));
                }
            }

            //OrderBy date modified
            list = new ObservableCollection<MusicProject>(list.OrderByDescending(x => x.Date));

            return list;
        }

        public async Task DeleteProjectFolder(string path, string name)
        {

            StorageFolder projectsFolder = await KnownFolders.MusicLibrary.GetFolderAsync("SongManagerProjects");
            StorageFolder deletedFolder = await projectsFolder.GetFolderAsync(name);
            await deletedFolder.DeleteAsync();

        }

        public async Task<ObservableCollection<MusicProject>> GetProjectFiles(string projectName)
        {
            ObservableCollection<MusicProject> files = new ObservableCollection<MusicProject>();
            IStorageFolder mainFolder = await KnownFolders.MusicLibrary.GetFolderAsync("SongManagerProjects");
            IStorageFolder projectFolder = await mainFolder.GetFolderAsync(projectName);
            var filesList = await projectFolder.GetFilesAsync();
            foreach(var file in filesList)
            {
                if (file.Name.Contains(".flp"))
                    continue;
                BasicProperties basic = await file.GetBasicPropertiesAsync();
                files.Add(new MusicProject(file.DisplayName, file.Path, basic.DateModified.DateTime, file));
            }
            return files;
        }
   

        public async void CreateProject(string projectName)
        {
           StorageFolder projectsFolder = await KnownFolders.MusicLibrary.GetFolderAsync("SongManagerProjects");

            if (!Directory.Exists(projectsFolder.Path + "/" + projectName))
            {
                await projectsFolder.CreateFolderAsync(projectName);
                StorageFolder newProject = await projectsFolder.GetFolderAsync(projectName);
                await newProject.CreateFileAsync(projectName + ".flp");
                
                ProjectLaunch(projectName);

            }
               

           

        }

        public async void ProjectLaunch(string projectName)
        {
            IStorageFolder projectsFolder = await KnownFolders.MusicLibrary.GetFolderAsync("SongManagerProjects");
            IStorageFolder currentProject = await projectsFolder.GetFolderAsync(projectName);

            try
            {
                IStorageFile file = await currentProject.GetFileAsync(projectName + ".flp");

                if (file != null)
                {
                    // Launch the retrieved file
                    var success = await Windows.System.Launcher.LaunchFileAsync(file);

                    if (success)
                    {
                        // File launched
                    }
                    else
                    {
                        // File launch failed
                    }
                }
                else
                {
                    // Could not find file
                }
            }
            catch (Exception ex) { }

        }

        public async void DefaultLaunch(IStorageFile file)
        {
           
            try
            {              

                if (file != null)
                {
                    // Launch the retrieved file
                    var success = await Windows.System.Launcher.LaunchFileAsync(file);

                    if (success)
                    {
                        // File launched
                    }
                    else
                    {
                        // File launch failed
                    }
                }
                else
                {
                    // Could not find file
                }
            }
            catch (Exception ex) { }
      

           
        }
    }

    }

