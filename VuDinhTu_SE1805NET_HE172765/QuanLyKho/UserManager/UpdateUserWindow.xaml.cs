using BusinessObject;
using DataAccess.Models;
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

namespace QuanLyKho.UserManager
{
    /// <summary>
    /// Interaction logic for UpdateUserWindow.xaml
    /// </summary>
    public partial class UpdateUserWindow : Window
    {
        private int userId;

        private readonly UserObject userObject;
        public UpdateUserWindow(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            userObject = new UserObject();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserDetails();
        }
        private void LoadUserDetails()
        {
            var user = userObject.GetUserById(userId);
            if (user != null)
            {
                txtDisplayName.Text = user.DisplayName;
                txtUserName.Text = user.UserName;
            
                if (user.Status == "Active")
                {
                    rbActive.IsChecked = true;
                }
                else
                {
                    rbInactive.IsChecked = true;
                }
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var user = userObject.GetUserById(userId);
            if (user != null)
            {
               user.DisplayName = txtDisplayName.Text;
                user.UserName = txtUserName.Text;
              
                user.Status = rbActive.IsChecked == true ? "Active" : "Inactive";
                var result = userObject.UpdateUser(user);
                if (result)
                {
                    MessageBox.Show("Cập nhật thành công.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.");
                }

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
