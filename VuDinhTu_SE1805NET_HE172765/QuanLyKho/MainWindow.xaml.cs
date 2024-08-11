using QuanLyKho.CustomerManager;
using QuanLyKho.InputInfoManager;
using QuanLyKho.SuplierManager;
using BusinessObject.Utils;
using System.Windows;
using QuanLyKho.Login;
using System.Windows.Controls;
using DataAccess.Models;
using QuanLyKho.OutputInfoManager;
using QuanLyKho.ObjectManager;
using BusinessObject;
using QuanLyKho.UserManager;
using QuanLyKho.BillHistoryManager;

namespace QuanLyKho
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User currentUser;
        private readonly InputInfoObject _inputInfoObject;
        private readonly OutputInfoObject _outputInfoObject;
        private readonly ObjectObject objectObject;
        public MainWindow()
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

        //private void SetAccessControl()
        //{
        //    // Hide the "Nhân Viên" button for staff role (IdRole = 2)
        //    if (currentUser.IdRole == 2)
        //    {
        //        btnNhanVien.Visibility = Visibility.Collapsed;
        //    }
        //}
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

        private void btnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow();
            userWindow.Show();
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
