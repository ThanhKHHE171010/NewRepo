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

namespace QuanLyKho.ObjectManager
{
    /// <summary>
    /// Interaction logic for ObjectDetailWindow.xaml
    /// </summary>
    public partial class ObjectDetailWindow : Window
    {
        private readonly ObjectObject objectObject;
        public ObjectDetailWindow()
        {

            InitializeComponent();
            objectObject = new ObjectObject();
            Load();
        }
        void Load()
        {
            dgDisplay.ItemsSource = objectObject.GetAllObjDetail();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                try
                {
                    dgDisplay.ItemsSource = objectObject.SearchByNameDetailObj(searchText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm vật tư: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ObjectWindow objectWindow = new ObjectWindow(); 
            objectWindow.Show();

            Close();
        }
    }
}
