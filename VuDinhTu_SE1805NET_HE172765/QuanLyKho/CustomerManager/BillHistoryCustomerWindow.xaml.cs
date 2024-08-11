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

namespace QuanLyKho.CustomerManager
{
    /// <summary>
    /// Interaction logic for BillHistoryCustomerWindow.xaml
    /// </summary>
    public partial class BillHistoryCustomerWindow : Window
    {
        private readonly LoginObject loginObject;
        private readonly BillHistoryObject billHistoryObject;
        public BillHistoryCustomerWindow()
        {
            InitializeComponent();
            loginObject = new LoginObject();    
            billHistoryObject = new BillHistoryObject();
        }
        private void load()
        {
            int cusId = loginObject.GetCurrentCustomerId();
            dgBillHistory.ItemsSource = billHistoryObject.getByIdCus(cusId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
