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
using Windows.Storage.FileProperties;
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
        public ObservableCollection<MusicProject> FilteredMusicProjects { get; set; }

       

        FileManager fm;
        public MainPage()
        {
            this.InitializeComponent();
            fm = new FileManager();
            fm.GenerateProjectsFolder();
            _ = fm.DecomposeProjectsAsync();


        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MusicProjects = await fm.FillMusicProjectList();
            FilteredMusicProjects = MusicProjects;
            filesList.ItemsSource = FilteredMusicProjects;

        }
        private void filesList_SelectionChanged(object sender, ItemClickEventArgs e)
        {
            MusicProject selectedProject = (MusicProject)e.ClickedItem;
            Frame.Navigate(typeof(ProjectPage), selectedProject);
        }

        private async void OnFilterChanged(object sender, TextChangedEventArgs args)
        {
            // This is a Linq query that selects only items that return True after being passed through
            // the Filter function, and adds all of those selected items to filtered.
            MusicProjects = await fm.FillMusicProjectList();
            var filtered = MusicProjects.Where(mp => Filter(mp));
            Remove_NonMatching(filtered);

            AddBack_Projects(filtered);

        }
        private bool Filter(MusicProject mp)
        {
            return mp.ProjectName.Contains(SearchBox.Text, StringComparison.InvariantCultureIgnoreCase);
        }
        private void Remove_NonMatching(IEnumerable<MusicProject> filteredData)
        {
            for (int i = FilteredMusicProjects.Count - 1; i >= 0; i--)
            {
                var item = FilteredMusicProjects[i];
                // If contact is not in the filtered argument list, remove it from the ListView's source.
                if (!filteredData.Contains(item))
                {
                    FilteredMusicProjects.Remove(item);
                }
            }
        }
        private void AddBack_Projects(IEnumerable<MusicProject> filteredData)
        {
            foreach (var item in filteredData)
            {
                // If item in filtered list is not currently in ListView's source collection, add it back in
                if (!FilteredMusicProjects.Contains(item))
                {
                    FilteredMusicProjects.Add(item);
                }
            }


        }
    }
}
