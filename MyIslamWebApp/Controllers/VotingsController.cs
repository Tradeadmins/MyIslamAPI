using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.Voting;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// Votings Apis List
 /// </summary>
    [RoutePrefix("api/voting")]
    public class VotingsController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IVotingService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IVotingService() instance
        /// </summary>
        /// <param name="service"></param>
        public VotingsController(IVotingService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public VotingsController()
        {
            _service = new VotingService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Add new Voting
        /// </summary>
        /// <remarks>
        /// Add a new Voting
        /// </remarks>
        /// <param name="voting">Voting to add</param>
        /// <returns></returns>
        /// <response code="201">Voting 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] VotingDTO voting)
        {
            try
            {
                if (voting == null)
                    return BadRequest("Voting Data model is empty.");
                _service.AddVoting(voting, userId);
                return Ok("Voting Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing Voting
        /// </summary>
        /// <param name="voting">Voting to update</param>
        /// <returns></returns>
        /// <response code="200">Voting updated</response>
        /// <response code="404">Voting not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] VotingDTO voting)
        {
            try
            {
                if (voting != null)
                {
                    var result = _service.UpdateVoting(voting, userId);
                    if (result)
                        return Ok("Voting Updated Successfully.");
                    else
                        return Ok("Voting not found.");
                }
                else
                {
                    return BadRequest("Voting Data model is empty.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a Voting
        /// </summary>
        /// <remarks>
        /// Delete a Voting
        /// </remarks>
        /// <param name="votingId">Id of the Voting to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int votingId)
        {
            try
            {
                if (!int.Equals(votingId, 0))
                {
                    var response = _service.DeleteVotingById(votingId, userId);
                    if (response)
                        return Ok("Voting has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("Voting Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get Voting by id
        /// </summary>
        /// <remarks>
        /// Get a Voting by id
        /// </remarks>
        /// <param name="votingId">Id of Voting</param>
        /// <returns></returns>
        /// <response code="200">Voting found</response>
        /// <response code="404">Voting not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int votingId)
        {
            try
            {
                if (!int.Equals(votingId, 0))
                {
                    var voting = _service.GetVotingById(votingId);

                    if (voting != null)
                        return Ok(voting);
                    else
                        return BadRequest("Voting not available.");
                }
                else
                {
                    return BadRequest("Voting ID is invalid");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get all Voting
        /// </summary>
        /// <remarks>
        /// Get a list of all Voting
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllVoting") Route("GetAllVoting")]
        public IHttpActionResult Get()
        {
            try
            {
                var allVotings = _service.GetAllVotings();
                return Ok(allVotings);
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
