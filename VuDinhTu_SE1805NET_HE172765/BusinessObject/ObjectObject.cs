using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class ObjectObject
    {
        private readonly ObjectDAO objectDao;

        public ObjectObject(){

            objectDao = new ObjectDAO();
        }
        public void AddObject(DataAccess.Models.Object obj) => objectDao.Add(obj);
        public List<DataAccess.Models.Object> GetAllObject() => objectDao.GetAll();
        public int GetTotalInputInfoCount(int objectId) => objectDao.GetTotalInputInfo(objectId);
        public int GetTotalOutputInfoCount(int objectId) => objectDao.GetTotalOutputInfo(objectId);
        public int GetTotalInventory(int objectId) => objectDao.GetInventory(objectId);

        public void UpdateObj(DataAccess.Models.Object obj) => objectDao.Update(obj);
        public List<ObjectDetail> GetAllObjDetail() => objectDao.GetAllObjDetail();
        public void UpdateObject(DataAccess.Models.Object obj) => objectDao.UpdateObject(obj);
        public void DeleteObject(DataAccess.Models.Object obj)
        {
            objectDao.Delete(obj);
        }
        public List<ObjectDetail> SearchByNameDetailObj(string name)
        {
            return objectDao.SearchByNameDetail(name);
        }
        public List<ObjectDetail> GetDetailsByObjectId(int objectId) => objectDao.GetDetailsByObjectId(objectId);

        public List<DataAccess.Models.Object> GetAll() => objectDao.GetAll();
        public DataAccess.Models.Object GetObjById(int id) => objectDao.GetObjectById(id);
    }
}
