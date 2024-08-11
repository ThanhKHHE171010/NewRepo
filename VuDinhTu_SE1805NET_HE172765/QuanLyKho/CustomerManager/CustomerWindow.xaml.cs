using BusinessObject;
using BusinessObject.Utils;
using DataAccess.Models;
using QuanLyKho.Login;
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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly CustomerObject customerObject;
  
        public CustomerWindow()
        {
            InitializeComponent();
            customerObject = new CustomerObject();
        }

        
        
        private void btnBill_Click(object sender, RoutedEventArgs e)
        {
            BillHistoryCustomerWindow billHistoryCustomerWindow = new BillHistoryCustomerWindow();
            billHistoryCustomerWindow.ShowDialog();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
           
            Customer customer = SessionManager.GetSessionCus();
            if (customer != null)
            {
                UpdateCustomerWindow updateCustomerWindow = new UpdateCustomerWindow(customer);
                updateCustomerWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không thể lấy thông tin khách hàng.");
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainLogin mainLogin = new MainLogin();
            mainLogin.Show();
            Close();
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            BuyObjectWindow buyObjectWindow = new BuyObjectWindow();
            buyObjectWindow.ShowDialog();

        }
    }
}
