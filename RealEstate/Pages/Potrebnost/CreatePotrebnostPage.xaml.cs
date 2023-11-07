using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для CreatePotrebnostPage.xaml
    /// </summary>
    public partial class CreatePotrebnostPage : Page
    {
        public RealEstate.Potrebnost thisPotrebnost;
        public CreatePotrebnostPage(RealEstate.Potrebnost potr)
        {
            InitializeComponent();
            client.ItemsSource = nedvizhEntities1.GetContext().Client.Select(x => x.firstname + " " + x.middlename + " " + x.lastname).ToList();
            rieltor.ItemsSource = nedvizhEntities1.GetContext().Rieltor.Select(x => x.firstname + " " + x.middlename + " " + x.lastname).ToList();
            type.ItemsSource = nedvizhEntities1.GetContext().Type_Object.Select(x => x.name).ToList();
            adres.ItemsSource = nedvizhEntities1.GetContext().Address.Select(x => x.name).ToList();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.HeaderText = "Создание потребности";

            if (client == null)
            {
                mainWindow.HeaderText = "Создание потребности";
            }
            else
            {
                mainWindow.HeaderText = "Редактирование потребности";
                thisPotrebnost = potr;


                // Заполняем форму значениями из объекта potr
                client.SelectedItem = $"{potr.Client.firstname} {potr.Client.middlename} {potr.Client.lastname}";
                rieltor.SelectedItem = $"{potr.Rieltor.firstname} {potr.Rieltor.middlename} {potr.Rieltor.lastname}";
                type.SelectedItem = potr.Type_Object.name;
                adres.SelectedValue = potr.Address.name;
                min_p.Value = potr.min_price.HasValue ? (double)potr.min_price.Value : 0;
                max_p.Value = potr.max_price.HasValue ? (double)potr.max_price.Value : 0;
                min_a.Value = potr.min_area.HasValue ? (double)potr.min_area.Value : 0;
                max_a.Value = potr.max_area.HasValue ? (double)potr.max_area.Value : 0;
                min_r.Value = potr.min_rooms.HasValue ? (double)potr.min_rooms.Value : 0;
                max_r.Value = potr.max_rooms.HasValue ? (double)potr.max_rooms.Value : 0;
                min_f.Value = potr.min_floor.HasValue ? (double)potr.min_floor.Value : 0;
                max_f.Value = potr.max_floor.HasValue ? (double)potr.max_floor.Value : 0;


            }
        }
        private void type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedIndex == 2)
            {
                mir.Visibility = Visibility.Collapsed;
                mar.Visibility = Visibility.Collapsed;
                mif.Visibility = Visibility.Collapsed;
                maf.Visibility = Visibility.Collapsed;

            }
            else
            {
                mir.Visibility = Visibility.Visible;
                mar.Visibility = Visibility.Visible;
                mif.Visibility = Visibility.Visible;
                maf.Visibility = Visibility.Visible;

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

                int id_type = nedvizhEntities1.GetContext().Type_Object
                   .Where(x => x.name == type.SelectedItem.ToString())
                   .Select(x => x.id)
                   .FirstOrDefault();

                int id_address = nedvizhEntities1.GetContext().Address.Where(x => x.name == adres.SelectedValue.ToString()).FirstOrDefault().id;

                if (thisPotrebnost != null)
                {
                    var potrebnost = nedvizhEntities1.GetContext().Potrebnost.Where(x => x.id == thisPotrebnost.id).FirstOrDefault();
                    potrebnost.id_client = id_client;
                    potrebnost.id_rieltor = id_rieltor;
                    potrebnost.id_type = id_type;
                    potrebnost.id_address = id_address;
                    potrebnost.min_price = Convert.ToInt32(min_p.Value);
                    potrebnost.max_price = Convert.ToInt32(max_p.Value);
                    potrebnost.min_area = Convert.ToInt32(min_a.Value);
                    potrebnost.max_area = Convert.ToInt32(max_a.Value);
                    potrebnost.min_rooms = Convert.ToInt32(min_r.Value);
                    potrebnost.max_rooms = Convert.ToInt32(max_r.Value);
                    potrebnost.min_floor = Convert.ToInt32(min_f.Value);
                    potrebnost.max_floor = Convert.ToInt32(max_f.Value);
                }
                else
                {
                    // Создаем экземпляр класса Potrebnost и заполняем его свойства
                    RealEstate.Potrebnost potrebnost = new RealEstate.Potrebnost
                    {
                        id_client = id_client,
                        id_rieltor = id_rieltor,
                        id_type = id_type,
                        id_address = id_address,
                        min_price = Convert.ToInt32(min_p.Value),
                        max_price = Convert.ToInt32(max_p.Value),
                        min_area = Convert.ToInt32(min_a.Value),
                        max_area = Convert.ToInt32(max_a.Value),
                        min_rooms = Convert.ToInt32(min_r.Value),
                        max_rooms = Convert.ToInt32(max_r.Value),
                        min_floor = Convert.ToInt32(min_f.Value),
                        max_floor = Convert.ToInt32(max_f.Value),
                    };


                    nedvizhEntities1.GetContext().Potrebnost.Add(potrebnost);
                }
                nedvizhEntities1.GetContext().SaveChanges();
                Class.Inf.ContentFrame.Navigate(new Pages.Potrebnost.PotrebnostPage());
                MessageBox.Show("Успешно");
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }


        private void min_p_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = Math.Round(min_p.Value / 10000) * 10000;
            min_p_txt.Text = value.ToString();
        }

        private void max_p_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double value = Math.Round(max_p.Value / 10000) * 10000;
            max_p_txt.Text = value.ToString();
        }
    }
}
