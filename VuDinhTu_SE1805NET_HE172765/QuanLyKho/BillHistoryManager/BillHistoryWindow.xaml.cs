using BusinessObject;
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

namespace QuanLyKho.BillHistoryManager
{
    /// <summary>
    /// Interaction logic for BillHistoryWindow.xaml
    /// </summary>
    public partial class BillHistoryWindow : Window
    {
        private readonly BillHistoryObject _billHistoryObject;
        public BillHistoryWindow()
        {
            InitializeComponent();
            _billHistoryObject = new BillHistoryObject();
        }

       
        private void load()
        {
            dgBillHistory.ItemsSource = _billHistoryObject.getAll();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }
    }
}
