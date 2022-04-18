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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MySongManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProjectPage : Page
    {
        public MusicProject CurrentMS { get; set; }
        public ObservableCollection<MusicProject> MusicDemoProjects { get; set; }
        public ProjectPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            FileManager fm = new FileManager();
            CurrentMS = (MusicProject)e.Parameter;
            Title.Text = CurrentMS.ProjectName;
            ProjectPath.Text = CurrentMS.ProjectPath;
            MusicDemoProjects =  await fm.GetProjectFiles(CurrentMS.ProjectName);
            
            ProjectDemosList.ItemsSource = MusicDemoProjects;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            FileManager fm = new FileManager();
            _ = fm.DeleteProjectFolder(CurrentMS.ProjectPath, CurrentMS.ProjectName);
            _ = fm.DecomposeProjectsAsync();
            BackButton_Click(sender, e);
        }

        private void OpenProjectBTN_Click(object sender, RoutedEventArgs e)
        {
            FileManager fm = new FileManager();
            fm.ProjectLaunch(CurrentMS.ProjectName);
        }

        private void ProjectDemosList_ItemClick(object sender, ItemClickEventArgs e)
        {
            FileManager fm = new FileManager();
            MusicProject currentSong = (MusicProject) e.ClickedItem; 
            fm.DefaultLaunch(currentSong.StorageFile);
        }
    }
}
