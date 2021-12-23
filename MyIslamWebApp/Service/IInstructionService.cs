using System.Collections.Generic;
using MyIslamWebApp.DataTransferObjects.Instruction;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IInstructionService
    {
        //Instruction CRUD Operations
        ResultDTO<InstructionDTO> GetAllInstructions(int pageIndex, int pageSize);
        InstructionDTO GetInstructionById(int myEventId);
        bool AddInstruction(InstructionDTO myEvent, string userId);
        bool UpdateInstruction(InstructionDTO myEvent, string userId);
        bool DeleteInstructionById(int myEventId, string userId);
        List<InstructionDTO> GetInstructionByLanguage(int languageCode);
    }
}