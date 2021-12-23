using Microsoft.AspNet.Identity;
using MyIslamWebApp.DataTransferObjects.UmrahTaskComplete;
using MyIslamWebApp.DataTransferObjects.PrayerRequest;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using System;
using System.Web.Http;

namespace MyIslamWebApp.Controllers
{ /// <summary>
  /// UmrahTaskCompletes Apis List
  /// </summary>
    [RoutePrefix("api/umrahTaskComplete")]
    public class UmrahTaskCompletesController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IUmrahTaskCompleteService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IUmrahTaskCompleteService() instance
        /// </summary>
        /// <param name="service"></param>
        public UmrahTaskCompletesController(IUmrahTaskCompleteService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public UmrahTaskCompletesController()
        {
            _service = new UmrahTaskCompleteService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods

        /// <summary>
        /// Add new UmrahTaskComplete
        /// </summary>
        /// <remarks>
        /// Add a new UmrahTaskComplete
        /// </remarks>
        /// <param name="umrahTaskComplete">UmrahTaskComplete to add</param>
        /// <returns></returns>
        /// <response code="201">UmrahTaskComplete 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] UmrahTaskCompleteDTO umrahTaskComplete)
        {
            try
            {
                if (umrahTaskComplete == null)
                    return BadRequest("Make Dua Data model is empty.");
                _service.AddUmrahTaskComplete(umrahTaskComplete, userId);
                return Ok("UmrahTaskCompletes Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a UmrahTaskComplete
        /// </summary>
        /// <remarks>
        /// Delete a UmrahTaskComplete
        /// </remarks>
        /// <param name="umrahTaskCompleteId">Id of the UmrahTaskComplete to delete</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int umrahTaskCompleteId)
        {
            try
            {
                if (!int.Equals(umrahTaskCompleteId, 0))
                {
                    var response = _service.DeleteUmrahTaskComplete(umrahTaskCompleteId);
                    if (response)
                        return Ok("UmrahTaskComplete has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("UmrahTaskComplete Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get all UmrahTaskComplete
        /// </summary>
        /// <remarks>
        /// Get a list of all UmrahTaskComplete
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllUmrahTaskComplete") Route("GetAllUmrahTaskComplete")]
        public IHttpActionResult Get()
        {
            try
            {
                var allUmrahTaskCompletes = _service.GetAllUmrahTaskComplete();
                return Ok(allUmrahTaskCompletes);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get UmrahTaskComplete by UserId
        /// </summary>
        /// <remarks>
        /// Get a UmrahTaskComplete by UserId
        /// </remarks>
        /// <param name="umrahTaskCompleteUserId">Username of UmrahTaskComplete</param>
        /// <returns></returns>
        /// <response code="200">UmrahTaskComplete found</response>
        /// <response code="404">UmrahTaskComplete not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllUmrahTaskCompleteByUserId") Route("GetAllUmrahTaskCompleteByUserId")]
        public IHttpActionResult GetAllUmrahTaskCompleteByUser(string umrahTaskCompleteUserId)
        {
            try
            {
                if (!string.Equals(umrahTaskCompleteUserId, null))
                {
                    var prayerRequest = _service.GetAllUmrahTaskCompleteByUser(umrahTaskCompleteUserId);
                    if (prayerRequest != null)
                        return Ok(prayerRequest);
                    else
                        return BadRequest("Data not found with this particular information");
                }
                else
                {
                    return BadRequest("The UserId is invalid");
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
