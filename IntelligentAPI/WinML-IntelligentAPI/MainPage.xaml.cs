using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


using CommunityToolkit.Labs.Intelligent.ImageClassification;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WinML_IntelligentAPI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            //Instantiate file picker and store selected file
            FileOpenPicker fileOpenPicker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                ViewMode = PickerViewMode.Thumbnail
            };
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".png");
            StorageFile selectedStorageFile = await fileOpenPicker.PickSingleFileAsync();


            //Call ClassifyImage to get image classes
            List<ClassificationResult> list = await SqueezeNetImageClassifier.ClassifyImage(selectedStorageFile, 3);

            //Display Image and result
            ResultsBlock.Text = "1st guess: " + list[0].category + "\nwith confidence: " + list[0].confidence + " \n\n\n2nd guess: " + list[1].category + "\nwith confidence: " + list[1].confidence + " \n\n\n3rd guess: " + list[2].category + "\nwith confidence: " + list[2].confidence;

            await DisplayImage(selectedStorageFile);

        }

        private async Task DisplayImage(StorageFile selectedStorageFile)
        {
            SoftwareBitmap softwareBitmap;
            using (IRandomAccessStream stream = await selectedStorageFile.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream 
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                // Get the SoftwareBitmap representation of the file in BGRA8 format
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }

            // Display the image
            SoftwareBitmapSource imageSource = new SoftwareBitmapSource();
            await imageSource.SetBitmapAsync(softwareBitmap);
            UIPreviewImage.Source = imageSource;
        }
    }
}
