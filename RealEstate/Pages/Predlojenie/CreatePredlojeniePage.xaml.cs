using RealEstate.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для CreatePredlojeniePage.xaml
    /// </summary>
    public partial class CreatePredlojeniePage : Page
    {
        public RealEstate.Predlojenie thisPredlojenie;
        public CreatePredlojeniePage(RealEstate.Predlojenie predlojenie)
        {
            InitializeComponent();
            client.ItemsSource = nedvizhEntities1.GetContext().Client.Select(x => x.firstname + " " + x.middlename + " " + x.lastname).ToList();
            rieltor.ItemsSource = nedvizhEntities1.GetContext().Rieltor.Select(x => x.firstname + " " + x.middlename + " " + x.lastname).ToList();
            obj.ItemsSource = nedvizhEntities1.GetContext().Object_Nedv.Select(x => x.id).ToList();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            if (predlojenie == null)
            {
                mainWindow.HeaderText = "Создание предложения";
            }
            else
            {
                mainWindow.HeaderText = "Редактирование предложения";
                thisPredlojenie = predlojenie;

                // Заполнение контролов значениями из предложения
                client.SelectedItem = $"{predlojenie.Client.firstname} {predlojenie.Client.middlename} {predlojenie.Client.lastname}";
                obj.SelectedItem = predlojenie.Object_Nedv.id;

                rieltor.SelectedItem = $"{predlojenie.Rieltor.firstname} {predlojenie.Rieltor.middlename} {predlojenie.Rieltor.lastname}";
                price.Value = predlojenie.price;
            }
        }

        private void Popup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id_client = nedvizhEntities1.GetContext().Client
                    .Where(x => (x.firstname + " " + x.middlename + " " + x.lastname) == client.SelectedItem.ToString())
                    .Select(x => x.id)
                    .FirstOrDefault();

                int id_rieltor = nedvizhEntities1.GetContext().Rieltor
                   .Where(x => (x.firstname + " " + x.middlename + " " + x.lastname) == rieltor.SelectedItem.ToString())
                   .Select(x => x.id)
                   .FirstOrDefault();
                if (thisPredlojenie != null)
                {
                    var predlojenie = nedvizhEntities1.GetContext().Predlojenie.Where(x => x.id == thisPredlojenie.id).FirstOrDefault();
                    predlojenie.id_client = id_client;
                    predlojenie.id_rieltor = id_rieltor;
                    predlojenie.id_object = Convert.ToInt32(obj.SelectedItem.ToString());
                    predlojenie.price = Convert.ToInt32(price.Value);
                   
                }
                else
                {

                    // Создаем экземпляр класса Potrebnost и заполняем его свойства
                    RealEstate.Predlojenie predlojenie = new RealEstate.Predlojenie
                    {
                        id_client = id_client,
                        id_rieltor = id_rieltor,
                        id_object = Convert.ToInt32(obj.SelectedItem.ToString()),
                        price = Convert.ToInt32(price.Value)

                    };

                    // Добавляем объект Potrebnost в контекст данных и сохраняем изменения
                    nedvizhEntities1.GetContext().Predlojenie.Add(predlojenie);
                }
                nedvizhEntities1.GetContext().SaveChanges();
                Class.Inf.ContentFrame.Navigate(new Pages.Predlojenie.PredlojeniePage());
                MessageBox.Show("Успешно");
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
