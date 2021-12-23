using System;
using System.Web.Http;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;
using MyIslamWebApp.DataTransferObjects.QuranTranslate;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// QuranTranslates Apis List
 /// </summary>
    [RoutePrefix("api/quranTranslate")]
    public class QuranTranslatesController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IQuranTranslateService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IIQuranTranslatesService() instance
        /// </summary>
        /// <param name="service"></param>
        public QuranTranslatesController(IQuranTranslateService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public QuranTranslatesController()
        {
            _service = new QuranTranslateService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new QuranTranslates
        /// </summary>
        /// <remarks>
        /// Add a new QuranTranslates
        /// </remarks>
        /// <param name="quranTranslate">QuranTranslates to add</param>
        /// <returns></returns>
        /// <response code="201">QuranTranslates 
        /// created</response>
        [AllowAnonymous]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] QuranTranslateDTO quranTranslate)
        {
            try
            {
                if (quranTranslate == null)
                    return BadRequest("QuranTranslates Data model is empty.");
                _service.AddQuranTranslate(quranTranslate, userId);
                return Ok("QuranTranslates Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing QuranTranslates
        /// </summary>
        /// <param name="quranTranslate">QuranTranslates to update</param>
        /// <returns></returns>
        /// <response code="200">QuranTranslates updated</response>
        /// <response code="404">QuranTranslates not found</response>
        [AllowAnonymous]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] QuranTranslateDTO quranTranslate)
        {
            try
            {
                if (quranTranslate != null)
                {
                    var result = _service.UpdateQuranTranslate(quranTranslate, userId);
                    if (result)
                        return Ok("QuranTranslates Updated Successfully.");
                    else
                        return Ok("QuranTranslates not found.");
                }
                else
                {
                    return BadRequest("QuranTranslates Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a QuranTranslates
        /// </summary>
        /// <remarks>
        /// Delete a QuranTranslates
        /// </remarks>
        /// <param name="quranTranslateId">Id of the QuranTranslates to delete</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int quranTranslateId)
        {
            try
            {
                if (!int.Equals(quranTranslateId, 0))
                {
                    var response = _service.DeleteQuranTranslateById(quranTranslateId, userId);
                    if (response)
                        return Ok("QuranTranslates has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("QuranTranslates Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get QuranTranslates by id
        /// </summary>
        /// <remarks>
        /// Get a QuranTranslates by id
        /// </remarks>
        /// <param name="quranTranslateId">Id of QuranTranslates</param>
        /// <returns></returns>
        /// <response code="200">QuranTranslates found</response>
        /// <response code="404">QuranTranslates not found</response>
        [AllowAnonymous]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int quranTranslateId)
        {
            try
            {
                if (!int.Equals(quranTranslateId, 0))
                {
                    var quranTranslate = _service.GetQuranTranslateById(quranTranslateId);

                    if (quranTranslate != null)
                        return Ok(quranTranslate);
                    else
                        return BadRequest("QuranTranslates not available.");
                }
                else
                {
                    return BadRequest("QuranTranslates ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all QuranTranslates
        /// </summary>
        /// <remarks>
        /// Get a list of all QuranTranslates
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [AllowAnonymous]
        [HttpGet]
        [ActionName("GetAllQuranTranslates") Route("GetAllQuranTranslates")]
        public IHttpActionResult Get()
        {
            try
            {
                var allQuranTranslates = _service.GetAllQuranTranslate();
                return Ok(allQuranTranslates);
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
