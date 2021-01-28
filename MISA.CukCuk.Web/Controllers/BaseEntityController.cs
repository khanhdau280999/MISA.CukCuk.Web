using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Enums;
using MISA.ApplicationCore.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<TEntity> : ControllerBase
    {
        IBaseService<TEntity> _baseService;
        public BaseEntityController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        /// CreatedBy : PQKHANH(31/12/2020)
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.GetEntities();
            return Ok(entities);
        }

        /// <summary>
        /// Lấy danh sách khách hàng theo id và tên
        /// </summary>
        /// <param name="id">id của khách hàng</param>
        /// <param name="name">tên của khách hàng</param>
        /// <returns>Danh sách khách hàng</returns>
        /// CreatedBy: PQKHANH(31/12/2020)
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var entity = _baseService.GetEntityById(Guid.Parse(id));
            return Ok(entity);
        }

        // POST api/<CustomersController>
        /// <summary>
        /// Api Thêm khách hàng
        /// </summary>
        /// <param name="customer">objec khách hàng</param>
        /// <returns>kết quả số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: PQKHANH(31/12/2020)
        [HttpPost]
        public IActionResult Post([FromBody] TEntity entity)
        {
            //var serviceResult = _baseService.Add(entity);
            //if (serviceResult.MISACode == MISACode.NotValid)
            //{
            //    return BadRequest(serviceResult);
            //}
            //return Created("Add",serviceResult);
            var rowAffects = _baseService.Add(entity);
            if (rowAffects.MISACode == MISACode.NotValid)
            {
                return BadRequest(rowAffects);
            }
            else
                return Ok(rowAffects);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute]string id, [FromBody] TEntity entity)
        {
            //var serviceResult = _baseService.Update(id, entity);
            //if (serviceResult.MISACode == MISACode.NotValid)
            //{
            //    return BadRequest(serviceResult);
            //}
            //return Ok(serviceResult);

            var keyProperty = entity.GetType().GetProperty($"{typeof(TEntity).Name}Id");
            if (keyProperty.PropertyType == typeof(Guid))
            {
                keyProperty.SetValue(entity, Guid.Parse(id));
            }
            else if (keyProperty.PropertyType == typeof(int))
            {
                keyProperty.SetValue(entity, int.Parse(id));
            }
            else
            {
                keyProperty.SetValue(entity, id);
            }
            var rowAffects = _baseService.Update(entity);
            if (rowAffects.MISACode == MISACode.NotValid)
            {
                return BadRequest(rowAffects);
            }
            else
                return Ok(rowAffects);

        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var serviceResult = _baseService.Delete(Guid.Parse(id));
            if (serviceResult.MISACode == MISACode.NotSuccess)
            {
                return NoContent();
            }
            return Ok(serviceResult);
        }

        [HttpGet("filter")]
        public IActionResult GetEntitiesFilter([FromQuery] string specs, [FromQuery] Guid? departmentId, [FromQuery] Guid? positionId)
        {
            return Ok(_baseService.GetEntitiesFilter(specs, departmentId, positionId));
        }

    }
}
