using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public class SuplierObject
    {
        private readonly SuplierDAO _suplierDAO;

        public SuplierObject()
        {
            _suplierDAO = new SuplierDAO();
        }

        // Get All Supliers
        public List<Suplier> GetAllSupliers()
        {
            try
            {
                return _suplierDAO.GetAllSupliers();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Get Suplier by ID
        public Suplier GetSuplierById(int id)
        {
            try
            {
                return _suplierDAO.GetSuplierById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Add Suplier
        public bool AddSuplier(Suplier suplier)
        {
            try
            {
                return _suplierDAO.AddSuplier(suplier);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update Suplier
        public bool UpdateSuplier(Suplier suplier)
        {
            try
            {
                return _suplierDAO.UpdateSuplier(suplier);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Delete Suplier
        public bool DeleteSuplier(int id)
        {
            try
            {
                return _suplierDAO.DeleteSuplier(id);
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
                return _suplierDAO.SuplierExistsByName(name);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
