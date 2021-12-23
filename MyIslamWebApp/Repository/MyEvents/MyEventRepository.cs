using MyIslamWebApp.DataContext;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Spatial;
using MyIslamWebApp.DataTransferObjects.MyEvent;
using System.Net;

namespace MyIslamWebApp.Repository.MyEvents
{
    /// <summary>
    /// CRUD operations for MyEvent table using Generic Repository Pattern
    /// </summary>
    public class MyEventRepository : RepositoryBase<MyEvent>, IMyEventRepository
    {
        #region Properties
        private readonly AuthContext _dbContext;
        #endregion

        #region Constructor
        public MyEventRepository() : base(new AuthContext())
        {
            _dbContext = new AuthContext();
        }

        public MyEventRepository(AuthContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        /// <summary>
        /// To add a new record in MyEvent table
        /// </summary>
        /// <param name="myEvent">Instance of MyEvent class</param>        
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool AddMyEvent(MyEvent myEvent)
        {
            try
            {
                if (myEvent == null)
                    return false;

                _dbContext.MyEvents.Add(myEvent);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To update a record in MyEvent table
        /// </summary>
        /// <param name="myEvent">The instance of MyEvent class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool UpdateMyEvent(MyEvent myEvent)
        {
            try
            {
                if (myEvent == null)
                    return false;

                _dbContext.Entry(myEvent).State = EntityState.Modified;
                return true;
                //Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To delete a record from the MyEvent table
        /// </summary>
        /// <param name="myEvent">The instance of MyEvent class</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteMyEvent(MyEvent myEvent)
        {
            try
            {
                if (myEvent == null)
                    return false;

                _dbContext.Entry(myEvent).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// To delete a record using MyEvent ID of the MyEvent table
        /// </summary>
        /// <param name="myEventId">Primary Key of the row (MyEvent ID)</param>
        /// <returns>Returns a bool flag, either true or false</returns>
        public bool DeleteMyEventById(int myEventId)
        {
            try
            {
                if (int.Equals(myEventId, 0))
                    return false;

                MyEvent record = _dbContext.MyEvents.Find(myEventId);
                record.IsActive = false;

                if (record == null)
                    return false;

                _dbContext.Entry(record).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get a single record using MyEvent ID from MyEvent table
        /// </summary>
        /// <param name="myEventId">Primary Key of the row (MyEvent ID)</param>
        /// <returns>Returns a MyEvent row matching the passing ID</returns>
        public MyEvent GetMyEventById(int myEventId)
        {
            try
            {
                if (Equals(myEventId, 0))
                    return null;

                return _dbContext.MyEvents.AsNoTracking().Where(x => x.MyEventId == myEventId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// To get all data from MyEvent table
        /// </summary>
        /// <param name="pageIndex">pageIndex of MyEvent</param>
        /// <param name="pageSize">pageSize of MyEvent</param>
        /// <returns>An IQuerable List of MyEvent</returns>
        public Result<MyEvent> GetAllMyEvents(int pageIndex, int pageSize)
        {
            try
            {

                Result<MyEvent> myEventsList = new Result<MyEvent>();

                var count = _dbContext.MyEvents.Where(x => x.IsActive == true).Count();
                if (count > 0)
                {
                    myEventsList.Response = _dbContext.MyEvents.Where(x => x.IsActive == true).OrderBy(x => x.MyEventStartDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    myEventsList.TotalCount = _dbContext.MyEvents.Where(x => x.IsActive == true).Count();
                }
                else
                {
                    myEventsList.Response = _dbContext.MyEvents.Where(x => x.IsActive == true).ToList();
                    myEventsList.TotalCount = _dbContext.MyEvents.Where(x => x.IsActive == true).Count();
                }

                return myEventsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from MyEvent table
        /// </summary>
        /// <param name="pageIndex">pageIndex of MyEvent</param>
        /// <param name="pageSize">pageSize of MyEvent</param>
        /// <param name="userId">userId</param>
        /// <returns>An IQuerable List of MyEvent</returns>
        public Result<MyEvent> GetAllMyEventsByUserId(int pageIndex, int pageSize, string userId)
        {
            try
            {
                Result<MyEvent> myEventsList = new Result<MyEvent>();

                myEventsList.Response = _dbContext.MyEvents.Where(x => x.IsActive == true && x.CreatedBy== userId).OrderBy(x => x.MyEventStartDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                myEventsList.TotalCount = _dbContext.MyEvents.Where(x => x.IsActive == true && x.CreatedBy == userId).Count();
               
                return myEventsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To get all data from MyEvent table
        /// </summary>
        /// <param name="pageIndex">pageIndex of MyEvent</param>
        /// <param name="pageSize">pageSize of MyEvent</param>
        /// <param name="latitude">latitude of MyEvent</param>
        /// <param name="longitude">longitude of MyEvent</param>
        /// <returns>An IQuerable List of MyEvent</returns>
        public Result<MyEventDTO> GetAllMyEventsByLatLong(int pageIndex, int pageSize, double latitude, double longitude)
        {
            try
            {
                Result<MyEventDTO> myEventsList = new Result<MyEventDTO>();

                DbGeography searchLocation = DbGeography.FromText(String.Format("POINT({0} {1})", longitude, latitude));

                var eventsList = (from myEvent in _dbContext.MyEvents
                                  where myEvent.IsActive == true  // (Additional filtering criteria here...)
                                  select new MyEventDTO
                                  {
                                      MyEventId = myEvent.MyEventId,
                                      MyEventCategory = myEvent.MyEventCategory,
                                      MyEventName = myEvent.MyEventName,
                                      Address = myEvent.Address,
                                      City = myEvent.City,
                                      Country = myEvent.Country,
                                      MobileNumber = myEvent.MobileNumber,
                                      Description = myEvent.Description,
                                      MyEventLatitude = myEvent.MyEventLatitude,
                                      MyEventLongitude = myEvent.MyEventLongitude,
                                      MyEventMinor = myEvent.MyEventMinor,
                                      MyEventStartDate = myEvent.MyEventStartDate,
                                      MyEventEndDate = myEvent.MyEventEndDate,
                                      Distance = searchLocation.Distance(DbGeography.FromText("POINT(" + myEvent.MyEventLongitude + " " + myEvent.MyEventLatitude + ")")) / 1000
                                  })
                                    .OrderBy(location => location.Distance)
                                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                myEventsList.Response = eventsList;
                myEventsList.TotalCount = eventsList.Count();

                return myEventsList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        #endregion
    }
}