using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class CustomerObject
    {
        private readonly CustomerDAO _customerDAO;

        public CustomerObject()
        {
            _customerDAO = new CustomerDAO();
        }

        // Get All Customers
        public List<Customer> GetAllCustomers()
        {
            try
            {
                return _customerDAO.GetAllCustomers();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Get Customer by ID
        public Customer GetCustomerById(int id)
        {
            try
            {
                return _customerDAO.GetCustomerById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Add Customer
        public bool AddCustomer(Customer customer)
        {
            try
            {
                return _customerDAO.AddCustomer(customer);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update Customer
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                return _customerDAO.UpdateCustomer(customer);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Delete Customer
        public bool DeleteCustomer(int id)
        {
            try
            {
                return _customerDAO.DeleteCustomer(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Customer GetCusById(int id) => _customerDAO.GetOutputById(id);
    }
}
