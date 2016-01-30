using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Marketplace;

namespace markDice
{
    public partial class MainPage : PhoneApplicationPage
    {
        LicenseInformation linformation = new LicenseInformation();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            var bgc = Resources["PhoneBackgroundColor"].ToString();
            if (bgc == "#FF000000")
            {   
                imgLogo.Source = new BitmapImage(new Uri("Images/logoMarkDIceInvert.jpg", UriKind.Relative));
                imgLogoMarksys.Source = new BitmapImage(new Uri("Images/logoMarksysBlack.jpg", UriKind.Relative));
                
            }
            else
            {
                imgLogo.Source = new BitmapImage(new Uri("Images/logoMarkDIce.jpg", UriKind.Relative));
                imgLogoMarksys.Source = new BitmapImage(new Uri("Images/logoMarksysWhite.jpg", UriKind.Relative));
                
            }

            if (linformation.IsTrial())
                txtTrial.Text = "Trial Version";
            else
                txtTrial.Text = "Full Version";

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/CreateDice.xaml", UriKind.Relative));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/CubeTest.xaml", UriKind.Relative));
        }
    }
}