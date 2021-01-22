﻿using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Reflection;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Entities;

namespace MISA.Infrastructure
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: BaseEntity
    {
        #region DECLARE
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        protected string _tableName;
        #endregion
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(TEntity).Name;
        }
        public int Add(TEntity entity)
        {
            // TEntityhởi tạo TEntityết nối với Db:
            var parameters = MappingDBType(entity);
            // Thực thi commandText
            var rowAffects = _dbConnection.Execute($"Proc_Insert{_tableName}", parameters, commandType: CommandType.StoredProcedure);
            // Trả về số bản ghi thêm mới được
            return rowAffects;
        }

        public int Delete(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetEntities()
        {
            // Kết nối tới CSDL
            // Khởi tạo các commandText:
            var entities = _dbConnection.Query<TEntity>($"Proc_Get{_tableName}s", commandType: CommandType.StoredProcedure);
            // Trả về dữ liệu:
            return entities;
        }

        public TEntity GetEntityById(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public int Update(TEntity entity)
        {
            // Khởi tạo kết nối với Db:
            var parameters = MappingDBType(entity);
            // Thực thi commandText
            var rowAffects = _dbConnection.Execute($"Proc_Update{_tableName}", parameters, commandType: CommandType.StoredProcedure);
            // Trả về số bản ghi thêm mới được
            return rowAffects;
        }


        /// <summary>
        /// Mapping TEntityiểu dữ liệu Database
        /// CreatedBy: PQKHANH (3/1/2021)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        private DynamicParameters MappingDBType(TEntity entity)
        {
            var properties = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType;
                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    var dbValue = ((bool)propertyValue == true ? 1 : 0);
                    parameters.Add($"@{propertyName}", dbValue, DbType.Int32);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }
            return parameters;
        }

        public TEntity GetEntityByProperty(TEntity entity, PropertyInfo property)
        {
            var propertyName = property.Name;
            var propertyValue = property.GetValue(entity);
            var keyValue = entity.GetType().GetProperty($"{_tableName}Id").GetValue(entity);
            var query = string.Empty;
            if (entity.EntityState == EntityState.AddNew)
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}'";
            else if (entity.EntityState == EntityState.Update)
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}' AND {_tableName}Id <> '{keyValue}'";
            else
                return null;
            var entityReturn = _dbConnection.Query<TEntity>(query, commandType: CommandType.Text).FirstOrDefault();
            return entityReturn;
        }
    }
}
