using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Web.Controllers
{
    public class PositionsController : BaseEntityController<Position>
    {
        IPositionService _positionService;
        public PositionsController(IPositionService positionService) : base(positionService)
        {
            _positionService = positionService;
        }
    }
}
