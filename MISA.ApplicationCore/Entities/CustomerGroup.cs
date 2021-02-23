using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities
{
    public class CustomerGroup : BaseEntity
    {
        #region Property

        public Guid CustomerGroupId { get; set; }
        public string CustomerGroupName { get; set; }
        public string Description { get; set; }

        #endregion
    }
}
