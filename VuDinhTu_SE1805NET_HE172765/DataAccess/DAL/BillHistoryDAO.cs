using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    public class BillHistoryDAO
    {
        public List<BillHistory> getAll()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.BillHistories.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<BillHistory> getBillByIdCus(int customerId)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.BillHistories.Where(bh => bh.IdCustomer == customerId).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
