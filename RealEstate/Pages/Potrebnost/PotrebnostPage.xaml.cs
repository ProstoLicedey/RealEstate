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

namespace RealEstate.Pages.Potrebnost
{
    /// <summary>
    /// Логика взаимодействия для PotrebnostPage.xaml
    /// </summary>
    public partial class PotrebnostPage : Page
    {
        public PotrebnostPage()
        {
            InitializeComponent();
            DbGridModel.ItemsSource = nedvizhEntities1.GetContext().Potrebnost.ToList();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.HeaderText = "Потребности";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Class.Inf.ContentFrame.Navigate(new Pages.Potrebnost.CreatePotrebnostPage(null));
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить объект недвижимости?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    var deleteClient = ((FrameworkElement)sender).DataContext as RealEstate.Potrebnost;
                    nedvizhEntities1.GetContext().Potrebnost.Remove(deleteClient);
                    nedvizhEntities1.GetContext().SaveChanges();
                    DbGridModel.ItemsSource = nedvizhEntities1.GetContext().Potrebnost.ToList();
                }
                catch
                {
                    MessageBox.Show("Ошибка", "Внимание");
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            RealEstate.Potrebnost potr = (RealEstate.Potrebnost)button.DataContext;
            Class.Inf.ContentFrame.Navigate(new Pages.Potrebnost.CreatePotrebnostPage(potr));
        }

    }
}
