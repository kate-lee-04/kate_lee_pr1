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
using static WpfApp1.Jewelry;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Materials : Window
    {
        public ObservableCollection<object> materials;
        XDocument doc;
        public Materials()
        {
            InitializeComponent();
            doc = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\materials.xml");
            var mater = (from x in doc.Element("materials").Elements("material")
                           orderby x.Element("code").Value
                           select new
                           {
                               Код = x.Element("code").Value,
                               Название = x.Element("name").Value,
                               Цена_за_грамм = x.Element("per_gram").Value,
                           }).ToList();

            materials = new ObservableCollection<object>(mater);

            dg.ItemsSource = materials;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddMater_Click(object sender, RoutedEventArgs e)
        {
            doc.Element("materials").Add(new XElement("material",
                              new XElement("code", textid.Text),
                              new XElement("name", textname.Text),
                              new XElement("per_gram", textgram.Text)));
            doc.Save("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\materials.xml");
            MessageBox.Show("Материал добавлен");
            materials.Add(new Material { Code = textid.Text, Name = textname.Text, Price_g = textgram.Text });
            
            var mater = (from x in doc.Element("materials").Elements("material")
                         orderby x.Element("code").Value
                         select new
                         {
                             Код = x.Element("code").Value,
                             Название = x.Element("name").Value,
                             Цена_за_грамм = x.Element("per_gram").Value,
                         }).ToList();

            materials = new ObservableCollection<object>(mater);

            dg.ItemsSource = materials;
        }

        private void DelMater_Click(object sender, RoutedEventArgs e)
        {
            doc = XDocument.Load("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\materials.xml");
            XElement root = doc.Element("materials");
            foreach (XElement x in root.Elements("material"))
            {
                if ((x.Element("code").Value == textiddel.Text))
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены?", "Подтвердить",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        x.Remove();
                        doc.Save("C:\\Users\\Kat\\source\\repos\\WpfApp1\\WpfApp1\\Docs\\materials.xml");

                        var mater = (from y in doc.Element("materials").Elements("material")
                                     orderby y.Element("code").Value
                                     select new
                                     {
                                         Код = y.Element("code").Value,
                                         Название = y.Element("name").Value,
                                         Цена_за_грамм = y.Element("per_gram").Value,
                                     }).ToList();

                        materials = new ObservableCollection<object>(mater);

                        dg.ItemsSource = materials;

                        MessageBox.Show("Данные удалены", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {

                    }
                }
            }
        }

        public class Material
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Price_g { get; set; }
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
