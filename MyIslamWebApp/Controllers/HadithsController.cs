using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.Hadith;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// Hadiths Apis List
 /// </summary>
    [RoutePrefix("api/hadith")]
    public class HadithsController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IHadithService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IHadithService() instance
        /// </summary>
        /// <param name="service"></param>
        public HadithsController(IHadithService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public HadithsController()
        {
            _service = new HadithService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new Hadith
        /// </summary>
        /// <remarks>
        /// Add a new Hadith
        /// </remarks>
        /// <param name="hadith">Hadith to add</param>
        /// <returns></returns>
        /// <response code="201">Hadith 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] HadithDTO hadith)
        {
            try
            {
                if (hadith == null)
                    return BadRequest("Hadith Data model is empty.");
                _service.AddHadith(hadith, userId);
                return Ok("Hadith Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing Hadith
        /// </summary>
        /// <param name="hadith">Hadith to update</param>
        /// <returns></returns>
        /// <response code="200">Hadith updated</response>
        /// <response code="404">Hadith not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] HadithDTO hadith)
        {
            try
            {
                if (hadith != null)
                {
                    var result = _service.UpdateHadith(hadith, userId);
                    if (result)
                        return Ok("Hadith Updated Successfully.");
                    else
                        return Ok("Hadith not found.");
                }
                else
                {
                    return BadRequest("Hadith Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a Hadith
        /// </summary>
        /// <remarks>
        /// Delete a Hadith
        /// </remarks>
        /// <param name="hadithId">Id of the Hadith to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int hadithId)
        {
            try
            {
                if (!int.Equals(hadithId, 0))
                {
                    var response = _service.DeleteHadithById(hadithId, userId);
                    if (response)
                        return Ok("Hadith has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("Hadith Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get Hadith by id
        /// </summary>
        /// <remarks>
        /// Get a Hadith by id
        /// </remarks>
        /// <param name="hadithId">Id of Hadith</param>
        /// <returns></returns>
        /// <response code="200">Hadith found</response>
        /// <response code="404">Hadith not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int hadithId)
        {
            try
            {
                if (!int.Equals(hadithId, 0))
                {
                    var hadith = _service.GetHadithById(hadithId);

                    if (hadith != null)
                        return Ok(hadith);
                    else
                        return BadRequest("Hadith not available.");
                }
                else
                {
                    return BadRequest("Hadith ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all Hadith
        /// </summary>
        /// <remarks>
        /// Get a list of all Hadith
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllHadith") Route("GetAllHadith")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allHadiths = _service.GetAllHadiths(pageIndex, pageSize);
                return Ok(allHadiths);
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
