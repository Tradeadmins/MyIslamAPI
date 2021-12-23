using System;
using System.Web.Http;
using MyIslamWebApp.Filter;
using NLog;
using Microsoft.AspNet.Identity;
using MyIslamWebApp.Service;
using MyIslamWebApp.DataTransferObjects.InAppPurchase;
using MyIslamWebApp.DataTransferObjects.Donation;
using MyIslamWebApp.DataContext;

namespace MyIslamWebApp.Controllers
{
    /// <summary>
    /// InAppPurchases Apis List
    /// </summary>
    [RoutePrefix("api/inAppPurchase")]
    public class InAppPurchasesController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IInAppPurchaseService _service;
        private readonly IDonationService _donationService;
        private readonly IDonationCategoryService _categoryService;
        public static Logger logger = LogManager.GetCurrentClassLogger();      
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IInAppPurchaseService() instance
        /// </summary>
        /// <param name="service"></param>
        public InAppPurchasesController(IInAppPurchaseService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public InAppPurchasesController()
        {
            _service = new InAppPurchaseService();
            _donationService = new DonationService();
            _categoryService = new DonationCategoryService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods        

        /// <summary>
        /// Add new InAppPurchase
        /// </summary>
        /// <remarks>
        /// Add a new InAppPurchase
        /// </remarks>
        /// <param name="inAppPurchase">InAppPurchase to add</param>
        /// <param name="userId">User Id of the User asking InAppPurchase to add</param>
        /// <returns></returns>
        /// <response code="201">InAppPurchase 
        /// created</response>
        [AllowAnonymous]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] InAppPurchaseDTO inAppPurchase)
        {
            try
            {
                if (inAppPurchase == null)
                    return BadRequest("InAppPurchases Data model is empty.");
                _service.AddInAppPurchase(inAppPurchase, userId);

                DonationDTO donationDTO = new DonationDTO();
                donationDTO.DonationCategoryId = 1;
                donationDTO.DonationAmount = inAppPurchase.InAppPurchaseUserAmount;
                donationDTO.DonationLocalAmount = inAppPurchase.InAppPurchaseUserLocalAmount;
                donationDTO.DonationLocalCurrencyType = inAppPurchase.InAppPurchaseUserLocalCurrencyType;

                _donationService.AddDonation(donationDTO , userId);

                return Ok("InAppPurchases Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing InAppPurchase
        /// </summary>
        /// <param name="inAppPurchase">InAppPurchase to update</param>
        /// <returns></returns>
        /// <response code="200">InAppPurchase Updated Successfully.</response>
        /// <response code="404">InAppPurchase not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] InAppPurchaseDTO inAppPurchase)
        {
            try
            {
                if (inAppPurchase != null)
                {
                    var result = _service.UpdateInAppPurchase(inAppPurchase, userId);
                    if (result)
                        return Ok("InAppPurchase Updated Successfully.");
                    else
                        return Ok("InAppPurchase not found.");
                }
                else
                {
                    return BadRequest("InAppPurchase Data model is Empty");
                }

            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a InAppPurchase
        /// </summary>
        /// <remarks>
        /// Delete a InAppPurchase
        /// </remarks>
        /// <param name="inAppPurchaseId">Id of the InAppPurchase to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int inAppPurchaseId)
        {
            try
            {
                if (!int.Equals(inAppPurchaseId, 0))
                {
                    var response = _service.DeleteInAppPurchaseById(inAppPurchaseId, userId);
                    if (response)
                        return Ok("InAppPurchase has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("InAppPurchase Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get InAppPurchase by Id
        /// </summary>
        /// <remarks>
        /// Get a InAppPurchase by Id
        /// </remarks>
        /// <param name="inAppPurchaseId">Id of InAppPurchase</param>
        /// <returns></returns>
        /// <response code="200">InAppPurchase found</response>
        /// <response code="404">InAppPurchase not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int inAppPurchaseId)
        {
            try
            {
                if (!int.Equals(inAppPurchaseId, 0))
                {
                    var inAppPurchase = _service.GetInAppPurchaseById(inAppPurchaseId);

                    if (inAppPurchase != null)
                        return Ok(inAppPurchase);
                    else
                        return BadRequest("InAppPurchases are not available.");
                }
                else
                {
                    return BadRequest("The InAppPurchases ID is invalid");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get a list of all InAppPurchase
        /// </summary>
        /// <remarks>
        /// Get a list of all InAppPurchase
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetInAppPurchaseBy") Route("GetAllInAppPurchase")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allInAppPurchases = _service.GetAllInAppPurchases(pageIndex, pageSize);
                return Ok(allInAppPurchases);
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
