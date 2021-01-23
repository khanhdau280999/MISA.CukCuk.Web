using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IBaseRepository<Employee> _employeeRepository;
        public EmployeeService(IBaseRepository<Employee> employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
    }
}
