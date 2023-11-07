using RealEstate.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
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

namespace RealEstate.Pages.Sdelka
{
    /// <summary>
    /// Логика взаимодействия для CreateSdelkaPage.xaml
    /// </summary>
    public partial class CreateSdelkaPage : Page
    {
        public RealEstate.Sdelka thisSdelka;
        public CreateSdelkaPage(RealEstate.Sdelka sdelka)
        {
            InitializeComponent();
            potreb.ItemsSource = nedvizhEntities1.GetContext().Potrebnost.Select(x => x.id).ToList();
            predl.ItemsSource = nedvizhEntities1.GetContext().Predlojenie.Select(x => x.id).ToList();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.HeaderText = "Создание сделки";

            if (sdelka == null)
            {
                mainWindow.HeaderText = "Создание сделки";
            }
            else
            {
                mainWindow.HeaderText = "Редактирование сделки";
                thisSdelka = sdelka;

                potreb.SelectedItem = sdelka.Potrebnost.id;
                predl.SelectedItem = sdelka.Predlojenie.id;

            }
        }

        private void Popup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (thisSdelka != null)
                {
                    var sdelka = nedvizhEntities1.GetContext().Sdelka.Where(x => x.id == thisSdelka.id).FirstOrDefault();
                    sdelka.id_potr = Convert.ToInt32(potreb.SelectedValue.ToString());
                    sdelka.id_pred = Convert.ToInt32(predl.SelectedValue.ToString());



                }
                else
                {

                    // Создаем экземпляр класса Potrebnost и заполняем его свойства
                    RealEstate.Sdelka sdelka = new RealEstate.Sdelka
                    {
                        id_potr = Convert.ToInt32(potreb.SelectedValue.ToString()),
                        id_pred = Convert.ToInt32(predl.SelectedValue.ToString())
                    };

                    // Добавляем объект Potrebnost в контекст данных и сохраняем изменения
                    nedvizhEntities1.GetContext().Sdelka.Add(sdelka);
                }
                nedvizhEntities1.GetContext().SaveChanges();
                Class.Inf.ContentFrame.Navigate(new Pages.Sdelka.SdelkaPage());
                MessageBox.Show("Успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
