using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Khách hàng
    /// ----------
    /// CreatedBy : PQKHANH(31/12/2020)
    /// </summary>
    public class Customer : BaseEntity
    {
        #region Declare
        #endregion

        #region Constructor
        #endregion

        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>

        [PrimaryKey]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [CheckDuplicate]
        [Required]
        [DisplayName("Mã khách hàng")]
        [MaxLength(20,"Mã khách hàng không vượt quá 20 kí tự.")]
        public string CustomerCode { get; set; }

        /// <summary>
        /// Họ và tên
        /// </summary> 
        [DisplayName("họ và tên")]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính (0-nữ,1-Nam,2-Khác)
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ngày tháng năm sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [CheckDuplicate]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// mã nhóm khách hàng
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Số tiền nợ
        /// </summary>
        public double? DebitAmount { get; set; }

        /// <summary>
        /// Mã thẻ thành viên
        /// </summary>
        public string MemberCardCode { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Mã số thuế công ty
        /// </summary>
        public string CompanyTaxCode { get; set; }

        #endregion

        #region Method
        #endregion
    }
}
