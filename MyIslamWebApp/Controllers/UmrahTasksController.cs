using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.UmrahTask;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// UmrahTasks Apis List
 /// </summary>
    [RoutePrefix("api/umrahTask")]
    public class UmrahTasksController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IUmrahTaskService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IUmrahTaskService() instance
        /// </summary>
        /// <param name="service"></param>
        public UmrahTasksController(IUmrahTaskService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public UmrahTasksController()
        {
            _service = new UmrahTaskService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new UmrahTask
        /// </summary>
        /// <remarks>
        /// Add a new UmrahTask
        /// </remarks>
        /// <param name="umrahTask">UmrahTask to add</param>
        /// <returns></returns>
        /// <response code="201">UmrahTask 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] UmrahTaskDTO umrahTask)
        {
            try
            {
                if (umrahTask == null)
                    return BadRequest("UmrahTask Data model is empty.");
                _service.AddUmrahTask(umrahTask, userId);
                return Ok("UmrahTask Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing UmrahTask
        /// </summary>
        /// <param name="umrahTask">UmrahTask to update</param>
        /// <returns></returns>
        /// <response code="200">UmrahTask updated</response>
        /// <response code="404">UmrahTask not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] UmrahTaskDTO umrahTask)
        {
            try
            {
                if (umrahTask != null)
                {
                    var result = _service.UpdateUmrahTask(umrahTask, userId);
                    if (result)
                        return Ok("UmrahTask Updated Successfully.");
                    else
                        return Ok("UmrahTask not found.");
                }
                else
                {
                    return BadRequest("UmrahTask Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a UmrahTask
        /// </summary>
        /// <remarks>
        /// Delete a UmrahTask
        /// </remarks>
        /// <param name="umrahTaskId">Id of the UmrahTask to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int umrahTaskId)
        {
            try
            {
                if (!int.Equals(umrahTaskId, 0))
                {
                    var response = _service.DeleteUmrahTaskById(umrahTaskId, userId);
                    if (response)
                        return Ok("UmrahTask has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("UmrahTask Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get UmrahTask by id
        /// </summary>
        /// <remarks>
        /// Get a UmrahTask by id
        /// </remarks>
        /// <param name="umrahTaskId">Id of UmrahTask</param>
        /// <returns></returns>
        /// <response code="200">UmrahTask found</response>
        /// <response code="404">UmrahTask not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int umrahTaskId)
        {
            try
            {
                if (!int.Equals(umrahTaskId, 0))
                {
                    var umrahTask = _service.GetUmrahTaskById(umrahTaskId);

                    if (umrahTask != null)
                        return Ok(umrahTask);
                    else
                        return BadRequest("UmrahTask not available.");
                }
                else
                {
                    return BadRequest("UmrahTask ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all UmrahTask
        /// </summary>
        /// <remarks>
        /// Get a list of all UmrahTask
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllUmrahTask") Route("GetAllUmrahTask")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allUmrahTasks = _service.GetAllUmrahTasks(pageIndex, pageSize, userId);
                return Ok(allUmrahTasks);
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
