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
    /// Interaction logic for EnterOTPCusWinndow.xaml
    /// </summary>
    public partial class EnterOTPCusWinndow : Window
    {
        private readonly string _generatedOtp;
        private readonly string _username;
        private readonly LoginObject _loginObject;
        public EnterOTPCusWinndow(string generatedOtp, string username)
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
                if (_loginObject.ResetPasswordCus(_username, newPassword))
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
