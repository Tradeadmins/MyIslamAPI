using Microsoft.AspNet.Identity;
using MyIslamWebApp.DataTransferObjects.HajjTaskComplete;
using MyIslamWebApp.DataTransferObjects.PrayerRequest;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using System;
using System.Web.Http;

namespace MyIslamWebApp.Controllers
{ /// <summary>
  /// HajjTaskCompletes Apis List
  /// </summary>
    [RoutePrefix("api/hajjTaskComplete")]
    public class HajjTaskCompletesController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IHajjTaskCompleteService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IHajjTaskCompleteService() instance
        /// </summary>
        /// <param name="service"></param>
        public HajjTaskCompletesController(IHajjTaskCompleteService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public HajjTaskCompletesController()
        {
            _service = new HajjTaskCompleteService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods

        /// <summary>
        /// Add new HajjTaskComplete
        /// </summary>
        /// <remarks>
        /// Add a new HajjTaskComplete
        /// </remarks>
        /// <param name="hajjTaskComplete">HajjTaskComplete to add</param>
        /// <returns></returns>
        /// <response code="201">HajjTaskComplete 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] HajjTaskCompleteDTO hajjTaskComplete)
        {
            try
            {
                if (hajjTaskComplete == null)
                    return BadRequest("Make Dua Data model is empty.");
                _service.AddHajjTaskComplete(hajjTaskComplete, userId);
                return Ok("HajjTaskCompletes Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a HajjTaskComplete
        /// </summary>
        /// <remarks>
        /// Delete a HajjTaskComplete
        /// </remarks>
        /// <param name="hajjTaskCompleteId">Id of the HajjTaskComplete to delete</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int hajjTaskCompleteId)
        {
            try
            {
                if (!int.Equals(hajjTaskCompleteId, 0))
                {
                    var response = _service.DeleteHajjTaskComplete(hajjTaskCompleteId);
                    if (response)
                        return Ok("HajjTaskComplete has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("HajjTaskComplete Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }


        /// <summary>
        /// Get all HajjTaskComplete
        /// </summary>
        /// <remarks>
        /// Get a list of all HajjTaskComplete
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllHajjTaskComplete") Route("GetAllHajjTaskComplete")]
        public IHttpActionResult Get()
        {
            try
            {
                var allHajjTaskCompletes = _service.GetAllHajjTaskComplete();
                return Ok(allHajjTaskCompletes);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get HajjTaskComplete by UserId
        /// </summary>
        /// <remarks>
        /// Get a HajjTaskComplete by UserId
        /// </remarks>
        /// <param name="hajjTaskCompleteUserId">Username of HajjTaskComplete</param>
        /// <returns></returns>
        /// <response code="200">HajjTaskComplete found</response>
        /// <response code="404">HajjTaskComplete not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllHajjTaskCompleteByUserId") Route("GetAllHajjTaskCompleteByUserId")]
        public IHttpActionResult GetAllHajjTaskCompleteByUser(string hajjTaskCompleteUserId)
        {
            try
            {
                if (!string.Equals(hajjTaskCompleteUserId, null))
                {
                    var prayerRequest = _service.GetAllHajjTaskCompleteByUser(hajjTaskCompleteUserId);
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
