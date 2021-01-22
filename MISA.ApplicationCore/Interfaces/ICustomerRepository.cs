using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    /// <summary>
    /// Interface danh mục khách hàng
    /// </summary>
    /// CreatedBy : PQKHANH (4/1/2021)
    public interface ICustomerRepository : IBaseRepository<Customer>
    {

        //IEnumerable<Customer> GetCustomers();
        //Customer GetCustomerById(Guid customerId);
        //int AddCustomer(Customer customer);
        //int UpdateCustomer(Customer customer);
        //int DeleteCustomer(Guid customerId);

        /// <summary>
        /// Lấy thông tin khách hàng theo mã khách hàng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy : PQKHANH (4/1/2021)
        Customer GetCustomerByCode(string customerCode);
    }
}
