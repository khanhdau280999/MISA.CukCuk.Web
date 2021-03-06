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
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>, IDisposable where TEntity: BaseEntity
    {
        #region DECLARE
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        protected string _tableName = typeof(TEntity).Name;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
        }
        #endregion
        public int Add(TEntity entity)
        {
            var rowAffects = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    //Mapping type of data
                    var parameters = MappingDBType(entity);
                    //Excute commandText
                    rowAffects = _dbConnection.Execute($"Proc_Insert{_tableName}", parameters, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }

            }
            //Return number of record have been inserted
            return rowAffects;

        }

        public int Delete(Guid entityId)
        {
            var res = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                res = _dbConnection.Execute($"DELETE FROM {_tableName} WHERE {_tableName}Id = '{entityId.ToString()}'", commandType: CommandType.Text);
                transaction.Commit();
            }
            return res;

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
            //DynamicParameters id = new DynamicParameters();
            //id.Add(_paramName, entityId.ToString());
            //var res = _dbConnection.Query<TEntity>($"Proc_Get{_tableName}ById", id, commandType: CommandType.StoredProcedure).FirstOrDefault();
            //return res;

            //Create commandText
            var entity = _dbConnection.QueryFirstOrDefault<TEntity>($"SELECT * FROM {_tableName} WHERE {_tableName}Id = '{entityId.ToString()}' LIMIT 1", commandType: CommandType.Text);
            //Return data
            return entity;
        }


        public int Update(TEntity entity)
        {
            var rowAffects = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    //Mapping type of data
                    var parameters = MappingDBType(entity);
                    //Excute commandText
                    rowAffects = _dbConnection.Execute($"Proc_Update{_tableName}", parameters, commandType: CommandType.StoredProcedure);
                    //Return number of record have been inserted
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

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

        //public List<TEntity> GetEntitiesFilter(string specs, Guid? departmentId, Guid? positionId)
        //{
        //    var input = specs ?? string.Empty;
        //    var parameters = new DynamicParameters();
        //    parameters.Add($"@{_tableName}Code", input, DbType.String);
        //    parameters.Add("@FullName", input, DbType.String);
        //    parameters.Add("@PhoneNumber", input, DbType.String);
        //    parameters.Add("@DepartmentId", departmentId, DbType.String);
        //    parameters.Add("@PositionId", positionId, DbType.String);
        //    var entity = _dbConnection.Query<TEntity>($"Proc_Get{_tableName}Filter", parameters, commandType: CommandType.StoredProcedure).ToList();
        //    return entity;
        //}

        public void Dispose()
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
    }
}
