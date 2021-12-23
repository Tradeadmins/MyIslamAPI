using MyIslamWebApp.DataContext;
using MyIslamWebApp.DataTransferObjects.Instruction;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyIslamWebApp.Repository.Instructions
{
    /// <summary>
    /// CRUD operations for Instruction table using Generic Repository Pattern
    /// </summary>
    public class InstructionRepository : RepositoryBase<Instruction>, IInstructionRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public InstructionRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public InstructionRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in Instruction table
        /// </summary>
        /// <param name="instruction">Instance of Instruction class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddInstruction(Instruction instruction)
        {
            try
            {
                if (instruction == null)
                    return false;

                _dbContext.Instructions.Add(instruction);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in Instruction table
        /// </summary>
        /// <param name="instruction">The instance of Instruction class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateInstruction(Instruction instruction)
        {
            try
            {
                if (instruction == null)
                    return false;

                _dbContext.Entry(instruction).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the Instruction table
        /// </summary>
        /// <param name="instruction">The instance of Instruction class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteInstruction(Instruction instruction)
        {
            try
            {
                if (instruction == null)
                    return false;

                _dbContext.Entry(instruction).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using Instruction ID of the Instruction table
        /// </summary>
        /// <param name="instructionId">Primary Key of the row (Instruction ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteInstructionById(int instructionId)
        {
            try
            {
                if (int.Equals(instructionId, 0))
                    return false;

                Instruction record = _dbContext.Instructions.Find(instructionId);

                if (record == null)
                    return false;

                _dbContext.Instructions.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from Instruction table
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public Result<Instruction> GetAllInstructions(int pageIndex, int pageSize)
        {
            try
            {
                Result<Instruction> instructionsList = new Result<Instruction>();
               
                instructionsList.Response = _dbContext.Instructions.Where(x => x.IsActive == true).OrderBy(x=>x.InstructionId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                instructionsList.TotalCount = _dbContext.Instructions.Where(x => x.IsActive == true).Count();
               
                return instructionsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using Instruction ID from Instruction table
        /// </summary>
        /// <param name="instructionId">Primary Key of the row (Instruction ID)</param>
        /// <returns>Returns a Instruction row matching the passing ID</returns>
        public Instruction GetInstructionById(int instructionId)
        {
            try
            {
                if (Equals(instructionId, 0))
                    return null;

                return _dbContext.Instructions.AsNoTracking().Where(x => x.InstructionId == instructionId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using Instruction ID from Instruction table
        /// </summary>
        /// <returns>Returns a Instruction row matching the passing ID</returns>
        public IEnumerable<Instruction> GetAllInstructionCategory()
        {
            try
            {
                var data = _dbContext.Instructions.ToList();
                if (data.Count() == 0)
                    return null;

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using Instruction ID from Instruction table
        /// </summary>
        /// <param name="languageCode"></param>
        /// <param name="utcDateTime"></param>       
        /// <returns>Returns a Instruction row matching the passing ID</returns>
        public List<Instruction> GetInstructionByLanguage(int languageCode)
        {
            try
            {
                AppLangauges language = (AppLangauges)languageCode;
                List<Instruction> data = _dbContext.Instructions.Where(x => x.InstructionLanguage == language && x.IsActive == true).ToList();
                if (Equals(languageCode))
                    return new List<Instruction>();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}