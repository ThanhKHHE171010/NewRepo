using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAL
{
    public class OutputInfoDAO
    {
        private readonly QuanLyKhoOtoContext _context;

        public OutputInfoDAO()
        {
            _context = new QuanLyKhoOtoContext();
        }

        public List<HoaDonXuat> GetAll()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                var outputList = context.OutputInfos
                                        .Include(o => o.IdOutputNavigation)
                                        .Include(o => o.IdObjectNavigation)
                                        .Include(o => o.IdUserNavigation)
                                        .Include(o => o.IdCustomerNavigation)
                                        .Select(o => new HoaDonXuat
                                        {
                                            IdOutputInfo = o.Id,
                                            ObjectName = o.IdObjectNavigation.DisplayName,
                                            Color = o.Color,
                                            Count = o.Count,
                                            CusName = o.IdCustomerNavigation.DisplayName,
                                            UName = o.IdUserNavigation.DisplayName,
                                            DateOutput = o.IdOutputNavigation.DateOutput,
                                            Status = o.Status
                                        })
                                        .ToList();
                return outputList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<HoaDonXuat> GetAllProcess()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                var outputList = context.OutputInfos
                                        .Include(o => o.IdOutputNavigation)
                                        .Include(o => o.IdObjectNavigation)
                                        .Include(o => o.IdUserNavigation)
                                        .Include(o => o.IdCustomerNavigation)
                                        .Select(o => new HoaDonXuat
                                        {
                                            IdOutputInfo = o.Id,
                                            ObjectName = o.IdObjectNavigation.DisplayName,
                                            Color = o.Color,
                                            Count = o.Count,
                                            CusName = o.IdCustomerNavigation.DisplayName,
                                            UName = o.IdUserNavigation.DisplayName,
                                            DateOutput = o.IdOutputNavigation.DateOutput,
                                            Status = o.Status
                                        })
                                        .Where(o => o.Status == "process")
                                        .ToList();
                return outputList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public bool CanExport(int idObject, int count, string capa)
        //{
        //    using var context = new QuanLyKhoOtoContext();

        //    //var totalInput = context.InputInfos
        //    //    .Where(ii => ii.IdObject == idObject && ii.Capacity == capa)
        //    //    .Sum(ii => ii.Count);
        //    var totalInput = context.InputInfos
        //      .Where(ii => ii.IdObject == idObject )
        //      .Sum(ii => ii.Count);

        //    //var totalOutput = context.OutputInfos
        //    //    .Where(oi => oi.IdObject == idObject && oi.Capacity == capa
        //    //                 && (oi.Status == "accept" || oi.Status == "process"))
        //    //    .Sum(oi => oi.Count ?? 0);


        //    var totalOutput = context.OutputInfos
        //       .Where(oi => oi.IdObject == idObject
        //                    && (oi.Status == "accept" || oi.Status == "process"))
        //       .Sum(oi => oi.Count);

        //    return (totalInput - totalOutput) >= count;
        //}
        public bool CanExport(int idObject, int count, string color)
        {
            using var context = new QuanLyKhoOtoContext();

            var totalInput = context.InputInfos
                .Where(ii => ii.IdObject == idObject && ii.Color == color)
                .Sum(ii => ii.Count);

            var totalOutput = context.OutputInfos
                .Where(oi => oi.IdObject == idObject && oi.Color == color
                             && (oi.Status == "accept" || oi.Status == "process"))
                .Sum(oi => oi.Count ?? 0);

            return (totalInput - totalOutput) >= count;
        }

        public void AddOutput(OutputInfo output)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                output.Status = "process";
                context.OutputInfos.Add(output);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsOutputIdExists(int idOutputInfo)
        {
            using var context = new QuanLyKhoOtoContext();
            return context.OutputInfos.Any(oi => oi.Id == idOutputInfo);
        }

        public int GetTotalCount()
        {
            using var context = new QuanLyKhoOtoContext();
            return context.OutputInfos.Sum(oi => oi.Count ?? 0);
        }

        public int GetObjectDetailId(int objectId, string color)
        {
            var objectDetailId = _context.ObjectDetails
                                        .Where(od => od.IdObject == objectId && od.Color == color)
                                        .Select(od => od.Id)
                                        .FirstOrDefault();
            return objectDetailId;
        }
        public OutputInfo GetOutputById(int id)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.OutputInfos.FirstOrDefault(o => o.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(OutputInfo outputInfo)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Entry(outputInfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Error checking existence of supplier by name: {e.Message}");
            }
        }
        public void AddBill(BillHistory bill)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.BillHistories.Add(bill);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
