//Written by Dmitry Mironov 1/4/2021
//Just for fun :-)


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
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;//bitmap
using Windows.UI.Xaml.Media.Animation;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UniversalApplication
{
    
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            //Set the default window size. 800x600
            ApplicationView.PreferredLaunchViewSize = new Size(800, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            InitializeComponent();

          
        }

        //Show  the actual Message
        private  void Button_ShowDialog_Click(object sender, RoutedEventArgs e)
        {
            //Calling our method
            ShowMessageDialog();
        }


        //Logic....Under the Hood..........................
        #region custom methods
        public async void ShowMessageDialog()
        {
            await PlayClickSound();
            await PlayShortMusic();
            await new MessageDialog("This is our Message\n" +
                  "You've just cliked a button","Message Box").ShowAsync();
        }

        //music methods
        public async Task<MediaElement> PlayClickSound()
        {
            var element = new MediaElement();
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Sound");
            var file = await folder.GetFileAsync("ButtonClick.wav");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
            return element;
            
        }
        public async Task<MediaElement> PlayShortMusic()
        {
            var element = new MediaElement();
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Sound");
            var file = await folder.GetFileAsync("Win.wav");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
            return element;
        }
        public async Task<MediaElement> PlayTwoPacSong()
        {
            var element = new MediaElement();
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Sound");
            var file = await folder.GetFileAsync("2Pac.mp3");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
            return element;
        }

        //Rotation custom methods
        private Storyboard rotation = new Storyboard();
        private bool IsRotationActive = false;
        private void RotateImage()
        {
            //This code rotates the image
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0.0;
            animation.To = 360;
            animation.BeginTime = TimeSpan.FromSeconds(1);
            animation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(animation, image1);
            Storyboard.SetTargetProperty(animation, "(UIElement.Projection).(PlaneProjection.Rotation" + "Y" + ")");
            rotation.Children.Clear();
            rotation.Children.Add(animation);
            rotation.Begin();
            IsRotationActive = true;
            if (IsRotationActive)
            {
                btn7.Content = "Stop Rotation";

            }
            else
            {
                btn7.Content = "Start Rotation";
                IsRotationActive = false;
            }

        }
        private void StopRotation()
        {
            //Stops the rotation
            rotation.Stop();
            IsRotationActive = false;
            btn7.Content = "Start Rotation";
        }


        //Resets the bg color
        private void ResetButtonsColor(object sender, RoutedEventArgs e)
        {
            //Sets the background color of unused buttons
            //Only pressed buttons remain red.
            //sender is a "pressed button".
            //We extract pressed button's Name property from a sender object, and pass it to the switch.
            //Getting the value out of the sender object.
            string searchable = Convert.ToString(e.OriginalSource.GetType().GetProperty("Name").GetValue(e.OriginalSource, null));
            switch (searchable)
            {
                case "btn1":
                    //btn1.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn2.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn3.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn4.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn5.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    break;
                case "btn2":
                    btn1.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    // btn2.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn3.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn4.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn5.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    break;
                case "btn3":
                    btn1.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn2.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    // btn3.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn4.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn5.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    break;
                case "btn4":
                    btn1.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn2.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn3.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    // btn4.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn5.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    break;
                case "btn5":
                    btn1.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn2.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn3.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    btn4.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    // btn5.Background = (SolidColorBrush)Resources["ButtonDefaultColor"];
                    break;

            }

        }

        //Exit Button
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        //RotateImage toggle On-Off
        private void btnRotateToggleOnOff_Click(object sender, RoutedEventArgs e)
        {
            if (IsRotationActive)
            {
                StopRotation();
            }
            else
            {
                RotateImage();
            }


        }


        //Helper variables for PlayMusic control
        MediaElement MyMediaElement = new MediaElement();
        public bool IsSongPlaying = false;
        private async void PlayMusic_Click(object sender, RoutedEventArgs e)
        {

            //check if music is playing
            //Change the button content accordingly
            if (IsSongPlaying)
            {
                //playing the song
                MyMediaElement.Stop();

                //Change the button content to Play
                btnSongPlay.Content = "PLay Song";
                IsSongPlaying = false;
            }
            else
            {
                //putting song to a new element
                MyMediaElement = await PlayTwoPacSong();

                //Play the music
                MyMediaElement.Play();
                //change the button content to Play
                btnSongPlay.Content = "Stop";
                IsSongPlaying = true;

            }

        }
        #endregion


        //Set bg-color by pressing different buttons.
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            //set new bg color
            GridMain.Background = new SolidColorBrush(Windows.UI.Colors.Azure);
            //set button color
            btn1.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            ResetButtonsColor(sender,e);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Background = new SolidColorBrush(Windows.UI.Colors.DarkOrange);
            //set button color
            btn2.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            //send buttons arttributes to the reset method.
            ResetButtonsColor(sender, e);
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            //set grids color
            GridMain.Background = (SolidColorBrush)Resources["BlueColor"];
            //set button color
            btn3.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            ResetButtonsColor(sender, e);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Background = (SolidColorBrush)Resources["YellowColor"];
            //set button color
            btn4.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            //rest the color
            ResetButtonsColor(sender, e);
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Background = (SolidColorBrush)Resources["GreenColor"];
            //set button color
            btn5.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            ResetButtonsColor(sender, e);
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {

            //go to the next page
            this.Frame.Navigate(typeof(NewPage), null);
            //set the ismusic playing to false
            //otherwise you want be able to stop the music
            //when you come back from previous page.
            IsSongPlaying = false;
            btnSongPlay.Content = "stop"; 
        }
    }
}
