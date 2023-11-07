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
    /// Логика взаимодействия для CreateClientPage.xaml
    /// </summary>
    public partial class CreateClientPage : Page
    {
        public RealEstate.Client thisClient;
        public CreateClientPage(RealEstate.Client client)
        {
            InitializeComponent();
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (client == null)
            {
                mainWindow.HeaderText = "Создание клиента";
            }
            else
            {
                mainWindow.HeaderText = "Редактирование клиента";
                thisClient = client;

                lastname.Text = client.lastname;
                firstname.Text = client.firstname;
                middlename.Text = client.middlename;
                phoneNumberTextBox.Text = client.tel;
                email.Text = client.email;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new nedvizhEntities1())
            {
                try
                {
                    if (thisClient != null)
                    {
                        var client = nedvizhEntities1.GetContext().Client.Where(x => x.id == thisClient.id).FirstOrDefault();
                        client.firstname = firstname.Text;
                        client.middlename = middlename.Text;
                        client.lastname = lastname.Text;
                        client.tel = phoneNumberTextBox.Text;
                        client.email = email.Text;
                    }
                    else
                    {
                        var client = new RealEstate.Client()
                        {
                            lastname = lastname.Text,
                            firstname = firstname.Text,
                            middlename = middlename.Text,
                            tel = phoneNumberTextBox.Text,
                            email = email.Text

                        };
                        db.Client.Add(client);
                        
                    }

                    db.SaveChanges();
                    Class.Inf.ContentFrame.Navigate(new Pages.Client.ClientPage());
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
                if (!char.IsDigit(c))
                {
                    e.Handled = true; // отмена ввода, если символ не является цифрой
                    break;
                }
            }
        }
    }
}
