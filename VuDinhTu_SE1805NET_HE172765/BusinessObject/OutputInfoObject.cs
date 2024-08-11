using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public class OutputInfoObject
    {
        private readonly OutputInfoDAO _outputDao;

        public OutputInfoObject()
        {
            _outputDao = new OutputInfoDAO();
        }

        public List<HoaDonXuat> GetAllOutputs()
        {
            return _outputDao.GetAll();
        }
        public List<HoaDonXuat> GetAllOutputsProcess()
        {
            return _outputDao.GetAllProcess();
        }

        //public void AddOutput(OutputInfo output)
        //{
        //    if (_outputDao.CanExport(output.IdObject, output.Count))
        //    {
        //        _outputDao.AddOutput(output);
        //    }
        //    else
        //    {
        //        throw new Exception("Số lượng vật tư không đủ để xuất kho.");
        //    }
        //}

        public bool IsOutputIdExists(int idOutputInfo) => _outputDao.IsOutputIdExists(idOutputInfo);

        public int GetTotalOutputCount()
        {
            return _outputDao.GetTotalCount();
        }
        //public bool CanExport(int idObject, int count)
        //{
        //    return _outputDao.CanExport(idObject, count);
        //}

        public void AddOutput(OutputInfo output, string color)
        {
            if (_outputDao.CanExport(output.IdObject, output.Count ?? 0 , color))
            {
                _outputDao.AddOutput(output);
            }
            else
            {
                throw new Exception("Số lượng vật tư không đủ để xuất kho.");
            }
        }
        public OutputInfo GetById(int id) => _outputDao.GetOutputById(id);
        public void UpdateOutInfo(OutputInfo output) => _outputDao.Update(output);
        public void AddBill(BillHistory billHistory) => _outputDao.AddBill(billHistory);
       
    }
}
