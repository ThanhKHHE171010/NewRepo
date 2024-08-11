using BusinessObject;
using DataAccess.Models;
using System;
using System.Linq;
using System.Windows;

namespace QuanLyKho.InputInfoManager
{
    public partial class AddInputInfoWindow : Window
    {
        private readonly InputInfoObject inputInfoObject;
        private readonly LoginObject loginObject;
        private readonly ObjectObject objectObject;

        public AddInputInfoWindow()
        {
            InitializeComponent();
            inputInfoObject = new InputInfoObject();
            loginObject = new LoginObject();
            objectObject = new ObjectObject();
            LoadSupliers();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var name = txtNameObject.Text;
                var countText = int.Parse(txtCount.Text);
                
                var suplierId = (int?)cbSupplierName.SelectedValue;
                var color = txtColorObject.Text;

                if (string.IsNullOrWhiteSpace(name) || countText == 0 ||
                     suplierId == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                    ResetForm();
                    return;
                }

                var exitsObject = objectObject.GetAll().
                                  FirstOrDefault(o => o.DisplayName.ToLower() == name.ToLower() && o.IdSuplierNavigation.Id == suplierId);


                if (exitsObject != null)
                {

                    var existingDetail = exitsObject.ObjectDetails.FirstOrDefault(od => od.Color.ToLower() == color.ToLower());
                    if (existingDetail != null)
                    {
                        existingDetail.Count += countText;
                        objectObject.UpdateObject(exitsObject);
                        AddNewInput(exitsObject.Id, countText,  color);

                        Close();
                    }
                    else
                    {
                        ObjectDetail objectDetail = new ObjectDetail
                        {
                            IdObject = exitsObject.Id,
                            Color = color,
                            Count = countText,
                        };
                        exitsObject.ObjectDetails.Add(objectDetail);
                        objectObject.UpdateObject(exitsObject);

                        AddNewInput(exitsObject.Id, countText, color);

                        Close();
                    }
                }
                else
                {
                    DataAccess.Models.Object newObject = new DataAccess.Models.Object
                    {
                        DisplayName = name,
                        IdSuplier = suplierId.Value,
                        Status = "Active",
                        ObjectDetails = new List<ObjectDetail>
                        {
                            new ObjectDetail
                            {
                                Color = color,
                                Count = countText,
                            }
                        },
                    };

                    objectObject.AddObject(newObject);

                    AddNewInput(newObject.Id, countText, color);
                }

                MessageBox.Show("Thêm phiếu thành công!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phiếu nhập: " + ex.Message);
            }
        }

        private void AddNewInput(int objectId, int count,  string color)
        {
            int userId = loginObject.GetCurrentUserId();
            InputInfoObject inputInfoObject = new InputInfoObject();
            var inputInfo1 = new InputInfo
            {
                IdObject = objectId,
                Count = count,
                IdUser = userId,
                Color = color,
                IdInputNavigation = new Input
                {
                    DateInput = DateTime.Now
                },
            };

            inputInfoObject.AddInput(inputInfo1);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      

        private void ResetForm()
        {
            txtCount.Text = null;
            txtNameObject.Text = null;
            
            cbSupplierName.SelectedIndex = -1;
        }

        private void LoadSupliers()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                cbSupplierName.ItemsSource = context.Supliers.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhà cung cấp: " + ex.Message);
            }
        }

        private void AddNewColorAndInvoice(DataAccess.Models.Object existingObject, string carColor, int count, int supplierId, QuanLyKhoOtoContext context)
        {
            var newColorDetail = new ObjectDetail
            {
                IdObject = existingObject.Id,
                Color = carColor,
                Count = count
            };
            context.ObjectDetails.Add(newColorDetail);
            context.SaveChanges();

            AddNewInputInfo(existingObject.Id, count, context);
        }

        private void UpdateExistingInvoice(DataAccess.Models.Object existingObject, ObjectDetail existingColor, int count, int supplierId, QuanLyKhoOtoContext context)
        {
            existingColor.Count += count;
            context.ObjectDetails.Update(existingColor);
            context.SaveChanges();

            var existingInputInfo = context.InputInfos
                .FirstOrDefault(ii => ii.IdObject == existingObject.Id);

            if (existingInputInfo != null)
            {
                existingInputInfo.Count += count;
                context.InputInfos.Update(existingInputInfo);
            }
            else
            {
                AddNewInputInfo(existingObject.Id, count, context);
            }

            context.SaveChanges();
        }

        private void AddNewCarAndInvoice(string carName, string carColor, int count, int supplierId, QuanLyKhoOtoContext context)
        {
            var newObject = new DataAccess.Models.Object
            {
                DisplayName = carName,
                IdSuplier = supplierId,
                Status = "Active"
            };
            context.Objects.Add(newObject);
            context.SaveChanges();

            var newColorDetail = new ObjectDetail
            {
                IdObject = newObject.Id,
                Color = carColor,
                Count = count
            };
            context.ObjectDetails.Add(newColorDetail);
            context.SaveChanges();

            AddNewInputInfo(newObject.Id, count, context);
        }

        private void AddNewInputInfo(int objectId, int count, QuanLyKhoOtoContext context)
        {
            var newInput = new Input { DateInput = DateTime.Now };
            context.Inputs.Add(newInput);
            context.SaveChanges();

            var newInputInfo = new InputInfo
            {
                IdObject = objectId,
                IdInput = newInput.Id,
                Count = count,
                
                IdUser = 1
            };
            context.InputInfos.Add(newInputInfo);
            context.SaveChanges();
        }
    }
}
