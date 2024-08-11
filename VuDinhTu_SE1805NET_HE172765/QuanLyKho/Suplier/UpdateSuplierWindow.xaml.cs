using BusinessObject;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyKho.SuplierManager
{
    /// <summary>
    /// Interaction logic for UpdateSuplierWindow.xaml
    /// </summary>
    public partial class UpdateSuplierWindow : Window
    {
        private int _suplierId;
        private SuplierObject _suplierObject;

        public UpdateSuplierWindow(int suplierId)
        {
            InitializeComponent();
            _suplierId = suplierId;
            _suplierObject = new SuplierObject();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSuplierDetails();
        }

        private void LoadSuplierDetails()
        {
            var suplier = _suplierObject.GetSuplierById(_suplierId);
            if (suplier != null)
            {
                txtDisplayName.Text = suplier.DisplayName;
                txtAddress.Text = suplier.Address;
                txtPhone.Text = suplier.Phone;
                txtEmail.Text = suplier.Email;
                if (suplier.Status == "Active")
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
            var suplier = _suplierObject.GetSuplierById(_suplierId);
            if (suplier != null)
            {
                suplier.DisplayName = txtDisplayName.Text;
                suplier.Address = txtAddress.Text;
                suplier.Phone = txtPhone.Text;
                suplier.Email = txtEmail.Text;
                suplier.Status = rbActive.IsChecked == true ? "Active" : "Inactive";

                var result = _suplierObject.UpdateSuplier(suplier);
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
