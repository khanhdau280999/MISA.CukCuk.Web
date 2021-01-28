using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    /// CreatedBy: PQKHANH (3/1/2021)
    public class Employee : BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [CheckDuplicate]
        [Required]
        [DisplayName("Mã nhân viên")]
        [MaxLength(20, "Mã nhân viên không vượt quá 20 ký tự!")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        [Required]
        [DisplayName("Tên nhân viên")]
        public string FullName { get; set; }

        /// <summary>
        /// Ngày tháng năm sinh
        /// </summary>
        [DisplayName("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân / thẻ căn cước
        /// </summary>
        [Required]
        [CheckDuplicate]
        [DisplayName("Số CMTND hoặc Căn cước")]
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp Chứng minh nhân dân / thẻ căn cước
        /// </summary>
        /// 
        [DisplayName("Ngày cấp")]
        public DateTime? IdentityDate { get; set; }


        /// <summary>
        /// Nơi cấp
        /// </summary>
        /// 
        [DisplayName("Nơi cấp")]
        public string IdentityPlace { get; set; }

        /// <summary>
        /// địa chỉ email
        /// </summary>
        [Required]
        [Email]
        [DisplayName("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Required]
        [CheckDuplicate]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Mã chức vụ
        /// </summary>
        [DisplayName("Id chức vụ")]
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        [DisplayName("Tên chức vụ")]
        public string PositionName { get; set; }

        /// <summary>
        /// ID Phòng ban
        /// </summary>
        [DisplayName("ID Phòng ban")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên Phòng ban
        /// </summary>
        [DisplayName("Tên Phòng ban")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mã số thuế cá nhân
        /// </summary>
        [DisplayName("Mã số thuế cá nhân")]
        public string PersonalTaxCode { get; set; }

        /// <summary>
        /// Ngày tham gia công ty
        /// </summary>
        [DisplayName("Ngày tham gia công ty")]
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// Trạng thái làm việc
        /// </summary>
        [DisplayName("Trạng thái làm việc")]
        public int? WorkStatus { get; set; }

        /// <summary>
        /// Giới tính (0-nữ, 1-nam, 2-khác)
        [DisplayName("Giới tính")]
        public int? Gender { get; set; }

        /// <summary>
        /// Mức lương
        /// </summary>
        [DisplayName("Lương")]
        public double? Salary { get; set; }

        #endregion

    }
}
