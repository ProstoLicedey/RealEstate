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

namespace RealEstate.Pages.Predlojenie
{
    /// <summary>
    /// Логика взаимодействия для PredlojeniePage.xaml
    /// </summary>
    public partial class PredlojeniePage : Page
    {
        public PredlojeniePage()
        {
            InitializeComponent();
            DbGridModel.ItemsSource = nedvizhEntities1.GetContext().Predlojenie.ToList();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.HeaderText = "Предложения";
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить объект недвижимости?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    var delete = ((FrameworkElement)sender).DataContext as RealEstate.Predlojenie;
                    nedvizhEntities1.GetContext().Predlojenie.Remove(delete);
                    nedvizhEntities1.GetContext().SaveChanges();
                    DbGridModel.ItemsSource = nedvizhEntities1.GetContext().Predlojenie.ToList();
                }
                catch
                {
                    MessageBox.Show("Ошибка", "Внимание");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Class.Inf.ContentFrame.Navigate(new Pages.Predlojenie.CreatePredlojeniePage(null));
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            RealEstate.Predlojenie cl = (RealEstate.Predlojenie)button.DataContext;
            Class.Inf.ContentFrame.Navigate(new Pages.Predlojenie.CreatePredlojeniePage(cl));
        }
    }
}
