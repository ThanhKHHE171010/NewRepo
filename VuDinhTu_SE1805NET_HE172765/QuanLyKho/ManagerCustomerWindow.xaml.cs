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
using System.Windows.Shapes;

namespace QuanLyKho
{
    /// <summary>
    /// Interaction logic for ManagerCustomerWindow.xaml
    /// </summary>
    public partial class ManagerCustomerWindow : Window
    {
        private readonly CustomerObject customerObject;
        private readonly BillHistoryObject billHistoryObject;
        public ManagerCustomerWindow()
        {
            InitializeComponent();
            billHistoryObject = new BillHistoryObject();
            customerObject = new CustomerObject();
        }
        
    }
}
