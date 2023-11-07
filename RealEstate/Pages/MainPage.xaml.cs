using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace RealEstate.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Class.Inf.ContentFrame = ContentFrame;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Pages.Client.ClientPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Pages.Rieltor.RieltorPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Pages.Object.ObjectPage());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Pages.Potrebnost.PotrebnostPage());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Pages.Predlojenie.PredlojeniePage());
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Pages.Sdelka.SdelkaPage());
        }
    }
}
