﻿using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    public interface ICustomerService : IBaseService<Customer>
    {
        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH(4/1/2021)
        IEnumerable<Customer> GetCustomerPaging(int limit, int offset);
        /// <summary>
        /// Lấy danh sách khách hàng theo nhóm khách hàng
        /// </summary>
        /// <param name="GroupId">Id nhóm khách hàng</param>
        /// <returns>List khách hàng</returns>
        /// CreatedBy: PQKHANH(4/1/2021)
        IEnumerable<Customer> GetCustomersByGroup(Guid GroupId);
    }
}
