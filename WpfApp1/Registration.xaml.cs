using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using System.Xml;
using System.Xml.Linq;

namespace WpfApp1
{
    public partial class Registration : Window
    {
        XDocument doc;
        public ObservableCollection<object> auth;

        public Registration()
        {
            InitializeComponent();
            doc = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\auth.xml");
            

        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass_1 = passBox_1.Password.Trim();
            string pass_2 = passBox_2.Password.Trim();

            if (pass_1.Length < 5)
            {
                MessageBox.Show("Пароль должен содержать больше 5 символов");
            }
            
            else if (pass_2 != pass_1)
            {
                MessageBox.Show("Пароли не совпадают");
            }

            else
            {
                doc.Element("authentications").Add(new XElement("auth",
                              new XElement("log", textBoxLogin.Text),
                              new XElement("pass", passBox_2.Password)));
                doc.Save("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\auth.xml");
                MessageBox.Show("Новые данные добавлены");

                Hide();
                MainForm mainform = new MainForm();
                mainform.ShowDialog(); //переход на след страницу
            }
            
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }
    }

    
}
