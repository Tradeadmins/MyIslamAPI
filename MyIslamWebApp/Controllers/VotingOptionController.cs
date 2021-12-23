using System;
using System.Web.Http;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using Microsoft.AspNet.Identity;
using MyIslamWebApp.DataTransferObjects.VotionOption;

namespace MyIslamWebApp.Controllers
{
    /// <summary>
    /// VotingOptions Apis List
    /// </summary>
    [RoutePrefix("api/votingOption")]
    public class VotingOptionController : ApiController
    {

        #region Properties
        string userId = string.Empty;
        private readonly IVotingOptionService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IVotingOptionService() instance
        /// </summary>
        /// <param name="service"></param>
        public VotingOptionController(IVotingOptionService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public VotingOptionController()
        {
            _service = new VotingOptionService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods        

        /// <summary>
        /// Add new VotingOption
        /// </summary>
        /// <remarks>
        /// Add a new VotingOption
        /// </remarks>
        /// <param name="votingOption">VotingOption to add</param>
        /// <returns></returns>
        /// <response code="201">VotingOption 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] VotingOptionDTO votingOption)
        {
            try
            {
                if (votingOption == null)
                    return BadRequest("VotingOptions Data model is empty.");
                _service.AddVotingOption(votingOption, userId);
                return Ok("VotingOptions Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing VotingOption
        /// </summary>
        /// <param name="votingOption">VotingOption to update</param>
        /// <returns></returns>
        /// <response code="200">VotingOption Updated Successfully.</response>
        /// <response code="404">VotingOption not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] VotingOptionDTO votingOption)
        {
            try
            {
                if (votingOption != null)
                {
                    var result = _service.UpdateVotingOption(votingOption, userId);
                    if (result)
                        return Ok("VotingOption Updated Successfully.");
                    else
                        return Ok("VotingOption not found.");
                }
                else
                {
                    return BadRequest("VotingOption Data model is Empty");
                }

            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a VotingOption
        /// </summary>
        /// <remarks>
        /// Delete a VotingOption
        /// </remarks>
        /// <param name="votingOptionId">Id of the VotingOption to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int votingOptionId)
        {
            try
            {
                if (!int.Equals(votingOptionId, 0))
                {
                    var response = _service.DeleteVotingOptionById(votingOptionId, userId);
                    if (response)
                        return Ok("VotingOption has Deleted Succefully.");
                    else
                        return BadRequest("Record has failed to Deleted.");
                }
                else
                {
                    return BadRequest("VotingOption Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get VotingOption by Id
        /// </summary>
        /// <remarks>
        /// Get a VotingOption by Id
        /// </remarks>
        /// <param name="votingOptionId">Id of VotingOption</param>
        /// <returns></returns>
        /// <response code="200">VotingOption found</response>
        /// <response code="404">VotingOption not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int votingOptionId)
        {
            try
            {
                if (!int.Equals(votingOptionId, 0))
                {
                    var votingOption = _service.GetVotingOptionById(votingOptionId);

                    if (votingOption != null)
                        return Ok(votingOption);
                    else
                        return BadRequest("VotingOptions are not available.");
                }
                else
                {
                    return BadRequest("The VotingOptions ID is invalid");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get a list of all VotingOption
        /// </summary>
        /// <remarks>
        /// Get a list of all VotingOption
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllVotingOption") Route("GetAllVotingOption")]
        public IHttpActionResult Get()
        {
            try
            {
                var allVotingOptions = _service.GetAllVotingOptions();
                return Ok(allVotingOptions);
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
