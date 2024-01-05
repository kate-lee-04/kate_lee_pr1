using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Xml;
using System.Xml.Linq;

namespace WpfApp1
{

    public partial class Login : Window
    {
        XDocument doc;
        public Login()
        {
            InitializeComponent();
            doc = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\auth.xml");

        }
       
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            XElement root = doc.Element("authentications");
            
            foreach(XElement ho in root.Elements("auth"))
            {
                if (ho.Element("log").Value == textBoxLogin.Text && ho.Element("pass").Value == passBox.Password && ho.Element("log").Value != "admin" && ho.Element("pass").Value != "admin01")
                {
                    Hide();
                    MessageBox.Show("Выполняется вход");
                    MainForm mainform = new MainForm();
                    mainform.ShowDialog();
                    break;
                }
                else if (ho.Element("log").Value == "admin" && ho.Element("pass").Value == "admin01")
                {
                    Hide();
                    MessageBox.Show("Выполняется вход");
                    AdminWindow adminwindow = new AdminWindow();
                    adminwindow.ShowDialog();
                    break;
                }
            }
           
        }


        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Registration registration = new Registration();
            registration.ShowDialog();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }
    }
}
