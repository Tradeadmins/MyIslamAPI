using Microsoft.AspNet.Identity;
using MyIslamWebApp.DataTransferObjects.UmrahGuideComplete;
using MyIslamWebApp.DataTransferObjects.PrayerRequest;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using System;
using System.Web.Http;

namespace MyIslamWebApp.Controllers
{ /// <summary>
  /// UmrahGuideCompletes Apis List
  /// </summary>
    [RoutePrefix("api/umrahGuideComplete")]
    public class UmrahGuideCompletesController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IUmrahGuideCompleteService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IUmrahGuideCompleteService() instance
        /// </summary>
        /// <param name="service"></param>
        public UmrahGuideCompletesController(IUmrahGuideCompleteService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public UmrahGuideCompletesController()
        {
            _service = new UmrahGuideCompleteService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods

        /// <summary>
        /// Add new UmrahGuideComplete
        /// </summary>
        /// <remarks>
        /// Add a new UmrahGuideComplete
        /// </remarks>
        /// <param name="umrahGuideComplete">UmrahGuideComplete to add</param>
        /// <returns></returns>
        /// <response code="201">UmrahGuideComplete 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] UmrahGuideCompleteDTO umrahGuideComplete)
        {
            try
            {
                if (umrahGuideComplete == null)
                    return BadRequest("Make Dua Data model is empty.");
                _service.AddUmrahGuideComplete(umrahGuideComplete, userId);
                return Ok("UmrahGuideCompletes Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a UmrahGuideComplete
        /// </summary>
        /// <remarks>
        /// Delete a UmrahGuideComplete
        /// </remarks>
        /// <param name="umrahGuideCompleteId">Id of the UmrahGuideComplete to delete</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int umrahGuideCompleteId)
        {
            try
            {
                if (!int.Equals(umrahGuideCompleteId, 0))
                {
                    var response = _service.DeleteUmrahGuideComplete(umrahGuideCompleteId);
                    if (response)
                        return Ok("UmrahGuideComplete has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("UmrahGuideComplete Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get all UmrahGuideComplete
        /// </summary>
        /// <remarks>
        /// Get a list of all UmrahGuideComplete
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllUmrahGuideComplete") Route("GetAllUmrahGuideComplete")]
        public IHttpActionResult Get()
        {
            try
            {
                var allUmrahGuideCompletes = _service.GetAllUmrahGuideComplete();
                return Ok(allUmrahGuideCompletes);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get UmrahGuideComplete by UserId
        /// </summary>
        /// <remarks>
        /// Get a UmrahGuideComplete by UserId
        /// </remarks>
        /// <param name="umrahGuideCompleteUserId">Username of UmrahGuideComplete</param>
        /// <returns></returns>
        /// <response code="200">UmrahGuideComplete found</response>
        /// <response code="404">UmrahGuideComplete not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllUmrahGuideCompleteByUserId") Route("GetAllUmrahGuideCompleteByUserId")]
        public IHttpActionResult GetAllUmrahGuideCompleteByUser(string umrahGuideCompleteUserId)
        {
            try
            {
                if (!string.Equals(umrahGuideCompleteUserId, null))
                {
                    var prayerRequest = _service.GetAllUmrahGuideCompleteByUser(umrahGuideCompleteUserId);
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
