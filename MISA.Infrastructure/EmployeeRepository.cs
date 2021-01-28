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
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Employee> FilterEmployee(string inputValue, Guid? departmentId, Guid? positionId)
        {
            //Get value input
            var inputs = inputValue != null ? inputValue : string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", inputs);
            parameters.Add("@FullName", inputs);
            parameters.Add("@PhoneNumber", inputs);
            parameters.Add("@DepartmentId", departmentId, DbType.String);
            parameters.Add("@PositionId", positionId, DbType.String);
            var employees = _dbConnection.Query<Employee>("Proc_FilterEmployee", parameters, commandType: CommandType.StoredProcedure).ToList();
            return employees;
        }

        public Employee GetEmployeeByCode(string employeeCode)
        {
            var employeeDuplicate = _dbConnection.QueryFirstOrDefault<Employee>($"Prop_Get{_tableName}ByCode", param: new { EmployeeCode = employeeCode }, commandType: CommandType.StoredProcedure);
            return employeeDuplicate;
        }

        public double GetMaxEmployeeCode()
        {
            var max = _dbConnection.ExecuteScalar<double>("Proc_GetEmployeeCode", commandType: CommandType.StoredProcedure);
            return max;
        }
    }
}
