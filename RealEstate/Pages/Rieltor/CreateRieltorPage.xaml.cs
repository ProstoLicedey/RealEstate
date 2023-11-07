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

namespace RealEstate.Pages.Rieltor
{
    /// <summary>
    /// Логика взаимодействия для CreateRieltorPage.xaml
    /// </summary>
    public partial class CreateRieltorPage : Page
    {
        public RealEstate.Rieltor thisRieltor;
        public CreateRieltorPage(RealEstate.Rieltor rieltor)
        {
            InitializeComponent();

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.HeaderText = "Создание риэлтора";

            if (rieltor == null)
            {
                mainWindow.HeaderText = "Создание риэлтора";
            }
            else
            {
                mainWindow.HeaderText = "Редактирование риэлтора";
                thisRieltor = rieltor;

                lastname.Text = rieltor.lastname;
                firstname.Text = rieltor.firstname;
                middlename.Text = rieltor.middlename;
                NumberTextBox.Text = rieltor.dolya.ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new nedvizhEntities1())
            {

                try
                {
                    if (thisRieltor != null)
                    {
                        var rieltor = nedvizhEntities1.GetContext().Rieltor.Where(x => x.id == thisRieltor.id).FirstOrDefault();
                        rieltor.firstname = firstname.Text;
                        rieltor.middlename = middlename.Text;
                        rieltor.lastname = lastname.Text;
                        rieltor.dolya = float.Parse(NumberTextBox.Text);
               
                    }
                    else
                    {
                        var rieltor = new RealEstate.Rieltor()
                        {
                            lastname = lastname.Text,
                            firstname = firstname.Text,
                            middlename = middlename.Text,
                            dolya = float.Parse(NumberTextBox.Text),


                        };
                        db.Rieltor.Add(rieltor);
                    }
                    db.SaveChanges();
                    Class.Inf.ContentFrame.Navigate(new Pages.Rieltor.RieltorPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                                    "Внимание!",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
        }
        private void PhoneNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c)) // Allow digits and the dot character
                {
                    e.Handled = true; // Cancel input if the character is not a digit or a dot
                    break;
                }
            }
        }
    }
}
