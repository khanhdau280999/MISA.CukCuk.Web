using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using MISA.ApplicationCore.Interfaces;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Services;

namespace MISA.ApplicationCore
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        /// <summary>
        /// Tiêm DI khử phụ thuộc
        /// CreatedBy: PQKHANH (3/1/2021)
        /// </summary>
        ICustomerRepository _customerRepository;
        #region Constructor
        public CustomerService(ICustomerRepository customerRepository) :base(customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetCustomerPaging(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetCustomersByGroup(Guid departmentId)
        {
            throw new NotImplementedException();
        }
        // Sửa thông tin khách hàng

        // Xóa khách hàng:
        #endregion
    }
}
