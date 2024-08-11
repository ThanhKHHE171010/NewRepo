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
    /// Interaction logic for AddObjectWindow.xaml
    /// </summary>
    public partial class AddObjectWindow : Window
    {
        private readonly ObjectObject objectObject;
        public AddObjectWindow()
        {
            InitializeComponent();
            objectObject = new ObjectObject();
            LoadSupliers();
        }

        private void LoadSupliers()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                cbSupplierName.ItemsSource = context.Supliers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhà cung cấp: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtDisplayName.Text.Trim();
                int? idSuplier = (int?)cbSupplierName.SelectedValue;
                string color = txtColor.Text.Trim();

                if (string.IsNullOrEmpty(name) || idSuplier == null || string.IsNullOrWhiteSpace(color))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin để thêm vật tư.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var existingObject = objectObject.GetAllObject().FirstOrDefault(o => o.DisplayName.ToLower() == name.ToLower() && o.IdSuplier == idSuplier);
                
                if (existingObject != null)
                {
                    var colorExist = existingObject.ObjectDetails.FirstOrDefault(c => c.Color.ToLower() == color.ToLower());
                    if (colorExist != null)
                    {
                        MessageBox.Show("Màu của xe này đã tồn tại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    ObjectDetail newObjectDetail = new ObjectDetail
                    {
                        IdObject = existingObject.Id,
                        Color   = color,
                    };

                    existingObject.ObjectDetails.Add(newObjectDetail);
                    objectObject.UpdateObject(existingObject);
                }
                else
                {
                    // Tạo vật tư mới nếu vật tư chưa tồn tại
                    DataAccess.Models.Object newObject = new DataAccess.Models.Object
                    {
                        DisplayName = name,
                        IdSuplier = idSuplier.Value,
                        Status = "1",
                        ObjectDetails = new List<ObjectDetail>
                {
                    new ObjectDetail
                    {
                        Color = color,
                    }
                },
                    };

                    objectObject.AddObject(newObject);
                }

                MessageBox.Show("Thêm vật tư thành công!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();

                if (Owner is ObjectWindow parentWindow)
                {
                    parentWindow.load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm vật tư: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
