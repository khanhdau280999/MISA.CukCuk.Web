using MISA.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// check các trường bắt buộc nhập
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required:Attribute
    {

    }
    /// <summary>
    /// Check trùng lặp
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate:Attribute
    {

    }
    /// <summary>
    /// khóa chính định nghĩa
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }

    /// <summary>
    /// check dữ liệu kiểu Email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Email : Attribute { }

    /// <summary>
    /// check dữ liệu kiểu Số điện thoại
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneNum : Attribute { }


    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string Name { get; set; }
        public DisplayName(string name = "")
        {
            this.Name = name;
        }
    }

    /// <summary>
    /// validate độ dài 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        public int Value { get; set; }
        public string ErrorMsg { get; set; }
        public MaxLength(int length = 0, string errorMsg = null)
        {
            this.Value = length;
            this.ErrorMsg = errorMsg;
        }
    }

    public class BaseEntity
    {
        public EntityState EntityState { get; set; } = EntityState.AddNew;

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
