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

namespace RealEstate.Pages.Object
{
    /// <summary>
    /// Логика взаимодействия для ObjectPage.xaml
    /// </summary>
    public partial class ObjectPage : Page
    {
        public ObjectPage()
        {
            InitializeComponent();
            DbGridModel.ItemsSource = nedvizhEntities1.GetContext().Object_Nedv.ToList();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.HeaderText = "Объекты недвижимости";
        }
        private void TbPoisk(object sender, RoutedEventArgs e)
        {
            var listClient = nedvizhEntities1.GetContext().Object_Nedv.ToList();
            List<RealEstate.Object_Nedv> ListGrid = new List<RealEstate.Object_Nedv>();

            if (PoiskTB.Text != null)
            {
                string searchText = PoiskTB.Text.ToLower();
                ListGrid = listClient
                    .Where(x => (x.Address != null && x.Address.name != null && x.Address.name.ToLower().Contains(searchText)) ||
                                (x.city != null && x.city.ToLower().Contains(searchText)))
                    .ToList();
            }

            DbGridModel.ItemsSource = null;
            DbGridModel.ItemsSource = ListGrid;
            DbGridModel.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Class.Inf.ContentFrame.Navigate(new Pages.Object.ObjectCreatePage(null));
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить объект недвижимости?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    
                    var delete = ((FrameworkElement)sender).DataContext as RealEstate.Object_Nedv;
                    var wd = nedvizhEntities1.GetContext().Predlojenie.Where(x => x.id_object == delete.id).ToList();


                    if (nedvizhEntities1.GetContext().Predlojenie.Where(x => x.id_object == delete.id).ToList() == null)
                    {
                        MessageBox.Show("Нельзя удалить объект участвуещие в предложение, сначало удалите предожение", "Внимание");
                        return;
                    }
                    nedvizhEntities1.GetContext().Object_Nedv.Remove(delete);
                    nedvizhEntities1.GetContext().SaveChanges();
                    DbGridModel.ItemsSource = nedvizhEntities1.GetContext().Object_Nedv.ToList();
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
            RealEstate.Object_Nedv objectt = (RealEstate.Object_Nedv)button.DataContext;
            Class.Inf.ContentFrame.Navigate(new Pages.Object.ObjectCreatePage(objectt));
        }
    }
}