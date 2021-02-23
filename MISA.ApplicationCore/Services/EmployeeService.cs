using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Method
        protected override bool ValidateCustom(Employee entity)
        {
            return true;
        }

        public List<Employee> FilterEmployee(string inputValue, Guid? departmentId, Guid? positionId)
        {
            return _employeeRepository.FilterEmployee(inputValue, departmentId, positionId);
        }

        public double GetMaxEmployeeCode()
        {
            return _employeeRepository.GetMaxEmployeeCode();
        }
        #endregion
    }
}
