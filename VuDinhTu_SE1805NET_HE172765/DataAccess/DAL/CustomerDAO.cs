using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAL
{
    public class CustomerDAO
    {
        // Create
        public bool AddCustomer(Customer customer)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Customers.Add(customer);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Read
        public Customer GetCustomerById(int id)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Customers.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                context.Customers.Update(customer);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Delete
        public bool DeleteCustomer(int id)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                var customer = context.Customers.FirstOrDefault(c => c.Id == id);
                if (customer != null)
                {
                    context.Customers.Remove(customer);
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
        public List<Customer> GetAllCustomers()
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Customers.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Customer GetOutputById(int id)
        {
            try
            {
                using var context = new QuanLyKhoOtoContext();
                return context.Customers.FirstOrDefault(o => o.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
