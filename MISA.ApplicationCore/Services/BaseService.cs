using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Declare
        IBaseRepository<TEntity> _baseRepository;
        ServiceResult _serviceResult;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult() { MISACode = Enums.MISACode.Success };

        }
        #endregion
        public virtual ServiceResult Add(TEntity entity)
        {
            entity.EntityState = Enums.EntityState.AddNew;
            // Thực hiện Validate
            var isValidate = Validate(entity);
            //if (isValidate == true)
            //    isValidate = ValidateCustom(entity);
            if (isValidate == true)
            {
                _serviceResult.Data = _baseRepository.Add(entity);
                _serviceResult.MISACode = Enums.MISACode.IsValid;
                return _serviceResult;
            }
            else
            {

                return _serviceResult;
            }

        }

        public ServiceResult Delete(Guid entityId)
        {
            var rowAffects = _baseRepository.Delete(entityId);
            if (rowAffects == 0)
            {
                _serviceResult.MISACode = MISACode.NotSuccess;
                _serviceResult.Messenger = Properties.Resources.Msg_NotSuccess;
                _serviceResult.Data = 0;
                return _serviceResult;
            }
            _serviceResult.MISACode = MISACode.Success;
            _serviceResult.Messenger = Properties.Resources.Msg_Success;
            _serviceResult.Data = rowAffects;
            return _serviceResult;

            //_serviceResult.Data = _baseRepository.Delete(entityId);
            //return _serviceResult;
        }

        public IEnumerable<TEntity> GetEntities()
        {
            return _baseRepository.GetEntities();
        }

        public TEntity GetEntityById(Guid entityId)
        {
            return _baseRepository.GetEntityById(entityId);
        }

        public ServiceResult Update(TEntity entity)
        {
            entity.EntityState = Enums.EntityState.Update;
            var isValidate = Validate(entity);
            if (isValidate == true)
            {
                _serviceResult.Data = _baseRepository.Update(entity);
                _serviceResult.MISACode = Enums.MISACode.IsValid;
                return _serviceResult;
            }
            else
            {

                return _serviceResult;
            }
        }

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private bool Validate(TEntity entity)
        {
            var mesArrayError = new List<string>();
            var isValidate = true;
            // Đọc các Property:
            var properties = entity.GetType().GetProperties();

            _serviceResult.MISACode = MISACode.IsValid;
            _serviceResult.Messenger = Properties.Resources.Msg_IsValid;
            _serviceResult.Data = Properties.Resources.Msg_IsValid;

            foreach (var property in properties)
            {
                var displayName = string.Empty;
                var propertyValue = property.GetValue(entity);
                var displayNameAttributes = property.GetCustomAttributes(typeof(DisplayName), true);
                if (displayNameAttributes.Length > 0)
                {
                    displayName = (displayNameAttributes[0] as DisplayName).Name;
                }
                // Kiểm tra xem có attribute cần phải validate không :
                if (property.IsDefined(typeof(Required), false))
                {
                    // Check bắt buộc nhập:
                    if (propertyValue == null || propertyValue.ToString().Trim() == "")
                    {
                        isValidate = false;
                        mesArrayError.Add(string.Format(Properties.Resources.Msg_Required, displayName));
                        _serviceResult.MISACode = Enums.MISACode.NotValid;
                        _serviceResult.Messenger = Properties.Resources.Msg_IsNotValid;
                    }
                }
                if (property.IsDefined(typeof(CheckDuplicate), false))
                {
                    // Check trùng dữ liệu :
                    var propertyName = property.Name;
                    var entityDuplicate = _baseRepository.GetEntityByProperty(entity, property);
                    if (entityDuplicate != null)
                    {
                        isValidate = false;
                        mesArrayError.Add(string.Format(Properties.Resources.Msg_Duplicate, displayName));
                        _serviceResult.MISACode = Enums.MISACode.NotValid;
                        _serviceResult.Messenger = Properties.Resources.Msg_IsNotValid;
                    }
                }


                // Kiểm tra độ dài tối đa
                if (property.IsDefined(typeof(MaxLength), false))
                {
                    // Lấy độ dài đã khai báo
                    var attributeMaxLength = (property.GetCustomAttributes(typeof(MaxLength), true)[0]);
                    var length = (attributeMaxLength as MaxLength).Value;
                    var msg = (attributeMaxLength as MaxLength).ErrorMsg;
                    if(propertyValue != null)
                    {
                        if (propertyValue.ToString().Trim().Length > length)
                        {
                            isValidate = false;
                            mesArrayError.Add(msg ?? string.Format(Properties.Resources.Msg_MaxLengthError, length));
                            _serviceResult.MISACode = Enums.MISACode.NotValid;
                            _serviceResult.Messenger = Properties.Resources.Msg_IsNotValid;
                        }
                    }
                }
            }
            _serviceResult.Data = mesArrayError;
            if (isValidate == true)
            {
                isValidate = ValidateCustom(entity);
            }
            return isValidate;
        }

        /// <summary>
        /// Function used to check data custom
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool ValidateCustom(TEntity entity)
        {
            return true;
        }
    }
}
