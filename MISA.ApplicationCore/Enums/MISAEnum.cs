﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Enums
{
    /// <summary>
    /// MISACode để xác định trạng thái của việc validate
    /// </summary>
    public enum MISACode
    {
        /// <summary>
        /// Dữ liệu hợp lệ
        /// </summary>
        IsValid = 100,

        /// <summary>
        /// Dữ liệu chưa hợp lệ
        /// </summary>
        NotValid = 900,

        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200,


        /// <summary>
        /// Không thành công
        /// </summary>
        NotSuccess = 800,

        /// <summary>
        /// Lỗi Exception
        /// </summary>
        Exception = 500
    }

    /// <summary>
    /// Xác định trạng thái của object
    /// </summary>
    public enum EntityState
    {
        AddNew = 1,
        Update = 2,
        Delete = 3,
    }
}
