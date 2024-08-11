using BusinessObject;
using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace QuanLyKho.Login
{
    public partial class ForgotPasswordWindow : Window
    {
        private readonly LoginObject _loginObject;
        private string _generatedOtp;

        public ForgotPasswordWindow()
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

            var user = _loginObject.FindUserByUsername(username);
            if (user == null)
            {
                MessageBox.Show("Lỗi! Tài khoản không tồn tại.");
                return;
            }

            _generatedOtp = GenerateOtp();
            SendOtpEmail(username, _generatedOtp);
            MessageBox.Show("OTP đã được gửi tới email của bạn.");

            var enterOtpWindow = new EnterOTPWindow(_generatedOtp, username);
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
