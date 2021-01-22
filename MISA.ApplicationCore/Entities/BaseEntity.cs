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
        public MaxLength(int length, string errorMsg = null)
        {
            this.Value = length;
            this.ErrorMsg = ErrorMsg;
        }
    }

    public class BaseEntity
    {
        public EntityState EntityState { get; set; } = EntityState.AddNew;
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
