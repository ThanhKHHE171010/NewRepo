using BusinessObject;
using DataAccess.Models;
using System.Windows;

namespace QuanLyKho.CustomerManager
{
    public partial class UpdateCustomerWindow : Window
    {
        private readonly CustomerObject customerObject;
        private Customer customer;

        public UpdateCustomerWindow(Customer customer)
        {
            InitializeComponent();
            customerObject = new CustomerObject();
            this.customer = customer;
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            if (customer != null)
            {
                txtDisplayName.Text = customer.DisplayName;
                txtAddress.Text = customer.Address;
                txtPhone.Text = customer.Phone;
                txtEmail.Text = customer.Email;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            customer.DisplayName = txtDisplayName.Text;
            customer.Address = txtAddress.Text;
            customer.Phone = txtPhone.Text;
            

            var result = customerObject.UpdateCustomer(customer);
            if (result)
            {
                MessageBox.Show("Cập nhật khách hàng thành công.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi chưa thể cập nhật.");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
