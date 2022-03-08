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
        public  ObservableCollection<MusicProject> MusicProjects { get; set; }
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
           
  
            filesList.ItemsSource = await fm.FillMusicProjectList(); 
        }


    }
}
