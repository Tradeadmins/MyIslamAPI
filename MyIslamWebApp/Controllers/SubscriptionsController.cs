using System;
using System.Web.Http;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;
using MyIslamWebApp.DataTransferObjects.Subscription;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.Users;
using MyIslamWebApp.Entities;

namespace MyIslamWebApp.Controllers
{
    /// <summary>
    /// Subscriptions Apis List
    /// </summary>
    [RoutePrefix("api/subscription")]
    public class SubscriptionsController : ApiController
    {

        #region Properties      
        string userId = string.Empty;    
        private readonly ISubscriptionService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the ISubscriptionService() instance
        /// </summary>
        /// <param name="service"></param>
        public SubscriptionsController(ISubscriptionService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public SubscriptionsController()
        {
            _service = new SubscriptionService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods        

        /// <summary>
        /// Add new Subscription
        /// </summary>
        /// <remarks>
        /// Add a new Subscription
        /// </remarks>
        /// <param name="subscription">Subscription to add</param>   
        /// <returns></returns>
        /// <response code="201">Subscription 
        /// created</response>
        [AllowAnonymous]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] SubscriptionDTO subscription)
        {
            try
            {
                if (subscription == null)
                    return BadRequest("Subscriptions Data model is empty.");

                var checkUser = GetSubscriptionByUserId(subscription.SubscriptionByUserId);

                if (checkUser == null)
                {
                    return BadRequest("Subscriptions Already Subscribed.");
                }
                else
                {
                    _service.AddSubscription(subscription, userId);
                    return Ok("Subscriptions Created Successfully");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing Subscription
        /// </summary>
        /// <param name="subscription">Subscription to update</param>
        /// <returns></returns>
        /// <response code="200">Subscription Updated Successfully.</response>
        /// <response code="404">Subscription not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] SubscriptionDTO subscription)
        {
            try
            {
                if (subscription != null)
                {
                    var result = _service.UpdateSubscription(subscription, userId);
                    if (result)
                        return Ok("Subscription Updated Successfully.");
                    else
                        return Ok("Subscription not found.");
                }
                else
                {
                    return BadRequest("Subscription Data model is Empty");
                }

            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a Subscription
        /// </summary>
        /// <remarks>
        /// Delete a Subscription
        /// </remarks>
        /// <param name="subscriptionId">Id of the Subscription to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int subscriptionId)
        {
            try
            {
                if (!int.Equals(subscriptionId, 0))
                {
                    var response = _service.DeleteSubscriptionById(subscriptionId, userId);
                    if (response)
                        return Ok("Subscription has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("Subscription Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get Subscription by Id
        /// </summary>
        /// <remarks>
        /// Get a Subscription by Id
        /// </remarks>
        /// <param name="subscriptionId">Id of Subscription</param>
        /// <returns></returns>
        /// <response code="200">Subscription found</response>
        /// <response code="404">Subscription not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int subscriptionId)
        {
            try
            {
                if (!int.Equals(subscriptionId, 0))
                {
                    var subscription = _service.GetSubscriptionById(subscriptionId);

                    if (subscription != null)
                        return Ok(subscription);
                    else
                        return BadRequest("Subscriptions are not available.");
                }
                else
                {
                    return BadRequest("The Subscriptions ID is invalid");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get a list of all Subscription
        /// </summary>
        /// <remarks>
        /// Get a list of all Subscription
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetSubscriptionBy") Route("GetAllSubscription")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allSubscriptions = _service.GetAllSubscriptions(pageIndex, pageSize);
                return Ok(allSubscriptions);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get Subscription by Id
        /// </summary>
        /// <remarks>
        /// Get a Subscription by Id
        /// </remarks>
        /// <param name="userId">Id of Subscription</param>
        /// <returns></returns>
        /// <response code="200">Subscription found</response>
        /// <response code="404">Subscription not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetSubscriptionByUserId") Route("GetSubscriptionByUserId")]
        public IHttpActionResult GetSubscriptionByUserId(string userId)
        {
            try
            {
                if (!string.Equals(userId, null))
                {
                    var subscription = _service.GetSubscriptionByUserId(userId);

                    if (subscription == false)                       
                    return null;
                    else
                        return Ok("User not found in Subscription tabls");
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

        /// <summary>
        /// Get Subscription by Id
        /// </summary>
        /// <remarks>
        /// Get a Subscription by Id
        /// </remarks>
        /// <param name="userId">Id of Subscription</param>
        /// <returns></returns>
        /// <response code="200">Subscription found</response>
        /// <response code="404">Subscription not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetSubscriptionByUserId") Route("GetSubscriptionByUserId")]
        public IHttpActionResult CheckSubscriptionEndDate(string userId)
        {
            try
            {
                if (!string.Equals(userId, null))
                {
                    var subscription = _service.GetSubscriptionByUserId(userId);

                    if (subscription == false)
                        return null;
                    else
                        return Ok("User not found in Subscription tabls");
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
