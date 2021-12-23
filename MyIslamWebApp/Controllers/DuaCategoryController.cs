using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.DuaCategory;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// DuaCategory Apis List
 /// </summary>
    [RoutePrefix("api/duaCategory")]
    public class DuaCategoryController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IDuaCategoryService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IIDuaCategoryService() instance
        /// </summary>
        /// <param name="service"></param>
        public DuaCategoryController(IDuaCategoryService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public DuaCategoryController()
        {
            _service = new DuaCategoryService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new DuaCategory
        /// </summary>
        /// <remarks>
        /// Add a new DuaCategory
        /// </remarks>
        /// <param name="duaCategory">DuaCategory to add</param>
        /// <returns></returns>
        /// <response code="201">DuaCategory 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] DuaCategoryDTO duaCategory)
        {
            try
            {
                if (duaCategory == null)
                    return BadRequest("DuaCategory Data model is empty.");
                _service.AddDuaCategory(duaCategory, userId);
                return Ok("DuaCategory Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing DuaCategory
        /// </summary>
        /// <param name="duaCategory">DuaCategory to update</param>
        /// <returns></returns>
        /// <response code="200">DuaCategory updated</response>
        /// <response code="404">DuaCategory not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] DuaCategoryDTO duaCategory)
        {
            try
            {
                if (duaCategory != null)
                {
                    var result = _service.UpdateDuaCategory(duaCategory, userId);
                    if (result)
                        return Ok("DuaCategory Updated Successfully.");
                    else
                        return Ok("DuaCategory not found.");
                }
                else
                {
                    return BadRequest("DuaCategory Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a DuaCategory
        /// </summary>
        /// <remarks>
        /// Delete a DuaCategory
        /// </remarks>
        /// <param name="duaCategoryId">Id of the DuaCategory to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int duaCategoryId)
        {
            try
            {
                if (!int.Equals(duaCategoryId, 0))
                {
                    var response = _service.DeleteDuaCategoryById(duaCategoryId, userId);
                    if (response)
                        return Ok("DuaCategory has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("DuaCategory Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get DuaCategory by id
        /// </summary>
        /// <remarks>
        /// Get a DuaCategory by id
        /// </remarks>
        /// <param name="duaCategoryId">Id of DuaCategory</param>
        /// <returns></returns>
        /// <response code="200">DuaCategory found</response>
        /// <response code="404">DuaCategory not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int duaCategoryId)
        {
            try
            {
                if (!int.Equals(duaCategoryId, 0))
                {
                    var duaCategory = _service.GetDuaCategoryById(duaCategoryId);

                    if (duaCategory != null)
                        return Ok(duaCategory);
                    else
                        return BadRequest("DuaCategory not available.");
                }
                else
                {
                    return BadRequest("DuaCategory ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all DuaCategory
        /// </summary>
        /// <remarks>
        /// Get a list of all DuaCategory
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllDuaCategory") Route("GetAllDuaCategory")]
        public IHttpActionResult Get()
        {
            try
            {
                var allDuaCategory = _service.GetAllDuaCategory();
                return Ok(allDuaCategory);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }
        #endregion
    }
}
