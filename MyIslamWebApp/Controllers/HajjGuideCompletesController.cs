using Microsoft.AspNet.Identity;
using MyIslamWebApp.DataTransferObjects.HajjGuideComplete;
using MyIslamWebApp.DataTransferObjects.PrayerRequest;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using System;
using System.Web.Http;

namespace MyIslamWebApp.Controllers
{ /// <summary>
  /// HajjGuideCompletes Apis List
  /// </summary>
    [RoutePrefix("api/hajjGuideComplete")]
    public class HajjGuideCompletesController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IHajjGuideCompleteService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IHajjGuideCompleteService() instance
        /// </summary>
        /// <param name="service"></param>
        public HajjGuideCompletesController(IHajjGuideCompleteService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public HajjGuideCompletesController()
        {
            _service = new HajjGuideCompleteService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods

        /// <summary>
        /// Add new HajjGuideComplete
        /// </summary>
        /// <remarks>
        /// Add a new HajjGuideComplete
        /// </remarks>
        /// <param name="hajjGuideComplete">HajjGuideComplete to add</param>
        /// <returns></returns>
        /// <response code="201">HajjGuideComplete 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] HajjGuideCompleteDTO hajjGuideComplete)
        {
            try
            {
                if (hajjGuideComplete == null)
                    return BadRequest("Make Dua Data model is empty.");
                _service.AddHajjGuideComplete(hajjGuideComplete, userId);
                return Ok("HajjGuideCompletes Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a HajjGuideComplete
        /// </summary>
        /// <remarks>
        /// Delete a HajjGuideComplete
        /// </remarks>
        /// <param name="hajjGuideCompleteId">Id of the HajjGuideComplete to delete</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int hajjGuideCompleteId)
        {
            try
            {
                if (!int.Equals(hajjGuideCompleteId, 0))
                {
                    var response = _service.DeleteHajjGuideComplete(hajjGuideCompleteId);
                    if (response)
                        return Ok("HajjGuideComplete has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("HajjGuideComplete Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get all HajjGuideComplete
        /// </summary>
        /// <remarks>
        /// Get a list of all HajjGuideComplete
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllHajjGuideComplete") Route("GetAllHajjGuideComplete")]
        public IHttpActionResult Get()
        {
            try
            {
                var allHajjGuideCompletes = _service.GetAllHajjGuideComplete();
                return Ok(allHajjGuideCompletes);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get HajjGuideComplete by UserId
        /// </summary>
        /// <remarks>
        /// Get a HajjGuideComplete by UserId
        /// </remarks>
        /// <param name="hajjGuideCompleteUserId">Username of HajjGuideComplete</param>
        /// <returns></returns>
        /// <response code="200">HajjGuideComplete found</response>
        /// <response code="404">HajjGuideComplete not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllHajjGuideCompleteByUserId") Route("GetAllHajjGuideCompleteByUserId")]
        public IHttpActionResult GetAllHajjGuideCompleteByUser(string hajjGuideCompleteUserId)
        {
            try
            {
                if (!string.Equals(hajjGuideCompleteUserId, null))
                {
                    var prayerRequest = _service.GetAllHajjGuideCompleteByUser(hajjGuideCompleteUserId);
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
