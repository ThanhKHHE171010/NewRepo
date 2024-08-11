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

namespace QuanLyKho.Login
{
    /// <summary>
    /// Interaction logic for RegisterCustomerWindow.xaml
    /// </summary>
    public partial class RegisterCustomerWindow : Window
    {
        private readonly LoginObject _loginObject;
        public RegisterCustomerWindow()
        {
            InitializeComponent();
  
            _loginObject = new LoginObject();
        }
      

   

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string displayName = txtDisplayName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;   
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            

            if (!string.IsNullOrEmpty(displayName) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(phone))
            {
                try
                {
                    if (_loginObject.RegisterCus(username, password, displayName,phone,address))
                    {
                        MessageBox.Show("Đăng Kí Thành Công!");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Đăng Kí Thất Bại.");
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
            }
        }
    }
}
