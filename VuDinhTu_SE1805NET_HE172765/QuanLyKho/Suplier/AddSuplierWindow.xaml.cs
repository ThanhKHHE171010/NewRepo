using BusinessObject;
using DataAccess.Models;
using System.Windows;

namespace QuanLyKho.SuplierManager
{
    public partial class AddSuplierWindow : Window
    {
        private SuplierObject _suplierObject;

        public AddSuplierWindow()
        {
            InitializeComponent();
            _suplierObject = new SuplierObject();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var displayName = txtDisplayName.Text;

            // Check if the supplier with the same name already exists
            if (_suplierObject.SuplierExistsByName(displayName))
            {
                MessageBox.Show("Nhà cung cấp với tên này đã tồn tại.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newSuplier = new Suplier
            {
                DisplayName = displayName,
                Address = txtAddress.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Status =  "Active"
            };

            bool result = _suplierObject.AddSuplier(newSuplier);
            if (result)
            {
                MessageBox.Show("Thêm nhà cung cấp thành công!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm nhà cung cấp thất bại. Thử lại.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
