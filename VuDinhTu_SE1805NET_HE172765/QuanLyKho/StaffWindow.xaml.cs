using BusinessObject.Utils;
using BusinessObject;
using QuanLyKho.CustomerManager;
using QuanLyKho.InputInfoManager;
using QuanLyKho.Login;
using QuanLyKho.ObjectManager;
using QuanLyKho.OutputInfoManager;
using QuanLyKho.SuplierManager;
using QuanLyKho.UserManager;
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
using DataAccess.Models;
using QuanLyKho.BillHistoryManager;

namespace QuanLyKho
{
    /// <summary>
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        private User currentUser;
        private readonly InputInfoObject _inputInfoObject;
        private readonly OutputInfoObject _outputInfoObject;
        private readonly ObjectObject objectObject;
        public StaffWindow()
        {

            InitializeComponent();
            _inputInfoObject = new InputInfoObject();
            _outputInfoObject = new OutputInfoObject();
            objectObject = new ObjectObject();
            Loaded += MainWindow_Loaded;
        }



        
       
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadObject();
            LoadStatistics();
            cbObject.SelectionChanged += CbObject_SelectionChanged;
            LoadOutputInfos();
        }

        private void CbObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadStatistics();
        }

        
        private void LoadStatistics()
        {
            if (cbObject.SelectedValue != null)
            {
                int id = (int)cbObject.SelectedValue;
                txtIncomingInvoices.Text = objectObject.GetTotalInputInfoCount(id).ToString();
                txtOutgoingInvoices.Text = objectObject.GetTotalOutputInfoCount(id).ToString();
                txtTonKho.Text = objectObject.GetTotalInventory(id).ToString();
            }
            else
            {
                txtIncomingInvoices.Text = "0";
                txtOutgoingInvoices.Text = "0";
                txtTonKho.Text = "0";
            }
        }
        void LoadObject()
        {
            var obj = objectObject.GetAllObject();
            cbObject.ItemsSource = obj;
            cbObject.DisplayMemberPath = "DisplayName";
            //cbObject.DisplayMemberPath = "Color";
            cbObject.SelectedValuePath = "Id";
            cbObject.SelectedIndex = -1;
        }
        public void LoadOutputInfos()
        {
            try
            {
                var outputList = _outputInfoObject.GetAllOutputs();
                dgInput.ItemsSource = outputList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void btnNhap_Click(object sender, RoutedEventArgs e)
        {
            InputInfoWindow inputInforWindow = new InputInfoWindow();
            inputInforWindow.Show();
            Close();
        }

        private void btnXuat_Click(object sender, RoutedEventArgs e)
        {
            OutputInfoWindow outputInfoWindow = new OutputInfoWindow();
            outputInfoWindow.Show();
            Close();
        }

        private void btnDonVi_Click(object sender, RoutedEventArgs e)
        {
            SuplierWindow suplierWindow = new SuplierWindow();
            suplierWindow.Show();
            Close();
        }

        private void btnHangHoa_Click(object sender, RoutedEventArgs e)
        {
            ObjectWindow objectWindow = new ObjectWindow();
            objectWindow.Show();
            Close();
        }

        private void btnKhachHang_Click(object sender, RoutedEventArgs e)
        {
            ManagerCustomerWindow managerCustomerWindow = new ManagerCustomerWindow();
            managerCustomerWindow.Show();
            Close();
        }

       


        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {

            SessionManager.DestroySession();
            MainLogin mainLogin = new MainLogin();
            mainLogin.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadObject();

            LoadStatistics();
        }

        private void btnBill_Click(object sender, RoutedEventArgs e)
        {
            BillHistoryWindow billHistoryWindow = new BillHistoryWindow();
            billHistoryWindow.Show();
            Close();
        }



    }
}
