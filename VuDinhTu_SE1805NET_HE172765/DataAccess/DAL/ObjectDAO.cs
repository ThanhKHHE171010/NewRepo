using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    public class ObjectDAO
    {
   
        public List<Models.Object> GetAll()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Objects.Include(o => o.IdSuplierNavigation)
                                      .Include(o => o.ObjectDetails)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving objects: {ex.Message}");
            }
        }
        public void Add(Models.Object obj)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Objects.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding object: {ex.Message}");
            }
        }
      
       


        public void Update(Models.Object obj)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Entry(obj).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Error updating object: {e.Message}");
            }
        }


        public void Delete(Models.Object obj)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                var isUsed = context.InputInfos.Any(i => i.IdObject == obj.Id) || context.OutputInfos.Any(o => o.IdObject == obj.Id);
                if (isUsed)
                {
                    throw new Exception("Không thể xóa vật tư đã được sử dụng trong quá trình nhập, xuất kho.");
                }

                context.Objects.Remove(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting object: {ex.Message}");
            }
        }
        public void UpdateObject(Models.Object obj)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Objects.Update(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating object: {ex.Message}");
            }
        }
        public List<ObjectDetail> GetAllObjDetail()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.ObjectDetails.Include(o => o.IdObjectNavigation).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating object: {ex.Message}");
            }
        }
        public List<ObjectDetail> SearchByNameDetail(string name)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.ObjectDetails
                              .Include(o => o.IdObjectNavigation)
                              .Where(o => EF.Functions.Like(o.IdObjectNavigation.DisplayName, $"%{name}%"))
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching object details by name: {ex.Message}");
            }
        }
        public List<ObjectDetail> GetDetailsByObjectId(int objectId)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.ObjectDetails
                              .Where(od => od.IdObject == objectId)
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving object details by object Id: {ex.Message}");
            }
        }
        public Models.Object GetObjectById(int id)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Objects.FirstOrDefault(o => o.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetTotalInputInfo(int objectId)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                int totalCount;

                if (objectId == 0)
                {
                    totalCount = context.InputInfos.Sum(ii => ii.Count);
                }
                else
                {
                    totalCount = context.InputInfos
                                        .Where(ii => ii.IdObject == objectId)
                                        .Sum(ii => ii.Count);
                }

                return totalCount;
            }
            catch (Exception e)
            {
                throw new Exception($"Error retrieving total input info count: {e.Message}");
            }
        }

        public int GetTotalOutputInfo(int objectId)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                int totalCount;

                if (objectId == 0)
                {
                    totalCount = context.OutputInfos.Sum(ii => ii.Count ?? 0);
                }
                else
                {
                    totalCount = context.OutputInfos
                                        .Where(ii => ii.IdObject == objectId && ii.Status == "accept")
                                        .Sum(ii => ii.Count ?? 0);
                }

                return totalCount;
            }
            catch (Exception e)
            {
                throw new Exception($"Error retrieving total output info count: {e.Message}");
            }
        }

        public int GetInventory(int objectId)
        {
            try
            {
                return GetTotalInputInfo(objectId) - GetTotalOutputInfo(objectId);
            }
            catch (Exception e)
            {
                throw new Exception($"Error retrieving inventory count: {e.Message}");
            }
        }


    }
}
