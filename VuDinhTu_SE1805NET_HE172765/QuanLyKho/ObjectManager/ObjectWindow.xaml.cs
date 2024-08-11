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
    /// Interaction logic for ObjectWindow.xaml
    /// </summary>
    public partial class ObjectWindow : Window
    {
        private readonly ObjectObject objectObject;
        public ObjectWindow()
        {
            InitializeComponent();

            objectObject = new ObjectObject();
            Loaded += LoadObject;
        }
        public void load()
        {
            dgDisplay.ItemsSource = objectObject.GetAllObject();

        }
        private void LoadObject(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text;
            var findUser = objectObject.GetAllObject().
                Where(s => s.DisplayName.ToLower().Contains(keyword));
            dgDisplay.ItemsSource = findUser;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddObjectWindow addObjectWindow = new AddObjectWindow();
            addObjectWindow.Owner = this;
            addObjectWindow.ShowDialog();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            ObjectDetailWindow detailWindow = new ObjectDetailWindow(); 

            detailWindow.Show();
            Close();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (dgDisplay.SelectedItem is DataAccess.Models.Object obj)
            {
                try
                {
                    obj.Status = obj.Status == "1" ? "0" : "1";
                    objectObject.UpdateObj(obj);
                    MessageBox.Show("Cập nhật trạng thái thành công!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgDisplay.SelectedItem is DataAccess.Models.Object selectedObject)
            {
                try
                {
                    objectObject.DeleteObject(selectedObject);

                    MessageBox.Show("Xóa vật tư thành công!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa vật tư: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn vật tư cần xóa.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
