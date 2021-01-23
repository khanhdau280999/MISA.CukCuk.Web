using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Infrastructure
{
    public class PoistionRepository : BaseRepository<Position>
    {
        IConfiguration _configuration;
        public PoistionRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
    }
}
