using MyIslamWebApp.DataTransferObjects.MyEvent;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using System;
using System.Collections.Generic;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class MyEventService : IMyEventService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public MyEventService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public MyEventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region MyEvent Methods

        public bool AddMyEvent(MyEventDTO myEvent, string userId)
        {
            try
            {
                var result = false;

                if (myEvent == null)
                    return result;

                var addMyEvent = new MyEvent();
                addMyEvent.MyEventId = myEvent.MyEventId;
                addMyEvent.MyEventCategory = myEvent.MyEventCategory;
                addMyEvent.MyEventName = myEvent.MyEventName;
                addMyEvent.Address = myEvent.Address;
                addMyEvent.City = myEvent.City;
                addMyEvent.Country = myEvent.Country;
                addMyEvent.MobileNumber = myEvent.MobileNumber;
                addMyEvent.Description = myEvent.Description;
                addMyEvent.MyEventLatitude = myEvent.MyEventLatitude;
                addMyEvent.MyEventLongitude = myEvent.MyEventLongitude;
                addMyEvent.MyEventMinor = myEvent.MyEventMinor;
                addMyEvent.MyEventStartDate = myEvent.MyEventStartDate;
                addMyEvent.MyEventEndDate = myEvent.MyEventEndDate;

                //Default Values
                addMyEvent.IsActive = true;
                addMyEvent.CreatedBy = userId;
                addMyEvent.CreatedOn = DateTime.Now;
                addMyEvent.UpdatedBy = userId;
                addMyEvent.UpdatedOn = DateTime.Now;

                result = _unitOfWork.myEventRepository.AddMyEvent(addMyEvent);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateMyEvent(MyEventDTO myEvent, string userId)
        {
            try
            {
                var result = false;
                if (myEvent == null)
                    return result;

                var _event = _unitOfWork.myEventRepository.GetMyEventById(myEvent.MyEventId);

                if (_event == null)
                    return result;

                var updateMyEvent = new MyEvent();
                updateMyEvent.MyEventId = myEvent.MyEventId;
                updateMyEvent.MyEventCategory = myEvent.MyEventCategory;
                updateMyEvent.MyEventName = myEvent.MyEventName;
                updateMyEvent.Address = myEvent.Address;
                updateMyEvent.City = myEvent.City;
                updateMyEvent.Country = myEvent.Country;
                updateMyEvent.MobileNumber = myEvent.MobileNumber;
                updateMyEvent.Description = myEvent.Description;
                updateMyEvent.MyEventLatitude = myEvent.MyEventLatitude;
                updateMyEvent.MyEventLongitude = myEvent.MyEventLongitude;
                updateMyEvent.MyEventMinor = myEvent.MyEventMinor;
                updateMyEvent.MyEventStartDate = myEvent.MyEventStartDate;
                updateMyEvent.MyEventEndDate = myEvent.MyEventEndDate;

                ////Default Values
                updateMyEvent.IsActive = _event.IsActive;
                updateMyEvent.CreatedBy = _event.CreatedBy;
                updateMyEvent.CreatedOn = _event.CreatedOn;
                updateMyEvent.UpdatedBy = userId;
                updateMyEvent.UpdatedOn = DateTime.Now;

                result = _unitOfWork.myEventRepository.UpdateMyEvent(updateMyEvent);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMyEventById(int myEventID, string userId)
        {
            try
            {
                var result = false;
                if (myEventID == 0)
                    return result;

                var _event = _unitOfWork.myEventRepository.GetMyEventById(myEventID);

                if (_event == null)
                    return result;

                var updateMyEvent = new MyEvent();
                updateMyEvent = _event;
                ////Default Values
                updateMyEvent.IsActive = false;
                updateMyEvent.UpdatedBy = userId;
                updateMyEvent.UpdatedOn = DateTime.Now;

                result = _unitOfWork.myEventRepository.DeleteMyEvent(updateMyEvent);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MyEventDTO GetMyEventById(int myEventID)
        {
            try
            {
                MyEventDTO myEventResponse = new MyEventDTO();
                var myEvent = _unitOfWork.myEventRepository.GetMyEventById(myEventID);
                if (myEvent == null)
                    return null;

                if (myEvent.IsActive)
                {
                    myEventResponse.MyEventId = myEvent.MyEventId;
                    myEventResponse.MyEventCategory = myEvent.MyEventCategory;
                    myEventResponse.MyEventName = myEvent.MyEventName;
                    myEventResponse.Address = myEvent.Address;
                    myEventResponse.City = myEvent.City;
                    myEventResponse.Country = myEvent.Country;
                    myEventResponse.MobileNumber = myEvent.MobileNumber;
                    myEventResponse.Description = myEvent.Description;
                    myEventResponse.MyEventLatitude = myEvent.MyEventLatitude;
                    myEventResponse.MyEventLongitude = myEvent.MyEventLongitude;
                    myEventResponse.MyEventMinor = myEvent.MyEventMinor;
                    myEventResponse.MyEventStartDate = myEvent.MyEventStartDate;
                    myEventResponse.MyEventEndDate = myEvent.MyEventEndDate;
                }
                return myEventResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<MyEventDTO> GetAllMyEvents(int pageIndex, int pageSize)
        {
            try
            {
                var response = new ResultDTO<MyEventDTO>();
                var myEventList = new List<MyEventDTO>();
                MyEventDTO myEventResponse;

                var allMyEvents = _unitOfWork.myEventRepository.GetAllMyEvents(pageIndex, pageSize);

                if (allMyEvents.TotalCount == 0)
                    return response;

                foreach (var myEvent in allMyEvents.Response)
                {
                    if (myEvent.IsActive)
                    {
                        myEventResponse = new MyEventDTO();
                        myEventResponse.MyEventId = myEvent.MyEventId;
                        myEventResponse.MyEventCategory = myEvent.MyEventCategory;
                        myEventResponse.MyEventName = myEvent.MyEventName;
                        myEventResponse.Address = myEvent.Address;
                        myEventResponse.City = myEvent.City;
                        myEventResponse.Country = myEvent.Country;
                        myEventResponse.MobileNumber = myEvent.MobileNumber;
                        myEventResponse.Description = myEvent.Description;
                        myEventResponse.MyEventLatitude = myEvent.MyEventLatitude;
                        myEventResponse.MyEventLongitude = myEvent.MyEventLongitude;
                        myEventResponse.MyEventMinor = myEvent.MyEventMinor;
                        myEventResponse.MyEventStartDate = myEvent.MyEventStartDate;
                        myEventResponse.MyEventEndDate = myEvent.MyEventEndDate;

                        myEventList.Add(myEventResponse);
                    }
                }

                response.Response = myEventList;
                response.TotalCount = allMyEvents.TotalCount;

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<MyEventDTO> GetAllMyEventsByUserId(int pageIndex, int pageSize, string userId)
        {
            try
            {
                var response = new ResultDTO<MyEventDTO>();
                var myEventList = new List<MyEventDTO>();
                MyEventDTO myEventResponse;

                var allMyEvents = _unitOfWork.myEventRepository.GetAllMyEventsByUserId(pageIndex, pageSize, userId);

                if (allMyEvents.TotalCount == 0)
                    return response;

                foreach (var myEvent in allMyEvents.Response)
                {
                    if (myEvent.IsActive)
                    {
                        myEventResponse = new MyEventDTO();
                        myEventResponse.MyEventId = myEvent.MyEventId;
                        myEventResponse.MyEventCategory = myEvent.MyEventCategory;
                        myEventResponse.MyEventName = myEvent.MyEventName;
                        myEventResponse.Address = myEvent.Address;
                        myEventResponse.City = myEvent.City;
                        myEventResponse.Country = myEvent.Country;
                        myEventResponse.MobileNumber = myEvent.MobileNumber;
                        myEventResponse.Description = myEvent.Description;
                        myEventResponse.MyEventLatitude = myEvent.MyEventLatitude;
                        myEventResponse.MyEventLongitude = myEvent.MyEventLongitude;
                        myEventResponse.MyEventMinor = myEvent.MyEventMinor;
                        myEventResponse.MyEventStartDate = myEvent.MyEventStartDate;
                        myEventResponse.MyEventEndDate = myEvent.MyEventEndDate;

                        myEventList.Add(myEventResponse);
                    }
                }

                response.Response = myEventList;
                response.TotalCount = allMyEvents.TotalCount;

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<MyEventDTO> GetAllMyEventsByLatLong(int pageIndex, int pageSize, double latitude, double longitude)
        {
            try
            {
                var response = new ResultDTO<MyEventDTO>();
                var myEventList = new List<MyEventDTO>();
                MyEventDTO myEventResponse;

                var allMyEvents = _unitOfWork.myEventRepository.GetAllMyEventsByLatLong(pageIndex, pageSize, latitude, longitude);

                if (allMyEvents.TotalCount == 0)
                    return response;

                foreach (var myEvent in allMyEvents.Response)
                {
                    myEventResponse = new MyEventDTO();
                    myEventResponse.MyEventId = myEvent.MyEventId;
                    myEventResponse.MyEventCategory = myEvent.MyEventCategory;
                    myEventResponse.MyEventName = myEvent.MyEventName;
                    myEventResponse.Address = myEvent.Address;
                    myEventResponse.City = myEvent.City;
                    myEventResponse.Country = myEvent.Country;
                    myEventResponse.MobileNumber = myEvent.MobileNumber;
                    myEventResponse.Description = myEvent.Description;
                    myEventResponse.MyEventLatitude = myEvent.MyEventLatitude;
                    myEventResponse.MyEventLongitude = myEvent.MyEventLongitude;
                    myEventResponse.MyEventMinor = myEvent.MyEventMinor;
                    myEventResponse.MyEventStartDate = myEvent.MyEventStartDate;
                    myEventResponse.MyEventEndDate = myEvent.MyEventEndDate;
                    myEventResponse.Distance = myEvent.Distance;

                    myEventList.Add(myEventResponse);
                }

                response.Response = myEventList;
                response.TotalCount = allMyEvents.TotalCount;

                return response;
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