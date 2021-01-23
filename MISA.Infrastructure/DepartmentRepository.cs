using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure
{
    public class DepartmentRepository : BaseRepository<Department>
    {
        IConfiguration _configuration;
        public DepartmentRepository(IConfiguration configuration) : base(configuration) 
        {
            _configuration = configuration;
        }

    }
}
