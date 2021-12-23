using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System;
using System.Collections.Generic;

namespace MyIslamWebApp.Repository.Instructions
{
    public interface IInstructionRepository : IRepository<Instruction>
    {
        Result<Instruction> GetAllInstructions(int pageIndex, int pageSize);
        Instruction GetInstructionById(int InstructionId);
        bool AddInstruction(Instruction Instruction);
        bool DeleteInstruction(Instruction Instruction);
        bool DeleteInstructionById(int InstructionId);
        bool UpdateInstruction(Instruction Instruction);
        List<Instruction> GetInstructionByLanguage(int languageCode);
    }
}