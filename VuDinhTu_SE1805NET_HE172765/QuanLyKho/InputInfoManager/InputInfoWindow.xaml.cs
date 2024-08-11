using BusinessObject;
using ClosedXML.Excel;
using DataAccess.Models;
using DocumentFormat.OpenXml.Spreadsheet;
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

namespace QuanLyKho.InputInfoManager
{
    /// <summary>
    /// Interaction logic for InputInfoWindow.xaml
    /// </summary>
    public partial class InputInfoWindow : Window
    {
        private readonly InputInfoObject inputInfoObject;
        public InputInfoWindow()
        {
            InitializeComponent();
            inputInfoObject = new InputInfoObject();
            Loaded += WindowInput_Loaded;
        }

        private void WindowInput_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddInputInfoWindow addInputInfoWindow = new AddInputInfoWindow();
            addInputInfoWindow.Owner = this;
            addInputInfoWindow.ShowDialog();
            // Refresh DataGrid after adding a new invoice
            LoadData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();
            var filteredItems = inputInfoObject.GetAll()
                .Where(s => s.ObjectName.ToLower().Contains(keyword));
            dgInput.ItemsSource = filteredItems;
        }

        private void LoadData()
        {
            dgInput.ItemsSource = inputInfoObject.GetAll();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExportDataGridToExcel();
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
                        var worksheet = workbook.Worksheets.Add("Input Info");

                        // Add custom header row
                        worksheet.Cell(1, 1).Value = "Tất cả hóa đơn nhập kho";
                        worksheet.Range("A1:H1").Merge().Style.Font.SetBold().Font.FontSize = 16;

                        // Adding headers from DataGrid
                        for (int i = 0; i < dgInput.Columns.Count; i++)
                        {
                            worksheet.Cell(2, i + 1).Value = dgInput.Columns[i].Header.ToString();
                        }

                        // Adding the rows
                        var itemsSource = dgInput.ItemsSource as IEnumerable<dynamic>;
                        if (itemsSource != null)
                        {
                            int row = 3;
                            foreach (var item in itemsSource)
                            {
                                for (int col = 0; col < dgInput.Columns.Count; col++)
                                {
                                    var cellValue = item.GetType().GetProperty(dgInput.Columns[col].SortMemberPath)?.GetValue(item, null);
                                    worksheet.Cell(row, col + 1).Value = cellValue?.ToString() ?? string.Empty;
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
    }
}
