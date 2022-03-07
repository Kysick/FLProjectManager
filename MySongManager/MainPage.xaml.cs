using SongManagerFL.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;



namespace MySongManager
{

    public sealed partial class MainPage : Page
    {
        public ObservableCollection<MusicProject> MusicProjects { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            FileManager fm = new FileManager();
            fm.GenerateProjectsFolder();
            _ = fm.DecomposeProjectsAsync();


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MusicProjects = new ObservableCollection<MusicProject>();
            StorageFolder projectsFolder = await KnownFolders.MusicLibrary.GetFolderAsync("SongManagerProjects");
            IReadOnlyList<StorageFolder> projects = await projectsFolder.GetFoldersAsync();
            foreach (StorageFolder folder in projects)
            {
                MusicProjects.Add(new MusicProject(folder.DisplayName,folder.Path,folder.DateCreated));
            }

            filesList.ItemsSource = MusicProjects;
        }


    }
}
