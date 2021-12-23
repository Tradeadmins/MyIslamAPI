using MyIslamWebApp.DataTransferObjects.Instruction;
using MyIslamWebApp.DataTransferObjects.Result;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using System;
using System.Collections.Generic;

namespace MyIslamWebApp.Service
{
    public class InstructionService : IInstructionService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public InstructionService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public InstructionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Instruction Methods
        
        public bool AddInstruction(InstructionDTO instruction, string userId)
        {
            try
            {
                var result = false;

                if (instruction == null)
                    return result;

                var addInstruction = new Instruction();
                addInstruction.InstructionId = instruction.InstructionId;
                addInstruction.InstructionTitle = instruction.InstructionTitle;
                addInstruction.InstructionDescription = instruction.InstructionDescription;
                addInstruction.InstructionImageURL = instruction.InstructionImageURL;
                addInstruction.InstructionLanguage = instruction.InstructionLanguage;

                //Default Values
                addInstruction.IsActive = true; 
                addInstruction.CreatedBy = userId;
                addInstruction.CreatedOn = DateTime.Now;
                addInstruction.UpdatedBy = userId;
                addInstruction.UpdatedOn = DateTime.Now;

                result = _unitOfWork.instructionRepository.AddInstruction(addInstruction);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateInstruction(InstructionDTO instruction, string userId)
        {
            try
            {
                var result = false;
                if (instruction == null)
                    return result;

                var _instruction = _unitOfWork.instructionRepository.GetInstructionById(instruction.InstructionId);

                if (_instruction == null)
                    return result;

                var updateInstruction = new Instruction();
                updateInstruction.InstructionId = instruction.InstructionId;
                updateInstruction.InstructionTitle = instruction.InstructionTitle;
                updateInstruction.InstructionDescription = instruction.InstructionDescription;
                updateInstruction.InstructionImageURL = instruction.InstructionImageURL;
                updateInstruction.InstructionLanguage = instruction.InstructionLanguage;

                ////Default Values
                updateInstruction.IsActive = _instruction.IsActive;
                updateInstruction.CreatedBy = _instruction.CreatedBy;
                updateInstruction.CreatedOn = _instruction.CreatedOn;
                updateInstruction.UpdatedBy = userId;
                updateInstruction.UpdatedOn = DateTime.Now;

                result = _unitOfWork.instructionRepository.UpdateInstruction(updateInstruction);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteInstruction(InstructionDTO instruction, string userId)
        {
            try
            {
                var result = false;
                if (instruction == null)
                    return result;

                var deleteInstruction = new Instruction();
                deleteInstruction.InstructionId = instruction.InstructionId;
                deleteInstruction.InstructionTitle = instruction.InstructionTitle;
                deleteInstruction.InstructionDescription = instruction.InstructionDescription;
                deleteInstruction.InstructionImageURL = instruction.InstructionImageURL;
                deleteInstruction.InstructionLanguage = instruction.InstructionLanguage;

                deleteInstruction.IsActive = false;
                deleteInstruction.UpdatedBy = userId;
                deleteInstruction.UpdatedOn = DateTime.Now;

                result = _unitOfWork.instructionRepository.DeleteInstruction(deleteInstruction);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteInstructionById(int instructionID, string userId)
        {
            try
            {
                var result = false;
                if (instructionID == 0)
                    return result;

                var instruction = _unitOfWork.instructionRepository.GetInstructionById(instructionID);

                if (instruction == null)
                    return result;

                var deleteInstruction = new Instruction();
                deleteInstruction = instruction;
                ////Default Values
                deleteInstruction.IsActive = false;
                deleteInstruction.UpdatedBy = userId;
                deleteInstruction.UpdatedOn = DateTime.Now;

                result = _unitOfWork.instructionRepository.DeleteInstruction(deleteInstruction);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<InstructionDTO> GetAllInstructions(int pageIndex, int pageSize)
        {
            try
            {
                var response = new ResultDTO<InstructionDTO>();
                var instructionsList = new List<InstructionDTO>();
                InstructionDTO instructionResponse;
                var allInstructions = _unitOfWork.instructionRepository.GetAllInstructions(pageIndex, pageSize);

                if (allInstructions.TotalCount == 0)
                    return response;

                foreach (var instruction in allInstructions.Response)
                {
                    if (instruction.IsActive)
                    {
                        instructionResponse = new InstructionDTO();
                        instructionResponse.InstructionId = instruction.InstructionId;
                        instructionResponse.InstructionTitle = instruction.InstructionTitle;
                        instructionResponse.InstructionDescription = instruction.InstructionDescription;
                        instructionResponse.InstructionLanguage = instruction.InstructionLanguage;
                        instructionResponse.InstructionImageURL = instruction.InstructionImageURL;
                        instructionsList.Add(instructionResponse);
                    }
                }
                response.Response = instructionsList;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InstructionDTO GetInstructionById(int instructionID)
        {
            try
            {
                InstructionDTO instructionResponse = new InstructionDTO();
                var instruction = _unitOfWork.instructionRepository.GetInstructionById(instructionID);
                if (instruction == null)
                    return null;

                if (instruction.IsActive)
                {
                    instructionResponse.InstructionId = instruction.InstructionId;
                    instructionResponse.InstructionTitle = instruction.InstructionTitle;
                    instructionResponse.InstructionDescription = instruction.InstructionDescription;
                    instructionResponse.InstructionImageURL = instruction.InstructionImageURL;
                    instructionResponse.InstructionLanguage = instruction.InstructionLanguage;
                }
                return instructionResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<InstructionDTO> GetInstructionByLanguage(int languageCode)
        {
            try
            {
                List<InstructionDTO> instructionResponse = new List<InstructionDTO>();
                var instruction = _unitOfWork.instructionRepository.GetInstructionByLanguage(languageCode);
                if (instruction == null)
                    return null;

                List<Instruction> instructionList = new List<Instruction>();
                foreach (var item in instruction)
                {
                    InstructionDTO instructionDTO = new InstructionDTO();

                    instructionDTO.InstructionId = item.InstructionId;
                    instructionDTO.InstructionTitle = item.InstructionTitle;
                    instructionDTO.InstructionDescription = item.InstructionDescription;
                    instructionDTO.InstructionLanguage = item.InstructionLanguage;
                    instructionDTO.InstructionImageURL = item.InstructionImageURL;
                    instructionResponse.Add(instructionDTO);
                }
                return instructionResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}