using BusinessObject;
using DataAccess.Models;
using System;
using System.Windows;

namespace QuanLyKho.Login
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly LoginObject _loginObject;

        public RegisterWindow()
        {
            InitializeComponent();
            _loginObject = new LoginObject();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string displayName = txtDisplayName.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (!string.IsNullOrEmpty(displayName) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    if (_loginObject.Register(username, password, displayName, "Active"))
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
