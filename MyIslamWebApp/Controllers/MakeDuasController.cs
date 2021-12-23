using Microsoft.AspNet.Identity;
using MyIslamWebApp.DataTransferObjects.MakeDua;
using MyIslamWebApp.DataTransferObjects.PrayerRequest;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using System;
using System.Web.Http;

namespace MyIslamWebApp.Controllers
{ /// <summary>
  /// MakeDuas Apis List
  /// </summary>
    [RoutePrefix("api/makeDua")]
    public class MakeDuasController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IMakeDuaService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IMakeDuaService() instance
        /// </summary>
        /// <param name="service"></param>
        public MakeDuasController(IMakeDuaService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public MakeDuasController()
        {
            _service = new MakeDuaService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods

        /// <summary>
        /// Add new MakeDua
        /// </summary>
        /// <remarks>
        /// Add a new MakeDua
        /// </remarks>
        /// <param name="makeDua">MakeDua to add</param>
        /// <returns></returns>
        /// <response code="201">MakeDua 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] MakeDuaDTO makeDua)
        {
            try
            {
                if (makeDua == null)
                    return BadRequest("Make Dua Data model is empty.");
                _service.AddMakeDua(makeDua, userId);
                return Ok("MakeDuas Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }        

        /// <summary>
        /// Get all MakeDua
        /// </summary>
        /// <remarks>
        /// Get a list of all MakeDua
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllMakeDua") Route("GetAllMakeDua")]
        public IHttpActionResult Get()
        {
            try
            {
                var allMakeDuas = _service.GetAllMakeDua();
                return Ok(allMakeDuas);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }


        /// <summary>
        /// Get MakeDua by UserId
        /// </summary>
        /// <remarks>
        /// Get a MakeDua by UserId
        /// </remarks>
        /// <param name="makeDuaUserId">Username of MakeDua</param>
        /// <returns></returns>
        /// <response code="200">MakeDua found</response>
        /// <response code="404">MakeDua not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllMakeDuaByUserId") Route("GetAllMakeDuaByUserId")]
        public IHttpActionResult GetAllMakeDuaByUser(string makeDuaUserId)
        {
            try
            {
                if (!string.Equals(makeDuaUserId, null))
                {
                    var prayerRequest = _service.GetAllMakeDuaByUser(makeDuaUserId);
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
