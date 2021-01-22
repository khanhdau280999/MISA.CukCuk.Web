using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        //IEnumerable<Employee> GetEmployees();
        //Employee GetEmployeeById(Guid employeeId);
        //int AddEmployee(Employee employee);
        //int UpdateEmployee(Employee employee);
        //int DeleteEmployee(Guid employeeId);
        Employee GetEmployeeByCode(string employeeCode);
    }
}
