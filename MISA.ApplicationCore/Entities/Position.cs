using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Chức vụ
    /// </summary>
    /// CreatedBy: PQKHANH (22/01/2021)
    public class Position : BaseEntity
    {
        /// <summary>
        /// Mã chức vụ
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string PositionName { get; set; }
    }
}
