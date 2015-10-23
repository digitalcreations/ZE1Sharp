using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ZE1Sharp.Universal
{
    using System.Collections.ObjectModel;

    using Windows.UI.Xaml.Media.Imaging;

    using ZE1Sharp.Models;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly CameraController _controller;

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this._controller = new CameraController();
            //this.Files = new ObservableCollection<Thumbnail>()
            //                 {
            //                     new Thumbnail("test", null)
            //                 };
        }

        //public ObservableCollection<Thumbnail> Files { get; private set; }

        //protected override async void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);

        //    var root = await this._controller.FileSystem.GetFolderAsync();
        //    var folder = await this._controller.FileSystem.GetFolderAsync(root.First());
        //    foreach (var file in folder)
        //    {
        //        try
        //        {
        //            var thumbnail = await this._controller.FileSystem.DownloadThumbnailAsync(file);
        //            var bitmapSource = new BitmapImage();
        //            await bitmapSource.SetSourceAsync(thumbnail.Stream.AsRandomAccessStream());
        //            this.Files.Add(new Thumbnail(file.FileName, bitmapSource));
        //        }
        //        catch (NullReferenceException)
        //        {
        //            // do nothing!
        //        }
        //    }
        //}

        private async void Capture(object sender, RoutedEventArgs e)
        {
            this.Progress.IsActive = true;
            await this._controller.SetModeAsync(CameraGenericMode.Still);
            var file = await this._controller.Still.StartCaptureAsync();

            var bitmapSource = new BitmapImage();

            var thumb = await this._controller.FileSystem.DownloadThumbnailAsync(file);
            await bitmapSource.SetSourceAsync(thumb.Stream.AsRandomAccessStream());
            this.ThumbnailImage.Source = bitmapSource;

            var full = await this._controller.FileSystem.DownloadFileAsync(file);
            await bitmapSource.SetSourceAsync(full.Stream.AsRandomAccessStream());
            this.ThumbnailImage.Source = bitmapSource;
            this.Progress.IsActive = false;
        }

        public class Thumbnail
        {
            public string Name { get; private set; }

            public BitmapSource Bitmap { get; private set; }

            public Thumbnail(string name, BitmapSource bitmap)
            {
                this.Name = name;
                this.Bitmap = bitmap;
            }
        }
    }
}
