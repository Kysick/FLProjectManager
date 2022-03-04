using SongManagerFL.Controllers;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MySongManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
      

        public MainPage()
        {
            this.InitializeComponent();
            FileManager fm = new FileManager();
            fm.GenerateProjectsFolder();
            _ = fm.DecomposeProjectsAsync();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFolder projectsFolder = await KnownFolders.MusicLibrary.GetFolderAsync("SongManagerProjects");
            IReadOnlyList<StorageFolder> projects = await projectsFolder.GetFoldersAsync();
            foreach (StorageFolder folder in projects)
                filesList.Items.Add(folder.DisplayName);
        }
    }
}
