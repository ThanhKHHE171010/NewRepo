using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAL
{
    public class SuplierDAO
    {
        // Create
        public bool AddSuplier(Suplier suplier)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Supliers.Add(suplier);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Read
        public Suplier GetSuplierById(int id)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Supliers.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update
        public bool UpdateSuplier(Suplier suplier)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Supliers.Update(suplier);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Delete
        public bool DeleteSuplier(int id)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                var suplier = context.Supliers.FirstOrDefault(s => s.Id == id);
                if (suplier != null)
                {
                    context.Supliers.Remove(suplier);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // List All
        public List<Suplier> GetAllSupliers()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Supliers.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool SuplierExistsByName(string name)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Supliers.Any(s => s.DisplayName == name);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
