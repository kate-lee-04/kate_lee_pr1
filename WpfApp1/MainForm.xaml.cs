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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{
    public partial class MainForm : Window
    {
        public ObservableCollection<object> jewellry;
        public ObservableCollection<object> orders;
        XDocument doc1;
        XDocument doc2;
        public MainForm()
        {
            InitializeComponent();
            doc2 = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\order.xml");

            doc1 = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\jewelry.xml");
            var jewelry = (from x in doc1.Element("jewelry").Elements("product")
                          orderby x.Element("code").Value
                          select new
                          {
                              Код = x.Element("code").Value,                              
                              Название = x.Element("name").Value,
                              Тип_изделия = x.Element("type").Value,
                              Материал = x.Element("material").Value,                             
                              Вес__г = x.Element("weight").Value,
                              Цена__р = x.Element("price").Value
                          }).ToList();

            jewellry = new ObservableCollection<object> (jewelry);

            dg.ItemsSource = jewellry;
        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /*private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textves_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textprice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }*/

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            doc2.Element("orders").Add(new XElement("order",
                              new XElement("id", textid.Text),
                              new XElement("last_name", textln.Text),
                              new XElement("name", textname.Text),
                              new XElement("patronymic", textpatronymic.Text),
                              new XElement("status", "не выполнен")));
            doc2.Save("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\order.xml");
            MessageBox.Show("Заказ добавлен");

            XElement root = doc2.Element("orders");
            int count = 0;
            foreach (XElement ho in root.Elements("order"))
            {
                if (ho.Element("last_name").Value == textln.Text && ho.Element("name").Value == textname.Text && ho.Element("patronymic").Value == textpatronymic.Text)
                {
                    count++;
                    if (count == 5)
                    {
                        MessageBox.Show("Дарим Вам скидку 20%");
                        break;
                    }
                }
            }

            var ord = (from x in doc2.Element("orders").Elements("order")
                           select new
                           {
                               Code = x.Element("id").Value,
                               Last_name = x.Element("last_name").Value,
                               Name = x.Element("name").Value,
                               Patronymic = x.Element("patronymic").Value,
                               Status = x.Element("status").Value
                           }).ToList();
            orders = new ObservableCollection<object>(ord);

            //orders.Add(new Order { Code = textid.Text, Last_name = textln.Text, Name = textname.Text, Patronymic = textpatronymic.Text, Status = "не выполнен" });
        }

        private void ButtonExit_Click (object sender, RoutedEventArgs e)
        {
            Hide();
            Login login = new Login();
            login.ShowDialog();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }
    }

    public class Order
    {
        public string Code { get; set; }
        public string Last_name { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Status { get; set; }
    }

    
}

