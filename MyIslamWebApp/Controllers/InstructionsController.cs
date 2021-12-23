using MyIslamWebApp.Filter;
using MyIslamWebApp.Service;
using NLog;
using System;
using System.Web.Http;
using MyIslamWebApp.DataTransferObjects.Instruction;
using Microsoft.AspNet.Identity;
using System.Web;
using System.IO;
using System.Linq;

namespace MyIslamWebApp.Controllers
{/// <summary>
 /// Instructions Apis List
 /// </summary>
    [RoutePrefix("api/instruction")]
    public class InstructionsController : ApiController
    {
        #region Properties
        string userId = string.Empty;
        private readonly IInstructionService _service;
        public static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor: 
        /// Used when someone access this controller by passing the IInstructionService() instance
        /// </summary>
        /// <param name="service"></param>
        public InstructionsController(IInstructionService service)
        {
            _service = service;
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }

        /// <summary>
        /// Default Constructor:
        /// Used by default or if no overload constructor is called
        /// </summary>
        public InstructionsController()
        {
            _service = new InstructionService();
            userId = !string.IsNullOrEmpty(User.Identity.GetUserId()) ? User.Identity.GetUserId() : "Null";
        }
        #endregion

        #region Action Methods 

        /// <summary>
        /// Add new Instruction
        /// </summary>
        /// <remarks>
        /// Add a new Instruction
        /// </remarks>
        /// <param name="instruction">Instruction to add</param>
        /// <returns></returns>
        /// <response code="201">Instruction 
        /// created</response>
        [Authorize]
        [HttpPost]
        [ActionName("Add") Route("Add")]
        [ModelValidator]
        public IHttpActionResult Post([FromBody] InstructionDTO instruction)
        {            
            try
            {
                if (instruction == null)
                return BadRequest("Instructions Data model is empty.");
                _service.AddInstruction(instruction, userId);
                return Ok("Instructions Created Successfully");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Update an existing Instruction
        /// </summary>
        /// <param name="instruction">Instruction to update</param>
        /// <returns></returns>
        /// <response code="200">Instruction updated</response>
        /// <response code="404">Instruction not found</response>
        [Authorize]
        [HttpPut]
        [ActionName("Update") Route("Update")]
        [ModelValidator]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public IHttpActionResult Put([FromBody] InstructionDTO instruction)
        {
            try
            {
                if (instruction != null)
                {

                    var result = _service.UpdateInstruction(instruction, userId);

                    if (result)
                        return Ok("Instruction Updated Successfully.");
                    else
                        return Ok("Instruction not found.");
                }
                else
                {
                    return BadRequest("Instruction Data model is empty.");
                }

            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Delete a Instruction
        /// </summary>
        /// <remarks>
        /// Delete a Instruction
        /// </remarks>
        /// <param name="instructionId">Id of the Instruction to delete</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [ActionName("DeleteById") Route("DeleteById")]
        public IHttpActionResult Delete(int instructionId)
        {
            try
            {
                if (!int.Equals(instructionId, 0))
                {
                    var response = _service.DeleteInstructionById(instructionId, userId);
                    if (response)
                        return Ok("Instruction has Deleted Succefully.");
                    else
                        return BadRequest("Instruction has failed to Deleted.");
                }
                else
                {
                    return BadRequest("Instruction Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get Instruction by id
        /// </summary>
        /// <remarks>
        /// Get a Instruction by id
        /// </remarks>
        /// <param name="instructionId">Id of Instruction</param>
        /// <returns></returns>
        /// <response code="200">Instruction found</response>
        /// <response code="404">Instruction not found</response>
        [Authorize]
        [HttpGet]
        [ActionName("GetById") Route("GetById")]
        public IHttpActionResult Get(int instructionId)
        {
            try
            {
                if (!int.Equals(instructionId, 0))
                {
                    var instruction = _service.GetInstructionById(instructionId);

                    if (instruction != null)
                        return Ok(instruction);
                    else
                        return BadRequest("Instruction are not available.");
                }
                else
                {
                    return BadRequest("The Instructions ID is invalid");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get all Instruction
        /// </summary>
        /// <remarks>
        /// Get a list of all Instruction
        /// </remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [Authorize]
        [HttpGet]
        [ActionName("GetAllInstruction") Route("GetAllInstruction")]
        public IHttpActionResult Get(int pageIndex, int pageSize)
        {
            try
            {
                var allInstructions = _service.GetAllInstructions(pageIndex, pageSize);
                return Ok(allInstructions);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Get a Instruction on the basis of language code
        /// </summary>
        /// <remarks>
        /// Get a Instruction on the basis of language code
        /// </remarks>
        /// <param name="languageCode">Language code of Instruction to get</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [ActionName("GetInstructionByLanguage") Route("GetInstructionByLanguage")]
        public IHttpActionResult GetInstructionByLanguage(int languageCode)
        {
            try
            {
                if (!int.Equals(languageCode, 0))
                {
                    var instruction = _service.GetInstructionByLanguage(languageCode);
                    if (instruction != null)
                        return Ok(instruction);
                    else
                        return BadRequest("Instructions Data model is empty.");
                }
                else
                {
                    return BadRequest("The Instructions language code or datetime is invalid");
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
