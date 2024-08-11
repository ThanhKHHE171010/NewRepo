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

namespace QuanLyKho.OutputInfoManager
{
    /// <summary>
    /// Interaction logic for AddOutputInfoWindow.xaml
    /// </summary>
    public partial class AddOutputInfoWindow : Window
    {

        private readonly ObjectObject objectObject;
        private readonly CustomerObject customerObject;
        private readonly OutputInfoObject outputInfoObject;
        public AddOutputInfoWindow()
        {
            InitializeComponent();
            objectObject = new ObjectObject();  
            customerObject = new CustomerObject();  
            outputInfoObject = new OutputInfoObject();
            Loaded += LoadData;


        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            LoadCustomers();
            LoadObject();
        }

        void LoadObject()
        {
            var object1 = objectObject.GetAllObject();
            cbNameObject.ItemsSource = object1;
            cbNameObject.DisplayMemberPath = "DisplayName";
            cbNameObject.SelectedValuePath = "Id";
        }
        void LoadCustomers()
        {
            var customers = customerObject.GetAllCustomers();
            cbCus.ItemsSource = customers;
            cbCus.DisplayMemberPath = "DisplayName";
            cbCus.SelectedValuePath = "Id";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    var idCustomer = cbCus.SelectedValue.ToString();
            //    if (string.IsNullOrEmpty(txtInputId.Text) ||
            //        cbNameObject.SelectedValue == null ||
            //        string.IsNullOrEmpty(txtCount.Text) ||
            //        cbCus.SelectedValue == null)
            //    {
            //        MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }

            //    if (outputInfoObject.IsOutputIdExists(txtInputId.Text))
            //    {
            //        MessageBox.Show("ID Hóa Đơn Xuất đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }
            //    var output = new OutputInfo
            //    {
            //        IdOutputInfo = txtInputId.Text,
            //        IdObject = cbNameObject.SelectedValue.ToString(),
            //        Count = int.Parse(txtCount.Text),
            //        IdCustomer = int.Parse(idCustomer),
            //        IdUser = LoginObject.Account.Id,
            //        IdNavigation = new Output
            //        {
            //            Id = txtInputId.Text,
            //            DateOutput = DateTime.Now
            //        },
            //    };

            //    outputInfoObject.AddOutput(output);
            //    MessageBox.Show("Thêm Hóa Đơn Xuất thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //    this.Close();


            //    if (Owner is OutputInfoWindow parentWindow)
            //    {
            //        parentWindow.LoadOutputInfos();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
