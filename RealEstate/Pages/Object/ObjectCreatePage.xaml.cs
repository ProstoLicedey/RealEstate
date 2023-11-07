using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace RealEstate.Pages.Object
{
    /// <summary>
    /// Логика взаимодействия для ObjectCreatePage.xaml
    /// </summary>
    public partial class ObjectCreatePage : Page
    {
        public RealEstate.Object_Nedv thisObject;
        public ObjectCreatePage(RealEstate.Object_Nedv Objectt)
        {
            InitializeComponent();
            type.ItemsSource = nedvizhEntities1.GetContext().Type_Object.Select(x => x.name).ToList().Distinct().OrderBy(x => x).ToList();
            adres.ItemsSource = nedvizhEntities1.GetContext().Address.Select(x => x.name).ToList().Distinct().OrderBy(x => x).ToList();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
             if (Objectt == null)
            {
                mainWindow.HeaderText = "Создание объекта недвижимости";
            }
            else
            {
                mainWindow.HeaderText = "Редактирование объекта недвижимости";
                thisObject = Objectt;

                // Заполнение контролов значениями из объекта
                type.SelectedValue = nedvizhEntities1.GetContext().Type_Object
                    .Where(x => x.id == Objectt.id_type)
                    .Select(x => x.name)
                    .FirstOrDefault();

                city.Text = Objectt.city;
                adres.SelectedValue = nedvizhEntities1.GetContext().Address
                    .Where(x => x.id == Objectt.id_address)
                    .Select(x => x.name)
                    .FirstOrDefault();

                // Дополнительные настройки видимости и значений контролов в зависимости от типа объекта
                num_h.Value = Objectt.num_house ?? 0;
                num_k.Value = Objectt.num_kv ?? 0;
                shir.Text = Objectt.shirina;
                dolg.Text = Objectt.dolgota;
                area.Value = Objectt.area;
                kolvo_k.Value = Objectt.rooms ?? 0;
                floor.Value = Objectt.floor ?? 0;

                // Регулировка видимости на основе выбранного типа
                type_SelectionChanged(type, null);

            }

        }

        private void Popup_Click(object sender, RoutedEventArgs e)
        {
            int ty = nedvizhEntities1.GetContext().Type_Object
                .Where(x => x.name == type.SelectedValue.ToString())
                .FirstOrDefault().id;
            string ci = city.Text;
            int ad = nedvizhEntities1.GetContext().Address
                .Where(x => x.name == adres.SelectedValue.ToString())
                .FirstOrDefault().id;
            int nh = Convert.ToInt32(num_h.Value);
            int nk = Convert.ToInt32(num_k.Value);
            string sh = shir.Text;
            string dol = dolg.Text;
            double ar = Convert.ToInt32(area.Value);
            int ko = Convert.ToInt32(kolvo_k.Value);
            int fl = Convert.ToInt32(floor.Value);
            int a;

            if (thisObject != null)
            {
                // Обновление существующего объекта
                thisObject.id_type = ty;
                thisObject.city = ci;
                thisObject.id_address = ad;
                thisObject.num_house = (nh == 0) ? (int?)null : nh;
                thisObject.num_kv = (nk == 0) ? (int?)null : nk;
                thisObject.shirina = sh;
                thisObject.dolgota = dol;
                thisObject.area = ar;
                thisObject.rooms = (ko == 0) ? (int?)null : ko;
                thisObject.floor = (fl == 0) ? (int?)null : fl;
                a = thisObject.id; // Используем существующий ID

                nedvizhEntities1.GetContext().Entry(thisObject).State = EntityState.Modified;
            }
            else
            {
                // Добавление нового объекта
                a = nedvizhEntities1.GetContext().Object_Nedv
                    .OrderByDescending(x => x.id)
                    .FirstOrDefault().id + 1;
            }

            if (type.SelectedIndex != -1 && ci != "" && ci != null && adres.SelectedIndex != -1
                && sh != "" && sh != null && dol != "" && dol != null && ar != 0)
            {
                switch (type.SelectedIndex)
                {
                    case 0:
                        Object_Nedv n = new Object_Nedv()
                        {
                            id = a,
                            id_type = ty,
                            city = ci,
                            id_address = ad,
                            num_house = (nh == 0) ? (int?)null : nh,
                            num_kv = null,
                            shirina = sh,
                            dolgota = dol,
                            area = ar,
                            rooms = null,
                            floor = (fl == 0) ? (int?)null : fl
                        };
                        nedvizhEntities1.GetContext().Object_Nedv.Add(n);
                        break;
                    case 1:
                        Object_Nedv m = new Object_Nedv()
                        {
                            id = a,
                            id_type = ty,
                            city = ci,
                            id_address = ad,
                            num_house = null,
                            num_kv = null,
                            shirina = sh,
                            dolgota = dol,
                            area = ar,
                            rooms = null,
                            floor = null
                        };
                        nedvizhEntities1.GetContext().Object_Nedv.Add(m);
                        break;
                    case 2:
                        Object_Nedv k = new Object_Nedv()
                        {
                            id = a,
                            id_type = ty,
                            city = ci,
                            id_address = ad,
                            num_house = (nh == 0) ? (int?)null : nh,
                            num_kv = (nk == 0) ? (int?)null : nk,
                            shirina = sh,
                            dolgota = dol,
                            area = ar,
                            rooms = (ko == 0) ? (int?)null : ko,
                            floor = (fl == 0) ? (int?)null : fl
                        };
                        nedvizhEntities1.GetContext().Object_Nedv.Add(k);
                        break;
                }

                nedvizhEntities1.GetContext().SaveChanges();
                MessageBox.Show("Объект успешно обработан");

                // Переход на другую страницу после обработки объекта
                Class.Inf.ContentFrame.Navigate(new Pages.Object.ObjectPage());
            }
            else
            {
                MessageBox.Show("Заполните поля в правильном формате.\nОбязательные поля помечены звездочкой (*)");
            }
        }   
        private void type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedIndex == 2)
            {
                nh.Visibility = Visibility.Visible;
                nk.Visibility = Visibility.Visible;
                kk.Visibility = Visibility.Visible;
                fl.Visibility = Visibility.Visible;
               
            }
            else if (comboBox.SelectedIndex == 0)
            {
                nh.Visibility = Visibility.Visible;
                nk.Visibility = Visibility.Collapsed;
                kk.Visibility = Visibility.Collapsed;
                fl.Visibility = Visibility.Visible;
                
            }
            else if (comboBox.SelectedIndex == 1)
            {
                nh.Visibility = Visibility.Collapsed;
                nk.Visibility = Visibility.Collapsed;
                kk.Visibility = Visibility.Collapsed;
                fl.Visibility = Visibility.Collapsed;
               
            }
        }
    }
}
