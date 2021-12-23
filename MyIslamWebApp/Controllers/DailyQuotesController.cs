using System;
using System.Web.Http;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;
using MyIslamWebApp.DataTransferObjects.VotionOption;
using MyIslamWebApp.DataTransferObjects.DailyQuotes;

namespace MyIslamWebApp.Controllers
{
    /// <summary>
    /// DailyQuotes Apis List
    /// </summary>
    [RoutePrefix("api/dailyQuote")]
    public class DailyQuotesController : ApiController
    {

        #region Properties
        string userId = string.Empty; 
        private readonly IDailyQuoteService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IDailyQuoteService() instance
        /// </summary>
        /// <param name="service"></param>
        public DailyQuotesController(IDailyQuoteService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public DailyQuotesController()
        {
            _service = new DailyQuoteService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods        

        /// <summary>
        /// Add new DailyQuote
        /// </summary>
        /// <remarks>
        /// Add a new DailyQuote
        /// </remarks>
        /// <param name="dailyQuote">DailyQuote to add</param>
        /// <returns></returns>
        /// <response code="201">DailyQuote 
        /// created</response>
       [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] DailyQuoteDTO dailyQuote)
        {
            try
            {
                if (dailyQuote == null)
                    return BadRequest("DailyQuotes Data model is empty.");
                _service.AddDailyQuote(dailyQuote, userId);
                return Ok("DailyQuotes Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing DailyQuote
        /// </summary>
        /// <param name="dailyQuote">DailyQuote to update</param>
        /// <returns></returns>
        /// <response code="200">DailyQuote Updated Successfully.</response>
        /// <response code="404">DailyQuote not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] DailyQuoteDTO dailyQuote)
        {
            try
            {
                if (dailyQuote != null)
                {
                    var result = _service.UpdateDailyQuote(dailyQuote, userId);
                    if (result)
                        return Ok("DailyQuote Updated Successfully.");
                    else
                        return Ok("DailyQuote not found.");
                }
                else
                {
                    return BadRequest("DailyQuote Data model is Empty");
                }

            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a DailyQuote
        /// </summary>
        /// <remarks>
        /// Delete a DailyQuote
        /// </remarks>
        /// <param name="dailyQuoteId">Id of the DailyQuote to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int dailyQuoteId)
        {
            try
            {
                if (!int.Equals(dailyQuoteId, 0))
                {
                    var response = _service.DeleteDailyQuoteById(dailyQuoteId, userId);
                    if (response)
                        return Ok("DailyQuote has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("DailyQuote Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get DailyQuote by Id
        /// </summary>
        /// <remarks>
        /// Get a DailyQuote by Id
        /// </remarks>
        /// <param name="dailyQuoteId">Id of DailyQuote</param>
        /// <returns></returns>
        /// <response code="200">DailyQuote found</response>
        /// <response code="404">DailyQuote not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int dailyQuoteId)
        {
            try
            {
                if (!int.Equals(dailyQuoteId, 0))
                {
                    var dailyQuote = _service.GetDailyQuoteById(dailyQuoteId);

                    if (dailyQuote != null)
                        return Ok(dailyQuote);
                    else
                        return BadRequest("DailyQuotes are not available.");
                }
                else
                {
                    return BadRequest("The DailyQuotes ID is invalid");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get a list of all DailyQuote
        /// </summary>
        /// <remarks>
        /// Get a list of all DailyQuote
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetDailyQuoteBy") Route("GetAllDailyQuote")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allDailyQuotes = _service.GetAllDailyQuotes(pageIndex, pageSize);
                return Ok(allDailyQuotes);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get DailyQuote by languageCode
        /// </summary>
        /// <remarks>
        /// Get a DailyQuote by Language Code
        /// </remarks>
        /// <param name="pageIndex">Language Code</param>     
        /// <param name="pageSize">UTC DateTime</param>
        /// <param name="languageCode">Language Code</param>     
        /// <param name="utcDateTime">UTC DateTime</param>
        /// <returns></returns>
        /// <response code="200">DailyQuote found</response>
        /// <response code="404">DailyQuote not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetDailyQuoteByLang_Date") Route("GetDailyQuoteByLang_Date")]
        public IHttpActionResult GetDailyQuoteByLang_Date(int languageCode, DateTime utcDateTime)
        {
            try
            {
                if (!int.Equals(languageCode, 0))
                {
                    var dailyQuote = _service.GetDailyQuoteByLang_Date(languageCode, utcDateTime);
                    if (dailyQuote != null)
                        return Ok(dailyQuote);
                    else
                        return BadRequest("DailyQuotes Data model is empty.");
                }
                else
                {
                    return BadRequest("The DailyQuotes language code or datetime is invalid");
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
