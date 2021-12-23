using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.UmrahGuide;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// UmrahGuides Apis List
 /// </summary>
    [RoutePrefix("api/umrahGuide")]
    public class UmrahGuidesController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IUmrahGuideService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IUmrahGuideService() instance
        /// </summary>
        /// <param name="service"></param>
        public UmrahGuidesController(IUmrahGuideService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public UmrahGuidesController()
        {
            _service = new UmrahGuideService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new UmrahGuide
        /// </summary>
        /// <remarks>
        /// Add a new UmrahGuide
        /// </remarks>
        /// <param name="umrahGuide">UmrahGuide to add</param>
        /// <returns></returns>
        /// <response code="201">UmrahGuide 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] UmrahGuideDTO umrahGuide)
        {
            try
            {
                if (umrahGuide == null)
                    return BadRequest("UmrahGuide Data model is empty.");
                _service.AddUmrahGuide(umrahGuide, userId);
                return Ok("UmrahGuide Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing UmrahGuide
        /// </summary>
        /// <param name="umrahGuide">UmrahGuide to update</param>
        /// <returns></returns>
        /// <response code="200">UmrahGuide updated</response>
        /// <response code="404">UmrahGuide not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] UmrahGuideDTO umrahGuide)
        {
            try
            {
                if (umrahGuide != null)
                {
                    var result = _service.UpdateUmrahGuide(umrahGuide, userId);
                    if (result)
                        return Ok("UmrahGuide Updated Successfully.");
                    else
                        return Ok("UmrahGuide not found.");
                }
                else
                {
                    return BadRequest("UmrahGuide Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a UmrahGuide
        /// </summary>
        /// <remarks>
        /// Delete a UmrahGuide
        /// </remarks>
        /// <param name="umrahGuideId">Id of the UmrahGuide to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int umrahGuideId)
        {
            try
            {
                if (!int.Equals(umrahGuideId, 0))
                {
                    var response = _service.DeleteUmrahGuideById(umrahGuideId, userId);
                    if (response)
                        return Ok("UmrahGuide has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("UmrahGuide Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get UmrahGuide by id
        /// </summary>
        /// <remarks>
        /// Get a UmrahGuide by id
        /// </remarks>
        /// <param name="umrahGuideId">Id of UmrahGuide</param>
        /// <returns></returns>
        /// <response code="200">UmrahGuide found</response>
        /// <response code="404">UmrahGuide not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int umrahGuideId)
        {
            try
            {
                if (!int.Equals(umrahGuideId, 0))
                {
                    var umrahGuide = _service.GetUmrahGuideById(umrahGuideId);

                    if (umrahGuide != null)
                        return Ok(umrahGuide);
                    else
                        return BadRequest("UmrahGuide not available.");
                }
                else
                {
                    return BadRequest("UmrahGuide ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all UmrahGuide
        /// </summary>
        /// <remarks>
        /// Get a list of all UmrahGuide
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllUmrahGuide") Route("GetAllUmrahGuide")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allUmrahGuides = _service.GetAllUmrahGuides(pageIndex, pageSize, userId);
                return Ok(allUmrahGuides);
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
