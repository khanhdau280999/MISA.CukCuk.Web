using MISA.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ApplicationCore.Interfaces
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 23/01/2021
        IEnumerable<TEntity> GetEntities();

        /// <summary>
        /// Lấy dữ liệu bằng id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 23/01/2021
        TEntity GetEntityById(Guid entityId);

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 23/01/2021
        ServiceResult Add(TEntity entity);

        /// <summary>
        /// Sửa thông tin dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 23/01/2021
        ServiceResult Update(TEntity entity);

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// CreatedBy: PQKHANH 23/01/2021
        ServiceResult Delete(Guid entityId);
        List<TEntity> GetEntitiesFilter(string specs, Guid? departmentId, Guid? positionId);
    }
}
