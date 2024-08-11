using BusinessObject;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using QuanLyKho.CustomerManager;
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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly QuanLyKhoOtoContext _context;
        private readonly User user;
        private readonly UserObject userObject;
        public UserWindow()
        {
            InitializeComponent();
            userObject = new UserObject();  
            _context   = new QuanLyKhoOtoContext();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            //var s = _context.Users
            //    .Include(u => u.DisplayName)
            //    .Where
            //    (u => u)
            //    .ToList
            //    ().;


            dgUsers.ItemsSource = userObject.GetAllUsers();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
           




            Button button = sender as Button;
            if (button != null)
            {
                User user = button.DataContext as User;
                if (user != null)
                {
                    int userId = user.Id;
                    UpdateUserWindow updateUserWindow = new UpdateUserWindow(userId);
                    updateUserWindow.ShowDialog();
                    LoadData();
                }
            }


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text;
            var findUser = userObject.GetAllUsers().
                Where(s => s.DisplayName.ToLower().Contains(keyword));
            dgUsers.ItemsSource = findUser;
        }
    }
}
