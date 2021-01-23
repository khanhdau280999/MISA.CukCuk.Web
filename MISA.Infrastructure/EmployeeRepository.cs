using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public Employee GetEmployeeByCode(string employeeCode)
        {
            var employeeDuplicate = _dbConnection.QueryFirstOrDefault<Employee>($"Prop_Get{_tableName}ByCode", param: new { EmployeeCode = employeeCode }, commandType: CommandType.StoredProcedure);
            return employeeDuplicate;
        }
    }
}
