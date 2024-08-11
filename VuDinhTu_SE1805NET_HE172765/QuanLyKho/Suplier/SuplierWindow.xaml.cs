using BusinessObject;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QuanLyKho.SuplierManager
{
    public partial class SuplierWindow : Window
    {
      
        private readonly SuplierObject _suplierObject;
    
        public SuplierWindow()
        {
            InitializeComponent();
            _suplierObject = new SuplierObject();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgSupliers.ItemsSource = _suplierObject.GetAllSupliers();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddSuplierWindow addSuplierWindow = new AddSuplierWindow();
            addSuplierWindow.ShowDialog();

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
            var filteredSupliers = _suplierObject.GetAllSupliers()
                .Where(s => s.DisplayName.ToLower().Contains(keyword) || s.Email.ToLower().Contains(keyword))
                .ToList();
            dgSupliers.ItemsSource = filteredSupliers;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Suplier suplier = btn.DataContext as Suplier;
                if (suplier != null)
                {
                    int suplierId = suplier.Id;
                    UpdateSuplierWindow updateSuplierWindow = new UpdateSuplierWindow(suplierId);
                    updateSuplierWindow.ShowDialog();
                    LoadData(); // Reload data to reflect updates
                }
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var suplier = button.DataContext as Suplier;
                if(suplier != null)
                {
                    var result = MessageBox.Show($"Bạn có muốn xóa {suplier.DisplayName} không ?","Confirm Delete",MessageBoxButton.YesNo,MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _suplierObject.DeleteSuplier(suplier.Id);
                    }
                }

            }
        }
    }
}
