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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Jewelry : Window
    {
        public ObservableCollection<object> jewellry;
        XDocument doc;
        XDocument doc1;

        public Jewelry()
        {
            InitializeComponent();
            UPD();
            doc = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\jewelry.xml");
            var jewelry = (from x in doc.Element("jewelry").Elements("product")
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

            jewellry = new ObservableCollection<object>(jewelry);

            dg.ItemsSource = jewellry;
        }

        public void UPD()
        {
            doc = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\jewelry.xml");
            XElement a = doc.Element("jewelry");
            doc1 = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\materials.xml");
            XElement b = doc1.Element("materials");
            foreach (XElement x in a.Elements("product"))
            {
                foreach (XElement y in b.Elements("material"))
                {
                    if (y.Element("code").Value == x.Element("material").Value)
                    {
                        int per_gram = int.Parse(y.Element("per_gram").Value);
                        int weight = int.Parse(x.Element("weight").Value);
                        int price = weight*per_gram;
                        x.Element("price").Value = price.ToString();
                        doc.Save("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\jewelry.xml");
                    }
                }
            }
            
            doc = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\jewelry.xml");
            var jewelry = (from x in doc.Element("jewelry").Elements("product")
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

            jewellry = new ObservableCollection<object>(jewelry);

            dg.ItemsSource = jewellry;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            doc.Element("jewelry").Add(new XElement("product",
                              new XElement("code", textid.Text),
                              new XElement("name", textname.Text),
                              new XElement("type", texttype.Text),
                              new XElement("material", textidmater.Text),
                              new XElement("weight", textweight.Text),
                              new XElement("price", textprice.Text)));
            doc.Save("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\jewelry.xml");
            MessageBox.Show("Изделие добавлено");
            jewellry.Add(new Products { Код = textid.Text, Название = textname.Text, Тип_изделия = texttype.Text, Материал = textidmater.Text, Вес__г = textweight.Text, Цена__р = textprice.Text});
            UPD();
        }

        private void DelProduct_Click(object sender, RoutedEventArgs e)
        {
            doc = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\jewelry.xml");
            XElement root = doc.Element("jewelry");
            foreach (XElement x in root.Elements("product"))
            {
                if ((x.Element("code").Value == textidfordel.Text))
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтвердить",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        x.Remove();
                        doc.Save("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\jewelry.xml");

                        UPD();
                        MessageBox.Show("Данные удалены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {

                    }
                }
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

public class Products
{
    public string Код { get; set; }
    public string Название { get; set; }
    public string Тип_изделия { get; set; }
    public string Материал { get; set; }
    public string Вес__г { get; set; }
    public string Цена__р { get; set; }
}
