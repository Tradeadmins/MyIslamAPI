using MyIslamWebApp.DataContext;
using MyIslamWebApp.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.Models;
using System.Data.Entity;

namespace MyIslamWebApp.Repository.MakeDuas
{
    /// <summary>
    /// CRUD operations for MakeDua table using Generic Repository Pattern
    /// </summary>
    public class MakeDuaRepository : RepositoryBase<MakeDua>, IMakeDuaRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public MakeDuaRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public MakeDuaRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        /// <summary>
        /// To add a new record in MakeDua table
        /// </summary>
        /// <param name="makeDua">Instance of MakeDua class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddMakeDua(MakeDua makeDua)
        {
            try
            {
                if (makeDua == null)
                    return false;
                _dbContext.MakeDuas.Add(makeDua);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the MakeDua table
        /// </summary>
        /// <param name="makeDua">The instance of MakeDua class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteMakeDua(MakeDua makeDua)
        {
            try
            {
                if (makeDua == null)
                    return false;

                _dbContext.Entry(makeDua).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record using MakeDua ID of the Dua table
        /// </summary>
        /// <param name="makeDuaId">Primary Key of the row (MakeDua ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteMakeDuaById(int makeDuaId)
        {
            try
            {
                if (int.Equals(makeDuaId, 0))
                    return false;

                MakeDua record = _dbContext.MakeDuas.Find(makeDuaId);

                if (record == null)
                    return false;

                _dbContext.MakeDuas.Remove(record);
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using MakeDua ID from Dua table
        /// </summary>
        /// <param name="makeDuaId">Primary Key of the row (MakeDua ID)</param>
        /// <returns>Returns a Dua row matching the passing ID</returns>
        public MakeDua GetMakeDuaById(int makeDuaId)
        {
            try
            {
                if (Equals(makeDuaId, 0))
                    return null;

                return _dbContext.MakeDuas.AsNoTracking().Where(x => x.MakeDuaId == makeDuaId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from MakeDua table
        /// </summary>
        /// <returns>An IQuerable List of MakeDua</returns>
        public IEnumerable<MakeDua> GetAllMakeDua()
        {
            try
            {
                return _dbContext.MakeDuas.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        /// <summary>
        /// To get all records using MakeDua Username of the MakeDua table
        /// </summary>
        /// <param name="MakeDuaUserId">MakeDua Username</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public IEnumerable<MakeDua> GetAllMakeDuaByUser(string MakeDuaUserId)
        {
            try
            {
                var data = _dbContext.MakeDuas.Where(x => x.MakeDuaByUserId == MakeDuaUserId).ToList();
                if (Equals(MakeDuaUserId, null))
                    return data;
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