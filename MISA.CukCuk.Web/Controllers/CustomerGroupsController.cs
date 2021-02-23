using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Web.Controllers
{
    public class CustomerGroupsController : BaseEntityController<CustomerGroup>
    {
        ICustomerGroupService _customerGroupService;

        public CustomerGroupsController(ICustomerGroupService customerGroupService) : base(customerGroupService)
        {
            _customerGroupService = customerGroupService;
        }
    }
}
