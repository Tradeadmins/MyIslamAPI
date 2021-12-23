using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.CustomDua;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// CustomDuas Apis List
 /// </summary>
    [RoutePrefix("api/customDua")]
    public class CustomDuasController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly ICustomDuaService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the ICustomDuaService() instance
        /// </summary>
        /// <param name="service"></param>
        public CustomDuasController(ICustomDuaService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public CustomDuasController()
        {
            _service = new CustomDuaService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new CustomDua
        /// </summary>
        /// <remarks>
        /// Add a new CustomDua
        /// </remarks>
        /// <param name="customDua">CustomDua to add</param>
        /// <returns></returns>
        /// <response code="201">CustomDua 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] CustomDuaDTO customDua)
        {
            try
            {
                if (customDua == null)
                    return BadRequest("CustomDua Data model is empty.");
                _service.AddCustomDua(customDua, userId);
                return Ok("CustomDua Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing CustomDua
        /// </summary>
        /// <param name="customDua">CustomDua to update</param>
        /// <returns></returns>
        /// <response code="200">CustomDua updated</response>
        /// <response code="404">CustomDua not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] CustomDuaDTO customDua)
        {
            try
            {
                if (customDua != null)
                {
                    var result = _service.UpdateCustomDua(customDua, userId);
                    if (result)
                        return Ok("CustomDua Updated Successfully.");
                    else
                        return Ok("CustomDua not found.");
                }
                else
                {
                    return BadRequest("CustomDua Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a CustomDua
        /// </summary>
        /// <remarks>
        /// Delete a CustomDua
        /// </remarks>
        /// <param name="customDuaId">Id of the CustomDua to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int customDuaId)
        {
            try
            {
                if (!int.Equals(customDuaId, 0))
                {
                    var response = _service.DeleteCustomDuaById(customDuaId, userId);
                    if (response)
                        return Ok("CustomDua has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("CustomDua Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get CustomDua by id
        /// </summary>
        /// <remarks>
        /// Get a CustomDua by id
        /// </remarks>
        /// <param name="customDuaId">Id of CustomDua</param>
        /// <returns></returns>
        /// <response code="200">CustomDua found</response>
        /// <response code="404">CustomDua not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int customDuaId)
        {
            try
            {
                if (!int.Equals(customDuaId, 0))
                {
                    var customDua = _service.GetCustomDuaById(customDuaId);

                    if (customDua != null)
                        return Ok(customDua);
                    else
                        return BadRequest("CustomDua not available.");
                }
                else
                {
                    return BadRequest("CustomDua ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all CustomDua
        /// </summary>
        /// <remarks>
        /// Get a list of all CustomDua
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllCustomDua") Route("GetAllCustomDua")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allCustomDuas = _service.GetAllCustomDuas(pageIndex, pageSize);
                return Ok(allCustomDuas);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get CustomDua by UserId
        /// </summary>
        /// <remarks>
        /// Get a CustomDua by UserId
        /// </remarks>
        /// <param name="userId">UserId of CustomDua</param>
        /// <returns></returns>
        /// <response code="200">CustomDua found</response>
        /// <response code="404">CustomDua not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllCustomDuaByUserId") Route("GetAllCustomDuaByUserId")]
        public IHttpActionResult GetAllCustomDuaByUserId(string userId)
        {
            try
            {
                if (!string.Equals(userId, null))
                {
                    var customDua = _service.GetAllCustomDuaByUserId(userId);

                    if (customDua != null)
                        return Ok(customDua);
                    else
                        return BadRequest("Custom Dua Not Found");
                }
                else
                {
                    return BadRequest("The userid is invalid");
                }
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
