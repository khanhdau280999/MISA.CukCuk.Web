using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Filter employee by multiple conditions
        /// </summary>
        /// <param name="inputValue">employeeCode, fullName, phoneNumber</param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <returns>List employee</returns>
        /// createdBy: pqkhanh(28/1/2021)
        List<Employee> FilterEmployee(string inputValue, Guid? departmentId, Guid? positionId);

        /// <summary>
        /// Get max number only in employee code
        /// </summary>
        /// <returns>Only number in employee code</returns>
        /// createdBy: pqkhanh (28/1/2021)
        double GetMaxEmployeeCode();
    }
}
