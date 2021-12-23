using System;
using System.Web.Http;
using MyIslamWebApp.Filter;
using NLog;
using Microsoft.AspNet.Identity;
using MyIslamWebApp.Service;
using MyIslamWebApp.DataTransferObjects.Donation;

namespace MyIslamWebApp.Controllers
{
    /// <summary>
    /// Donations Apis List
    /// </summary>
    [RoutePrefix("api/donation")]
    public class DonationsController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IDonationService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IDonationService() instance
        /// </summary>
        /// <param name="service"></param>
        public DonationsController(IDonationService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public DonationsController()
        {
            _service = new DonationService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods        

        /// <summary>
        /// Add new Donation
        /// </summary>
        /// <remarks>
        /// Add a new Donation
        /// </remarks>
        /// <param name="donation">Donation to add</param>
        /// <returns></returns>
        /// <response code="201">Donation 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] DonationDTO donation)
        {
            try
            {
                if (donation == null)
                    return BadRequest("Donations Data model is empty.");
                _service.AddDonation(donation, userId);
                return Ok("Donations Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing Donation
        /// </summary>
        /// <param name="donation">Donation to update</param>
        /// <returns></returns>
        /// <response code="200">Donation Updated Successfully.</response>
        /// <response code="404">Donation not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] DonationDTO donation)
        {
            try
            {
                if (donation != null)
                {
                    var result = _service.UpdateDonation(donation, userId);
                    if (result)
                        return Ok("Donation Updated Successfully.");
                    else
                        return Ok("Donation not found.");
                }
                else
                {
                    return BadRequest("Donation Data model is Empty");
                }

            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a Donation
        /// </summary>
        /// <remarks>
        /// Delete a Donation
        /// </remarks>
        /// <param name="donationId">Id of the Donation to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int donationId)
        {
            try
            {
                if (!int.Equals(donationId, 0))
                {
                    var response = _service.DeleteDonationById(donationId, userId);
                    if (response)
                        return Ok("Donation has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("Donation Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get Donation by Id
        /// </summary>
        /// <remarks>
        /// Get a Donation by Id
        /// </remarks>
        /// <param name="donationId">Id of Donation</param>
        /// <returns></returns>
        /// <response code="200">Donation found</response>
        /// <response code="404">Donation not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int donationId)
        {
            try
            {
                if (!int.Equals(donationId, 0))
                {
                    var donation = _service.GetDonationById(donationId);

                    if (donation != null)
                        return Ok(donation);
                    else
                        return BadRequest("Donations are not available.");
                }
                else
                {
                    return BadRequest("The Donations ID is invalid");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get a list of all Donation
        /// </summary>
        /// <remarks>
        /// Get a list of all Donation
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllDonationAmount") Route("GetAllDonationAmount")]
        public IHttpActionResult Get()
        {
            try
            {
                var allDonations = _service.GetAllDonationAmounts();
                return Ok(allDonations);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get Donation by UserId
        /// </summary>
        /// <remarks>
        /// Get a Donation by UserId
        /// </remarks>
        /// <param name="userId">UserId of Donation</param>
        /// <returns></returns>
        /// <response code="200">Donation found</response>
        /// <response code="404">Donation not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllDonationByUserId") Route("GetAllDonationByUserId")]
        public IHttpActionResult GetAllDonationByUserId(string userId)
        {
            try
            {
                if (!string.Equals(userId, null))
                {
                    var donation = _service.GetAllDonationByUserId(userId);

                    if (donation != null)
                        return Ok(donation);
                    else
                        return BadRequest("Custom Dua Not Found");
                }
                else
                {
                    return BadRequest("The userid is invalid");
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
