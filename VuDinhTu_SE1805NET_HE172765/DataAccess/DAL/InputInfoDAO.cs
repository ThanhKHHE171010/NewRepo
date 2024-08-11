using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    public class InputInfoDAO
    {
        public List<HoaDonNhap> GetAll()
        {
            using (var context = new QuanLyKhoOtoContext())
            {
                return context.InputInfos
                              .Include(i => i.IdObjectNavigation)
                              .ThenInclude(o => o.IdSuplierNavigation)
                              .Include(i => i.IdObjectNavigation.ObjectDetails)
                              .Select(i => new HoaDonNhap
                              {
                                  Id = i.Id,
                                  ObjectName = i.IdObjectNavigation.DisplayName,
                                  Color = i.Color,
                                  Count = i.Count,
                                  UserName = context.Users.FirstOrDefault(u => u.Id == i.IdUser).DisplayName,
                                  SuplierName = i.IdObjectNavigation.IdSuplierNavigation.DisplayName,
                                  DateInput = context.Inputs.FirstOrDefault(inp => inp.Id == i.IdInput).DateInput
                              }).ToList();
            }
        }




        public void AddInput(InputInfo inputInfo)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.InputInfos.Add(inputInfo);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsIdInputExists(int idInput)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.InputInfos.Any(ii => ii.IdInput == idInput);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsIdObjectExists(int idObject)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.InputInfos.Any(ii => ii.IdObject == idObject);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetTotalCount()
        {
            using var context = new QuanLyKhoOtoContext();
            return context.InputInfos.Sum(ii => ii.Count);
        }
    }
}
