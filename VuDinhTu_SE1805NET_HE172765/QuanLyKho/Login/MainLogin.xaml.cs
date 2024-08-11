using BusinessObject;
using BusinessObject.Utils;
using DataAccess.Models;
using QuanLyKho.CustomerManager;
using System;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.Login
{
    /// <summary>
    /// Interaction logic for MainLogin.xaml
    /// </summary>
    public partial class MainLogin : Window
    {
        private readonly LoginObject loginObject;

        public MainLogin()
        {
            InitializeComponent();
            loginObject = new LoginObject();
        }

        // Register
        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        // Forgot password
        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ForgotPasswordWindow forgotPasswordWindow = new ForgotPasswordWindow();
            forgotPasswordWindow.ShowDialog();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.ToString();
            string password = txtPassword.Password.ToString();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                bool isAuthorized = loginObject.Login(username, password);
                bool isCus = loginObject.LoginCus(username, password);  


                if (isAuthorized)
                {
                    if(loginObject.userACC().IdRole == 1)
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        StaffWindow staffWindow = new StaffWindow();
                        staffWindow.Show();
                        this.Close();
                    }
                }
                else if (isCus)
                {
                    CustomerWindow customerWindow = new CustomerWindow();
                    customerWindow.Show();
                    this.Close();
                }


                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không tồn tại", "Login");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu");
            }
        }

        private void TextBlock_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            RegisterCustomerWindow customerWindow = new RegisterCustomerWindow();
            customerWindow.ShowDialog();
        }

        private void TextBlock_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            ForgotPasswordCustomerWindow f = new ForgotPasswordCustomerWindow();    

            f.ShowDialog();
        }
    }
}
