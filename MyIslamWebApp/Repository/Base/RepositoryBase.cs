using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MyIslamWebApp.Repository.Base
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }
        #endregion

        #region Constructor
        public RepositoryBase(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentException("An instance of DbContext is required.");

            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in New Employee table
        /// </summary>
        /// <param name="entity">The instance of New Employee class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public virtual bool Add(T entity)
        {
            DbEntityEntry entry = DbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
                return false;
            }
            else
            {
                this.DbSet.Add(entity);
                return true;
            }
        }

        /// <summary>
        /// To delete a record from the New Employee table
        /// </summary>
        /// <param name="entity">The instance of New Employee class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public virtual bool Delete(T entity)
        {
            DbEntityEntry entry = DbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;

            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            return true;
        }

        /// <summary>
        /// To delete a record using Phone Number in the New Employee table
        /// </summary>
        /// <param name="id">Primary Key of the row (Phone Number)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public virtual bool DeleteById(string id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Delete(entity);
            }
            return true;
        }

        /// <summary>
        /// To get all data from New Employee table
        /// </summary>
        /// <returns>An IQuerable List of NewEmployee</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// To get a single record using Phone Number from New Employee table
        /// </summary>
        /// <param name="id">Primary Key of the row (Phone Number)</param>
        /// <returns>Returns a NewEmployee row matching the passing ID</returns>
        public virtual T GetById(string id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// To update a record in New Employee table
        /// </summary>
        /// <param name="entity">The instance of New Employee class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public virtual bool Update(T entity)
        {
            DbEntityEntry entry = DbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                DbSet.Attach(entity);

            }
            entry.State = EntityState.Modified;
            return true;
        }
        #endregion
    }
}