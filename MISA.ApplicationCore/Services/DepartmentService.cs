using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class DepartmentService : BaseService<Department>
    {
        IBaseRepository<Department> _baseRepository;
        public DepartmentService(IBaseRepository<Department> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
    }
}
