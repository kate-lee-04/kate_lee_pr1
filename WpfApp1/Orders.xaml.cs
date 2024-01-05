using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public ObservableCollection<object> orders;
        XDocument doc;
        public Orders()
        {
            InitializeComponent();
            doc = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\order.xml");
            var order = (from x in doc.Element("orders").Elements("order")
                         orderby x.Element("id").Value
                         select new
                         {
                             Код = x.Element("id").Value,
                             Фамилия = x.Element("last_name").Value,
                             Имя = x.Element("name").Value,
                             Отчество = x.Element("patronymic").Value,
                             Статус = x.Element("status").Value
                         }).ToList();

            orders = new ObservableCollection<object>(order);

            dg.ItemsSource = orders;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            XElement root = doc.Element("orders");

            foreach (XElement ho in root.Elements("order"))
            {
                if (ho.Element("id").Value == textid.Text && ho.Element("last_name").Value == textln.Text && ho.Element("name").Value == textname.Text && ho.Element("patronymic").Value == textpatronymic.Text)
                {
                    ho.Element("status").Value = "выполнено";
                    doc.Save("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\order.xml");
                    MessageBox.Show("Статус изменён");
                    break;
                }
                
            }

            var order = (from x in doc.Element("orders").Elements("order")
                         orderby x.Element("id").Value
                         select new
                         {
                             Код = x.Element("id").Value,
                             Фамилия = x.Element("last_name").Value,
                             Имя = x.Element("name").Value,
                             Отчество = x.Element("patronymic").Value,
                             Статус = x.Element("status").Value
                         }).ToList();

            orders = new ObservableCollection<object>(order);

            dg.ItemsSource = orders;
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.ShowDialog();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }
    }
}
