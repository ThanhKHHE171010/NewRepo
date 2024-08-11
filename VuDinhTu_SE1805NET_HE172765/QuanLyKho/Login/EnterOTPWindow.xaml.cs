using BusinessObject;
using System;
using System.Windows;

namespace QuanLyKho.Login
{
    public partial class EnterOTPWindow : Window
    {
        private readonly string _generatedOtp;
        private readonly string _username;
        private readonly LoginObject _loginObject;

        public EnterOTPWindow(string generatedOtp, string username)
        {
            InitializeComponent();
            _generatedOtp = generatedOtp;
            _username = username;
            _loginObject = new LoginObject();
        }

        private void btnVerifyOtp_Click(object sender, RoutedEventArgs e)
        {
            string enteredOtp = txtOtp.Text;
            string newPassword = txtNewPassword.Password;

            if (enteredOtp == _generatedOtp)
            {
                if (_loginObject.ResetPassword(_username, newPassword))
                {
                    MessageBox.Show("Thay đổi mật khẩu thành công!");
                    Close();
                }
                else
                {
                    MessageBox.Show("Lỗi! Tài khoản không tồn tại.");
                }
            }
            else
            {
                MessageBox.Show("Mã OTP không đúng. Vui lòng thử lại.");
            }
        }
    }
}
