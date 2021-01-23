using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class PositionService : BaseService<Position>
    {
        IBaseRepository<Position> _baseRepository;
        public PositionService(IBaseRepository<Position> baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }
    }
}
