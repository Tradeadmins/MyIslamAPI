using MyIslamWebApp.DataTransferObjects.PrayerRequest;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{ /// <summary>
  /// PrayerRequests Apis List
  /// </summary>
    [RoutePrefix("api/prayerRequest")]
    public class PrayerRequestsController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IPrayerRequestService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IPrayerRequestService() instance
        /// </summary>
        /// <param name="service"></param>
        public PrayerRequestsController(IPrayerRequestService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public PrayerRequestsController()
        {
            _service = new PrayerRequestService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods        

        /// <summary>
        /// Add new PrayerRequest
        /// </summary>
        /// <remarks>
        /// Add a new PrayerRequest
        /// </remarks>
        /// <param name="prayerRequest">PrayerRequest to add</param>
        /// <returns></returns>
        /// <response code="201">PrayerRequest 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] PrayerRequestDTO prayerRequest)
        {
            try
            {
                if (prayerRequest == null)
                    return BadRequest("Prayer Request Data model is empty.");

                _service.AddPrayerRequest(prayerRequest, userId);
                return Ok("PrayerRequests Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing PrayerRequest
        /// </summary>
        /// <param name="prayerRequest">PrayerRequest to update</param>
        /// <returns></returns>
        /// <response code="200">PrayerRequest updated</response>
        /// <response code="404">PrayerRequest not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] PrayerRequestDTO prayerRequest)
        {
            try
            {
                if (prayerRequest != null)
                {
                    _service.UpdatePrayerRequest(prayerRequest, userId);
                    return Ok("Prayer Request Updated Successfully.");
                }
                else
                {
                    return BadRequest("Prayer Request Data model is Empty");
                }

            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a PrayerRequest
        /// </summary>
        /// <remarks>
        /// Delete a PrayerRequest
        /// </remarks>
        /// <param name="prayerRequestId">Id of the PrayerRequest to delete</param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int prayerRequestId)
        {
            try
            {
                if (!int.Equals(prayerRequestId, 0))
                {
                    var response = _service.DeletePrayerRequestById(prayerRequestId, userId);
                    if (response)
                        return Ok("Record has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("Prayer Request Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get PrayerRequest by id
        /// </summary>
        /// <remarks>
        /// Get a PrayerRequest by id
        /// </remarks>
        /// <param name="prayerRequestId">Id of PrayerRequest</param>
        /// <returns></returns>
        /// <response code="200">PrayerRequest found</response>
        /// <response code="404">PrayerRequest not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int prayerRequestId)
        {
            try
            {
                if (!int.Equals(prayerRequestId, 0))
                {
                    var prayerRequest = _service.GetPrayerRequestById(prayerRequestId);

                    if (prayerRequest != null)
                        return Ok(prayerRequest);
                    else
                        return BadRequest("PrayerRequests data not available.");
                }
                else
                {
                    return BadRequest("The PrayerRequests ID is invalid");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get all PrayerRequest
        /// </summary>
        /// <remarks>
        /// Get a list of all PrayerRequest
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllPrayerRequest") Route("GetAllPrayerRequest")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                //PrayerRequestListDTO prayerRequestList = new PrayerRequestListDTO();
                var allPrayerRequests = _service.GetAllPrayerRequests(pageIndex, pageSize, userId);
                return Ok(allPrayerRequests);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get PrayerRequest by UserId
        /// </summary>
        /// <remarks>
        /// Get a PrayerRequest by UserId
        /// </remarks>
        /// <param name="prayerRequestUserId">UserId of PrayerRequest</param>
        /// <returns></returns>
        /// <response code="200">PrayerRequest found</response>
        /// <response code="404">PrayerRequest not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllPrayerRequestByUserId") Route("GetAllPrayerRequestByUserId")]
        public IHttpActionResult GetAllPrayerRequestByUserId(string prayerRequestUserId)
        {
            try
            {
                if (!string.Equals(prayerRequestUserId, null))
                {
                    var dua = _service.GetAllPrayerRequestByUserId(prayerRequestUserId);

                    if (dua != null)
                        return Ok(dua);
                    else
                        return BadRequest("Prayer Request Not Found");
                }
                else
                {
                    return BadRequest("The PrayerRequest Username is invalid");
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
