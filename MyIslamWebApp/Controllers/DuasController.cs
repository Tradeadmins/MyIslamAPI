using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.Dua;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// Duas Apis List
 /// </summary>
    [RoutePrefix("api/dua")]
    public class DuasController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IDuaService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IDuaService() instance
        /// </summary>
        /// <param name="service"></param>
        public DuasController(IDuaService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public DuasController()
        {
            _service = new DuaService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new Dua
        /// </summary>
        /// <remarks>
        /// Add a new Dua
        /// </remarks>
        /// <param name="dua">Dua to add</param>
        /// <returns></returns>
        /// <response code="201">Dua 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] DuaDTO dua)
        {
            try
            {
                if (dua == null)
                    return BadRequest("Dua Data model is empty.");
                _service.AddDua(dua, userId);
                return Ok("Dua Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing Dua
        /// </summary>
        /// <param name="dua">Dua to update</param>
        /// <returns></returns>
        /// <response code="200">Dua updated</response>
        /// <response code="404">Dua not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] DuaDTO dua)
        {
            try
            {
                if (dua != null)
                {
                    var result = _service.UpdateDua(dua, userId);
                    if (result)
                        return Ok("Dua Updated Successfully.");
                    else
                        return Ok("Dua not found.");
                }
                else
                {
                    return BadRequest("Dua Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a Dua
        /// </summary>
        /// <remarks>
        /// Delete a Dua
        /// </remarks>
        /// <param name="duaId">Id of the Dua to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int duaId)
        {
            try
            {
                if (!int.Equals(duaId, 0))
                {
                    var response = _service.DeleteDuaById(duaId, userId);
                    if (response)
                        return Ok("Dua has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("Dua Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get Dua by id
        /// </summary>
        /// <remarks>
        /// Get a Dua by id
        /// </remarks>
        /// <param name="duaId">Id of Dua</param>
        /// <returns></returns>
        /// <response code="200">Dua found</response>
        /// <response code="404">Dua not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int duaId)
        {
            try
            {
                if (!int.Equals(duaId, 0))
                {
                    var dua = _service.GetDuaById(duaId);

                    if (dua != null)
                        return Ok(dua);
                    else
                        return BadRequest("Dua not available.");
                }
                else
                {
                    return BadRequest("Dua ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all Dua
        /// </summary>
        /// <remarks>
        /// Get a list of all Dua
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllDua") Route("GetAllDua")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allDuas = _service.GetAllDuas(pageIndex, pageSize);
                return Ok(allDuas);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get Dua by CategoryId
        /// </summary>
        /// <remarks>
        /// Get a Dua by CategoryId
        /// </remarks>
        /// <param name="duaCategoryId">CategoryId of Dua</param>
        /// <returns></returns>
        /// <response code="200">Dua found</response>
        /// <response code="404">Dua not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllDuaByCategoryId") Route("GetAllDuaByCategoryId")]
        public IHttpActionResult GetAllDuaByCategoryId(int duaCategoryId)
        {
            try
            {
                if (!int.Equals(duaCategoryId, null))
                {
                    var dua = _service.GetAllDuaByCategoryId(duaCategoryId);

                    if (dua != null)
                        return Ok(dua);
                    else
                        return BadRequest("Dua Not Found");
                }
                else
                {
                    return BadRequest("The Dua CategoryId is invalid");
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
