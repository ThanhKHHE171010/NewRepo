using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Interaction logic for ForgotPasswordCustomerWindow.xaml
    /// </summary>
    public partial class ForgotPasswordCustomerWindow : Window
    {

        private readonly LoginObject _loginObject;
        private string _generatedOtp;
        public ForgotPasswordCustomerWindow()
        {
            InitializeComponent();
            _loginObject = new LoginObject();
        }

       

      

        private void btnSendOTP_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng điền tài khoản.");
                return;
            }

            var user = _loginObject.FindCusByUsername(username);
            if (user == null)
            {
                MessageBox.Show("Lỗi! Tài khoản không tồn tại.");
                return;
            }

            _generatedOtp = GenerateOtp();
            SendOtpEmail(username, _generatedOtp);
            MessageBox.Show("OTP đã được gửi tới email của bạn.");

            var enterOtpWindow = new EnterOTPCusWinndow(_generatedOtp, username);
            enterOtpWindow.Show();
            Close();
        }

        private string GenerateOtp()
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();
            return otp;
        }

        private void SendOtpEmail(string toEmail, string otp)
        {
            var fromAddress = new MailAddress("vudinhtu2904@gmail.com", "QuanLyKhoOto");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "cohz vkfx mgoi mtyl";
            const string subject = "Your OTP Code";
            string body = $"Your OTP code is: {otp}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }


    }
}
