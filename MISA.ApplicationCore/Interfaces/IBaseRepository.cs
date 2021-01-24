using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 22/01/2021
        IEnumerable<TEntity> GetEntities();

        /// <summary>
        /// Lấy dữ liệu qua id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 22/01/2021
        TEntity GetEntityById(Guid entityId);

        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 22/01/2021
        int Add(TEntity entity);

        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 22/01/2021
        int Update(TEntity entity);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 22/01/2021
        int Delete(Guid entityId);

        /// <summary>
        /// Lấy dữ liệu bằng property
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 22/01/2021
        TEntity GetEntityByProperty(TEntity entity, PropertyInfo property);
        List<TEntity> GetEntitiesFilter(string specs, Guid? departmentId, Guid? positionId);
    }
}
