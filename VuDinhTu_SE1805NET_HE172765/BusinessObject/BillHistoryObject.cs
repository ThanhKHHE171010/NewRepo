using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class BillHistoryObject
    {
        private readonly BillHistoryDAO billHistoryDAO;
        public BillHistoryObject()
        {
            billHistoryDAO = new BillHistoryDAO();
        }

        public List<BillHistory> getByIdCus(int id)
        {
            return billHistoryDAO.getBillByIdCus(id);
        }
        public List<BillHistory> getAll()
        {
            return billHistoryDAO.getAll();
        }
    }
}
