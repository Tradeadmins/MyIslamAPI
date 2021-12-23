using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.DonationCategory;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// DonationCategory Apis List
 /// </summary>
    [RoutePrefix("api/donationCategory")]
    public class DonationCategoryController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IDonationCategoryService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IIDonationCategoryService() instance
        /// </summary>
        /// <param name="service"></param>
        public DonationCategoryController(IDonationCategoryService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public DonationCategoryController()
        {
            _service = new DonationCategoryService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new DonationCategory
        /// </summary>
        /// <remarks>
        /// Add a new DonationCategory
        /// </remarks>
        /// <param name="donationCategory">DonationCategory to add</param>
        /// <returns></returns>
        /// <response code="201">DonationCategory 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] DonationCategoryDTO donationCategory)
        {
            try
            {
                if (donationCategory == null)
                    return BadRequest("DonationCategory Data model is empty.");
                _service.AddDonationCategory(donationCategory, userId);
                return Ok("DonationCategory Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing DonationCategory
        /// </summary>
        /// <param name="donationCategory">DonationCategory to update</param>
        /// <returns></returns>
        /// <response code="200">DonationCategory updated</response>
        /// <response code="404">DonationCategory not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] DonationCategoryDTO donationCategory)
        {
            try
            {
                if (donationCategory != null)
                {
                    var result = _service.UpdateDonationCategory(donationCategory, userId);
                    if (result)
                        return Ok("DonationCategory Updated Successfully.");
                    else
                        return Ok("DonationCategory not found.");
                }
                else
                {
                    return BadRequest("DonationCategory Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a DonationCategory
        /// </summary>
        /// <remarks>
        /// Delete a DonationCategory
        /// </remarks>
        /// <param name="donationCategoryId">Id of the DonationCategory to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int donationCategoryId)
        {
            try
            {
                if (!int.Equals(donationCategoryId, 0))
                {
                    var response = _service.DeleteDonationCategoryById(donationCategoryId, userId);
                    if (response)
                        return Ok("DonationCategory has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("DonationCategory Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get DonationCategory by id
        /// </summary>
        /// <remarks>
        /// Get a DonationCategory by id
        /// </remarks>
        /// <param name="donationCategoryId">Id of DonationCategory</param>
        /// <returns></returns>
        /// <response code="200">DonationCategory found</response>
        /// <response code="404">DonationCategory not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int donationCategoryId)
        {
            try
            {
                if (!int.Equals(donationCategoryId, 0))
                {
                    var donationCategory = _service.GetDonationCategoryById(donationCategoryId);

                    if (donationCategory != null)
                        return Ok(donationCategory);
                    else
                        return BadRequest("DonationCategory not available.");
                }
                else
                {
                    return BadRequest("DonationCategory ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all DonationCategory
        /// </summary>
        /// <remarks>
        /// Get a list of all DonationCategory
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllDonationCategory") Route("GetAllDonationCategory")]
        public IHttpActionResult Get()
        {
            try
            {
                var allDonationCategory = _service.GetAllDonationCategory();
                return Ok(allDonationCategory);
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
