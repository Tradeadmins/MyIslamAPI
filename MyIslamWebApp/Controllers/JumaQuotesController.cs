using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.JumaQuote;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{
    /// <summary>
    /// JumaQuotes Apis List
    /// </summary>
    [RoutePrefix("api/jumaQuote")]
    public class JumaQuotesController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IJumaQuoteService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IJumaQuoteService() instance
        /// </summary>
        /// <param name="service"></param>
        public JumaQuotesController(IJumaQuoteService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public JumaQuotesController()
        {
            _service = new JumaQuoteService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods        

        /// <summary>
        /// Add new JumaQuote
        /// </summary>
        /// <remarks>
        /// Add a new JumaQuote
        /// </remarks>
        /// <param name="jumaQuote">JumaQuote to add</param>
        /// <returns></returns>
        /// <response code="201">JumaQuote 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] JumaQuoteDTO jumaQuote)
        {
            try
            {
                if (jumaQuote == null)
                    return BadRequest("JumaQuotes Data model is empty.");
                _service.AddJumaQuote(jumaQuote, userId);
                return Ok("JumaQuotes Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing JumaQuote
        /// </summary>
        /// <param name="jumaQuote">JumaQuote to update</param>
        /// <returns></returns>
        /// <response code="200">JumaQuote updated</response>
        /// <response code="404">JumaQuote not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] JumaQuoteDTO jumaQuote)
        {
            try
            {
                if (jumaQuote != null)
                {
                    var result = _service.UpdateJumaQuote(jumaQuote, userId);

                    if (result)
                        return Ok("JumaQuote Updated Successfully.");
                    else
                        return Ok("JumaQuote not found.");
                }
                else
                {
                    return BadRequest("JumaQuote Data model is empty.");
                }

            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a JumaQuote
        /// </summary>
        /// <remarks>
        /// Delete a JumaQuote
        /// </remarks>
        /// <param name="jumaQuoteId">Id of the JumaQuote to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int jumaQuoteId)
        {
            try
            {
                if (!int.Equals(jumaQuoteId, 0))
                {
                    var response = _service.DeleteJumaQuoteById(jumaQuoteId, userId);
                    if (response)
                        return Ok("JumaQuote has Deleted Succefully.");
                    else
                        return BadRequest("JumaQuote has failed to Deleted.");
                }
                else
                {
                    return BadRequest("JumaQuote Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get JumaQuote by id
        /// </summary>
        /// <remarks>
        /// Get a JumaQuote by id
        /// </remarks>
        /// <param name="jumaQuoteId">Id of JumaQuote</param>
        /// <returns></returns>
        /// <response code="200">JumaQuote found</response>
        /// <response code="404">JumaQuote not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int jumaQuoteId)
        {
            try
            {
                if (!int.Equals(jumaQuoteId, 0))
                {
                    var jumaQuote = _service.GetJumaQuoteById(jumaQuoteId);

                    if (jumaQuote != null)
                        return Ok(jumaQuote);
                    else
                        return BadRequest("JumaQuote are not available.");
                }
                else
                {
                    return BadRequest("The JumaQuotes ID is invalid");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get all JumaQuote
        /// </summary>
        /// <remarks>
        /// Get a list of all JumaQuote
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllJumaQuote") Route("GetAllJumaQuote")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allJumaQuotes = _service.GetAllJumaQuotes(pageIndex, pageSize);
                return Ok(allJumaQuotes);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get JumaQuote by languageCode
        /// </summary>
        /// <remarks>
        /// Get a JumaQuote by Language Code
        /// </remarks>
        /// <param name="languageCode">Language Code</param>     
        /// <param name="utcDateTime">UTC DateTime</param>
        /// <returns></returns>
        /// <response code="200">JumaQuote found</response>
        /// <response code="404">JumaQuote not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetJumaQuoteByLang_Date") Route("GetJumaQuoteByLang_Date")]
        public IHttpActionResult GetByLng_DateTime(int languageCode, DateTime utcDateTime)
        {
            try
            {
                if (!int.Equals(languageCode, 0))
                {
                    var jumaQuote = _service.GetJumaQuoteByLang_Date(languageCode, utcDateTime);
                    if (jumaQuote != null)
                        return Ok(jumaQuote);
                    else
                        return BadRequest("JumaQuotes Data model is empty.");
                }
                else
                {
                    return BadRequest("The JumaQuotes language code or datetime is invalid");
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
