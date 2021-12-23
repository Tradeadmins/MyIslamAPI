using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.HajjTask;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// HajjTasks Apis List
 /// </summary>
    [RoutePrefix("api/hajjTask")]
    public class HajjTasksController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IHajjTaskService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IHajjTaskService() instance
        /// </summary>
        /// <param name="service"></param>
        public HajjTasksController(IHajjTaskService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public HajjTasksController()
        {
            _service = new HajjTaskService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new HajjTask
        /// </summary>
        /// <remarks>
        /// Add a new HajjTask
        /// </remarks>
        /// <param name="hajjTask">HajjTask to add</param>
        /// <returns></returns>
        /// <response code="201">HajjTask 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] HajjTaskDTO hajjTask)
        {
            try
            {
                if (hajjTask == null)
                    return BadRequest("HajjTask Data model is empty.");
                _service.AddHajjTask(hajjTask, userId);
                return Ok("HajjTask Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing HajjTask
        /// </summary>
        /// <param name="hajjTask">HajjTask to update</param>
        /// <returns></returns>
        /// <response code="200">HajjTask updated</response>
        /// <response code="404">HajjTask not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] HajjTaskDTO hajjTask)
        {
            try
            {
                if (hajjTask != null)
                {
                    var result = _service.UpdateHajjTask(hajjTask, userId);
                    if (result)
                        return Ok("HajjTask Updated Successfully.");
                    else
                        return Ok("HajjTask not found.");
                }
                else
                {
                    return BadRequest("HajjTask Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a HajjTask
        /// </summary>
        /// <remarks>
        /// Delete a HajjTask
        /// </remarks>
        /// <param name="hajjTaskId">Id of the HajjTask to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int hajjTaskId)
        {
            try
            {
                if (!int.Equals(hajjTaskId, 0))
                {
                    var response = _service.DeleteHajjTaskById(hajjTaskId, userId);
                    if (response)
                        return Ok("HajjTask has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("HajjTask Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get HajjTask by id
        /// </summary>
        /// <remarks>
        /// Get a HajjTask by id
        /// </remarks>
        /// <param name="hajjTaskId">Id of HajjTask</param>
        /// <returns></returns>
        /// <response code="200">HajjTask found</response>
        /// <response code="404">HajjTask not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int hajjTaskId)
        {
            try
            {
                if (!int.Equals(hajjTaskId, 0))
                {
                    var hajjTask = _service.GetHajjTaskById(hajjTaskId);

                    if (hajjTask != null)
                        return Ok(hajjTask);
                    else
                        return BadRequest("HajjTask not available.");
                }
                else
                {
                    return BadRequest("HajjTask ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all HajjTask
        /// </summary>
        /// <remarks>
        /// Get a list of all HajjTask
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllHajjTask") Route("GetAllHajjTask")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allHajjTasks = _service.GetAllHajjTasks(pageIndex, pageSize, userId);
                return Ok(allHajjTasks);
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
