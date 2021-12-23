using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System;
using MyIslamWebApp.DataTransferObjects.MyEvent;

namespace MyIslamWebApp.Repository.MyEvents
{
    public interface IMyEventRepository : IRepository<MyEvent>
    {
        Result<MyEvent> GetAllMyEvents(int pageIndex, int pageSize);
        Result<MyEvent> GetAllMyEventsByUserId(int pageIndex, int pageSize, string userId);
        Result<MyEventDTO> GetAllMyEventsByLatLong(int pageIndex, int pageSize, double latitude, double longitude);
        MyEvent GetMyEventById(int MyEventId);
        bool AddMyEvent(MyEvent MyEvent);
        bool DeleteMyEvent(MyEvent jumaQuote);
        bool DeleteMyEventById(int MyEventId);
        bool UpdateMyEvent(MyEvent MyEvent);
    }
}