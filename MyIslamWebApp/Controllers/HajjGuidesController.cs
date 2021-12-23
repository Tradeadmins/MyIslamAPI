using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.HajjGuide;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// HajjGuides Apis List
 /// </summary>
    [RoutePrefix("api/hajjGuide")]
    public class HajjGuidesController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IHajjGuideService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IHajjGuideService() instance
        /// </summary>
        /// <param name="service"></param>
        public HajjGuidesController(IHajjGuideService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public HajjGuidesController()
        {
            _service = new HajjGuideService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new HajjGuide
        /// </summary>
        /// <remarks>
        /// Add a new HajjGuide
        /// </remarks>
        /// <param name="hajjGuide">HajjGuide to add</param>
        /// <returns></returns>
        /// <response code="201">HajjGuide 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] HajjGuideDTO hajjGuide)
        {
            try
            {
                if (hajjGuide == null)
                    return BadRequest("HajjGuide Data model is empty.");
                _service.AddHajjGuide(hajjGuide, userId);
                return Ok("HajjGuide Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing HajjGuide
        /// </summary>
        /// <param name="hajjGuide">HajjGuide to update</param>
        /// <returns></returns>
        /// <response code="200">HajjGuide updated</response>
        /// <response code="404">HajjGuide not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] HajjGuideDTO hajjGuide)
        {
            try
            {
                if (hajjGuide != null)
                {
                    var result = _service.UpdateHajjGuide(hajjGuide, userId);
                    if (result)
                        return Ok("HajjGuide Updated Successfully.");
                    else
                        return Ok("HajjGuide not found.");
                }
                else
                {
                    return BadRequest("HajjGuide Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a HajjGuide
        /// </summary>
        /// <remarks>
        /// Delete a HajjGuide
        /// </remarks>
        /// <param name="hajjGuideId">Id of the HajjGuide to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int hajjGuideId)
        {
            try
            {
                if (!int.Equals(hajjGuideId, 0))
                {
                    var response = _service.DeleteHajjGuideById(hajjGuideId, userId);
                    if (response)
                        return Ok("HajjGuide has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("HajjGuide Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get HajjGuide by id
        /// </summary>
        /// <remarks>
        /// Get a HajjGuide by id
        /// </remarks>
        /// <param name="hajjGuideId">Id of HajjGuide</param>
        /// <returns></returns>
        /// <response code="200">HajjGuide found</response>
        /// <response code="404">HajjGuide not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int hajjGuideId)
        {
            try
            {
                if (!int.Equals(hajjGuideId, 0))
                {
                    var hajjGuide = _service.GetHajjGuideById(hajjGuideId);

                    if (hajjGuide != null)
                        return Ok(hajjGuide);
                    else
                        return BadRequest("HajjGuide not available.");
                }
                else
                {
                    return BadRequest("HajjGuide ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all HajjGuide
        /// </summary>
        /// <remarks>
        /// Get a list of all HajjGuide
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllHajjGuide") Route("GetAllHajjGuide")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allHajjGuides = _service.GetAllHajjGuides(pageIndex, pageSize, userId);
                return Ok(allHajjGuides);
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
