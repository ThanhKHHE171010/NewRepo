using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class InputInfoObject
    {
        private readonly InputInfoDAO inputDao;

        public InputInfoObject()
        {
            inputDao = new InputInfoDAO();
        }

        public void AddInput(InputInfo inputInfo)
        {
            inputDao.AddInput(inputInfo);
        }

        public List<HoaDonNhap> GetAll() => inputDao.GetAll();
       
        public bool IsIdInputExists(int idInput) => inputDao.IsIdInputExists(idInput);

        public bool IsIdObjectExists(int idObject) => inputDao.IsIdObjectExists(idObject);
        //public int GetTotalInputCount()
        //{
        //    return inputDao.GetTotalCount();
        //}
    }
}
