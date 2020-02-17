using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await DownloadImageAsync(this.Image1, "https://www.pasadenastarnews.com/wp-content/uploads/2019/04/A471529_Alice_b-1.jpg?w=467");
            await DownloadImageAsync(this.Image2, "https://icatcare.org/app/uploads/2018/07/Thinking-of-getting-a-cat.png");
            await DownloadImageAsync(this.Image3, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/angry-cat-looking-the-lens-royalty-free-image-1579204792.jpg");
            await DownloadImageAsync(this.Image4, "https://pbs.twimg.com/profile_images/1214161197123039233/h4KIya_e_400x400.jpg");
            await DownloadImageAsync(this.Image5, "https://static.boredpanda.com/blog/wp-content/uploads/2020/01/Japanese-illustrator-makes-hyper-realistic-cat-illustrations-that-will-probably-take-your-breath-away-5e1c1763ef0a5__880.jpg");
            await DownloadImageAsync(this.Image6, "https://icatcare.org/app/uploads/2019/02/declaration-1920x660.png");

        }

        private async Task DownloadImageAsync(Image image, string url)
        {
            var client = new HttpClient();
            await Task.Run(()=> Thread.Sleep(2000));
            var request = await client.GetAsync(url);
            var byteData = await request.Content.ReadAsByteArrayAsync();
            image.Source = this.LoadImage(byteData);
        }

        private BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
