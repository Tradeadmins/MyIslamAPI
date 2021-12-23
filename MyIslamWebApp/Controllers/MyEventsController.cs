using MyIslamWebApp.DataTransferObjects.MyEvent;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{
    /// <summary>
    /// MyEvents Apis List
    /// </summary>

    [RoutePrefix("api/myEvent")]
    public class MyEventsController : ApiController
    {
        #region Properties
        string userId = string.Empty;
        private readonly IMyEventService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IMyEventService() instance
        /// </summary>
        /// <param name="service"></param>
        public MyEventsController(IMyEventService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public MyEventsController()
        {
            _service = new MyEventService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods     
        
        /// <summary>
        /// Add new MyEvent
        /// </summary>
        /// <remarks>
        /// Add a new MyEvent
        /// </remarks>
        /// <param name="myEvent">MyEvent to add</param>
        /// <returns></returns>
        /// <response code="201">MyEvent 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] MyEventDTO myEvent)
        {
            try
            {
                if (myEvent == null)
                    return BadRequest("Event Data model is empty.");
                _service.AddMyEvent(myEvent, userId);
                return Ok("MyEvents Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing MyEvent
        /// </summary>
        /// <param name="myEvent">MyEvent to update</param>
        /// <returns></returns>
        /// <response code="200">MyEvent updated</response>
        /// <response code="404">MyEvent not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] MyEventDTO myEvent)
        {
            try
            {
                if (myEvent != null)
                {
                    var result = _service.UpdateMyEvent(myEvent, userId);

                    if (result)
                        return Ok("Event Updated Successfully.");
                    else
                        return Ok("Event not found.");
                }
                else
                {
                    return BadRequest("Event Data model is empty.");
                }

            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a MyEvent
        /// </summary>
        /// <remarks>
        /// Delete a MyEvent
        /// </remarks>
        /// <param name="myEventId">Id of the MyEvent to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int myEventId)
        {
            try
            {
                if (!int.Equals(myEventId, 0))
                {
                    var response = _service.DeleteMyEventById(myEventId, userId);
                    if (response)
                        return Ok("Record has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("Event Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get MyEvent by id
        /// </summary>
        /// <remarks>
        /// Get a MyEvent by id
        /// </remarks>
        /// <param name="myEventId">Id of MyEvent</param>
        /// <returns></returns>
        /// <response code="200">MyEvent found</response>
        /// <response code="404">MyEvent not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int myEventId)
        {
            try
            {
                if (!int.Equals(myEventId, 0))
                {
                    var myEvent = _service.GetMyEventById(myEventId);

                    if (myEvent != null)
                        return Ok(myEvent);
                    else
                        return BadRequest("Events are not available.");
                }
                else
                {
                    return BadRequest("Event Id is invalid");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }


        /// <summary>
        /// Get all MyEvents
        /// </summary>
        /// <remarks>
        /// Get a list of all MyEvents
        /// </remarks>
        /// <param name="pageIndex">pageIndex of MyEvent</param>
        /// <param name="pageSize">pageSize of MyEvent</param>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAll") Route("GetAll")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allMyEvents = _service.GetAllMyEvents(pageIndex, pageSize);
                return Ok(allMyEvents);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }


        /// <summary>
        /// Get all MyEvents By UserId
        /// </summary>
        /// <remarks>
        /// Get a list of all MyEvents  By UserId
        /// </remarks>
        /// <param name="pageIndex">pageIndex of MyEvent</param>
        /// <param name="pageSize">pageSize of MyEvent</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllEventsByUserId") Route("GetAllEventsByUserId")]
        public IHttpActionResult GetAllEventsByUserId(int pageIndex, int pageSize, string userId)
        {
            try
            {
                var allMyEvents = _service.GetAllMyEventsByUserId(pageIndex, pageSize, userId);
                return Ok(allMyEvents);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get all MyEvents By Lat Long
        /// </summary>
        /// <remarks>
        /// Get a list of all MyEvents By Lat Long
        /// </remarks>
        /// <param name="pageIndex">pageIndex of MyEvent</param>
        /// <param name="pageSize">pageSize of MyEvent</param>
        /// <param name="latitude">latitude of MyEvent</param>
        /// <param name="longitude">longitude of MyEvent</param>
        /// <returns></returns>
        /// <response code="200"></response>

        [Authorize]
        [HttpGet]
        [ActionName("GetAllEventsByLatLong") Route("GetAllEventsByLatLong")]
        public IHttpActionResult GetAllEventsByLatLong(int pageIndex, int pageSize, double latitude, double longitude)
        {
            try
            {
                var allMyEvents = _service.GetAllMyEventsByLatLong(pageIndex, pageSize, latitude, longitude);
                return Ok(allMyEvents);
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
