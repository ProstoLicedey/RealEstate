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

namespace RealEstate.Pages.Client
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            DbGridModel.ItemsSource = nedvizhEntities1.GetContext().Client.ToList();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                mainWindow.HeaderText = "Клиент";

        }

        private void TbPoisk(object sender, RoutedEventArgs e)
        {
            var listClient = nedvizhEntities1.GetContext().Client.ToList();
            List<RealEstate.Client> ListGrid = null;

            if (PoiskTB.Text != null)
            {
                string searchText = PoiskTB.Text.ToLower();
                ListGrid = listClient
                    .Where(x => x.firstname.ToLower().Contains(searchText) ||
                                x.lastname.ToLower().Contains(searchText) ||
                                x.middlename.ToLower().Contains(searchText))
                    .ToList();
            }

            DbGridModel.ItemsSource = null;
            DbGridModel.ItemsSource = ListGrid;
            DbGridModel.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Class.Inf.ContentFrame.Navigate(new Pages.Client.CreateClientPage(null));
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить объект недвижимости?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try { 
                var deleteClient = ((FrameworkElement)sender).DataContext as RealEstate.Client;
                nedvizhEntities1.GetContext().Client.Remove(deleteClient);
                nedvizhEntities1.GetContext().SaveChanges();
                DbGridModel.ItemsSource = nedvizhEntities1.GetContext().Client.ToList();
                }
                catch {
                    MessageBox.Show("Ошибка", "Внимание");
                        }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            RealEstate.Client cl = (RealEstate.Client)button.DataContext;
            Class.Inf.ContentFrame.Navigate(new Pages.Client.CreateClientPage(cl));
        }
    }
}
