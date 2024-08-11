using BusinessObject;
using ClosedXML.Excel;
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
    /// Interaction logic for OutputInfoWindow.xaml
    /// </summary>
    public partial class OutputInfoWindow : Window
    {
        private readonly OutputInfoObject outputInfoObject;
        private readonly LoginObject loginObject;
        private readonly CustomerObject customerObject;
        private readonly ObjectObject objectObject;
        public OutputInfoWindow()
        {
            InitializeComponent();
            outputInfoObject = new OutputInfoObject();
            loginObject = new LoginObject();
            customerObject = new CustomerObject();
            objectObject = new ObjectObject();
            LoadOutputInfos();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOutputInfoWindow addOutputInfoWindow = new AddOutputInfoWindow();
            addOutputInfoWindow.Owner = this;
            addOutputInfoWindow.ShowDialog();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExportDataGridToExcel();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();   
            mainWindow.Show();
            Close();
        }

       
        public void LoadOutputInfos()
        {
            try
            {
                var outputList = outputInfoObject.GetAllOutputsProcess();
                dgInput.ItemsSource = outputList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void ExportDataGridToExcel()
        {
            try
            {
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Output Info");

                        // Add custom header row
                        worksheet.Cell(1, 1).Value = "Tất cả hóa đơn xuất kho";
                        worksheet.Range("A1:H1").Merge().Style.Font.SetBold().Font.FontSize = 16; // Example: Merge cells A1 to H1, set bold font and font size

                        // Adding headers from DataGrid
                        for (int i = 0; i < dgInput.Columns.Count; i++)
                        {
                            worksheet.Cell(2, i + 1).Value = dgInput.Columns[i].Header.ToString();
                        }

                        // Adding the rows
                        var itemsSource = dgInput.ItemsSource as IEnumerable<dynamic>;
                        if (itemsSource != null)
                        {
                            int row = 3; // Start after the header row
                            foreach (var item in itemsSource)
                            {
                                for (int col = 0; col < dgInput.Columns.Count; col++)
                                {
                                    var cellValue = item.GetType().GetProperty(dgInput.Columns[col].SortMemberPath)?.GetValue(item, null);
                                    worksheet.Cell(row, col + 1).Value = cellValue ?? string.Empty;
                                }
                                row++;
                            }
                        }

                        workbook.SaveAs(saveFileDialog.FileName);
                    }
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi xuất file Excel: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private int outId;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgInput.SelectedItem is HoaDonXuat outputInfoViewModel)
                {
                    outId = outputInfoViewModel.IdOutputInfo;
                    var outputInfo1 = outputInfoObject.GetById(outId);

                    if (outputInfo1 != null)
                    {
                        outputInfo1.Status = "accept";
                        outputInfo1.IdUser = loginObject.GetCurrentUserId();


                        outputInfoObject.UpdateOutInfo(outputInfo1);

                        Customer customer = customerObject.GetCusById(outputInfo1.IdCustomer);
                        DataAccess.Models.Object obj = objectObject.GetObjById(outputInfo1.IdObject);

                        BillHistory bill = new BillHistory()
                        {
                            IdOutputInfo = outputInfo1.Id,
                            IdCustomer = customer.Id,
                            NameCustomer = customer.DisplayName,
                            Email = customer.Email,
                            Phone = customer.Phone,
                            ObjectName = obj.DisplayName,
                            Color = outputInfo1.Color,
                            Quantity = (int)outputInfo1.Count
                           
                        };

                        outputInfoObject.AddBill(bill);
                        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadOutputInfos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
      
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgInput.SelectedItem is HoaDonXuat outputInfoViewModel)
                {
                    outId = outputInfoViewModel.IdOutputInfo;
                    var outputInfo1 = outputInfoObject.GetById(outId);

                    if (outputInfo1 != null)
                    {
                        outputInfo1.Status = "cancel";
                        outputInfo1.IdUser = loginObject.GetCurrentUserId();
                        outputInfoObject.UpdateOutInfo(outputInfo1);
                        MessageBox.Show("Xác nhận hủy!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadOutputInfos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
