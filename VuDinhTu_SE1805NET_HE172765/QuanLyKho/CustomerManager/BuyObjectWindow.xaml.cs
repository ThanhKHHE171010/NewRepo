using BusinessObject;
using DataAccess.Models;
using System;
using System.Linq;
using System.Windows;

namespace QuanLyKho.CustomerManager
{
    public partial class BuyObjectWindow : Window
    {
        private readonly ObjectObject _objectObject;
        private readonly OutputInfoObject _outputInfoObject;
        private readonly LoginObject _loginObject;


        public BuyObjectWindow()
        {
            InitializeComponent();
            _objectObject = new ObjectObject();
            _outputInfoObject = new OutputInfoObject();
            _loginObject = new LoginObject();
            LoadObjects();
            
        }

       
        private void LoadObjects()
        {
            try
            {
                dgvObject.ItemsSource = _objectObject.GetAllObject();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void dgvObject_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgvObject.SelectedValue != null)
            {
                int selectedObjectId = (int)dgvObject.SelectedValue;
                LoadColors(selectedObjectId);
            }
        }

        private void LoadColors(int objectId)
        {
            try
            {
                dgvColor.ItemsSource = _objectObject.GetDetailsByObjectId(objectId);
                dgvColor.DisplayMemberPath = "Color";
                dgvColor.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu màu: " + ex.Message);
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            //if (dgvObject.SelectedValue != null && dgvColor.SelectedValue != null && int.TryParse(txtCount.Text, out int count))
            //{
            //    int objectId = (int)dgvObject.SelectedValue;
            //    int colorId = (int)dgvColor.SelectedValue;
            //    int customerId = _loginObject.GetCurrentCustomerId();
            //    // Check if the quantity is available
            //    if (_outputInfoObject.CanExport(objectId, count))
            //    {
            //        try
            //        {
            //            // Create and save a new Output record
            //            var newOutput = new Output { DateOutput = DateTime.Now };
            //            using (var context = new QuanLyKhoOtoContext())
            //            {
            //                context.Outputs.Add(newOutput);
            //                context.SaveChanges();
            //            }

            //            // Create and save a new OutputInfo record
            //            var newOutputInfo = new OutputInfo
            //            {
            //                IdObject = objectId,
            //                IdOutput = newOutput.Id,
            //                IdCustomer = customerId,
            //                Count = count,
            //                Status = "process",
            //                IdUser = null
            //            };

            //            _outputInfoObject.AddOutput(newOutputInfo);
            //            MessageBox.Show("Đặt xe thành công!");

            //            // Reset form
            //            ResetForm();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Lỗi khi đặt xe: " + ex.Message);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Số lượng xe không đủ.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn xe, màu xe và nhập số lượng hợp lệ.");
            //}
            try
            {
                int colorId = (int)dgvColor.SelectedValue;
                if (dgvObject.SelectedValue == null ||
                    string.IsNullOrEmpty(txtCount.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                int customerId = _loginObject.GetCurrentCustomerId();
                var selectedColorItem = (ObjectDetail)dgvColor.SelectedItem;
                string color = selectedColorItem.Color;

                var output = new OutputInfo
                {
                    IdObject = (int)dgvObject.SelectedValue,
                    Count = int.Parse(txtCount.Text),
                    IdCustomer = customerId,
                    IdUser = null,
                    Color = color,
                    IdOutputNavigation = new Output
                    {
                        DateOutput = DateTime.Now
                    },
                };

                _outputInfoObject.AddOutput(output, color);
                MessageBox.Show("Đặt Xe Thành Công, Chờ nhân viên xét duyệt!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();


                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


     









        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ResetForm()
        {
            dgvObject.SelectedIndex = -1;
            dgvColor.SelectedIndex = -1;
            txtCount.Text = string.Empty;
        }
    }
}
