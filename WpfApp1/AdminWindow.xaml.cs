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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void JewTable_Click (object sender, RoutedEventArgs e)
        {
            Hide();
            Jewelry jewelry= new Jewelry();
            jewelry.ShowDialog();
        }

        private void MaterTable_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Materials materials = new Materials();
            materials.ShowDialog();
        }

        private void OrdTable_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Orders orders = new Orders();
            orders.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
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
}
