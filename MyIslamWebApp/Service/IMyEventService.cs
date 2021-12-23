using MyIslamWebApp.DataTransferObjects.MyEvent;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IMyEventService
    {
        //MyEvent CRUD Operations
        ResultDTO<MyEventDTO> GetAllMyEvents(int pageIndex, int pageSize);
        ResultDTO<MyEventDTO> GetAllMyEventsByUserId(int pageIndex, int pageSize, string userId);
        ResultDTO<MyEventDTO> GetAllMyEventsByLatLong(int pageIndex, int pageSize, double latitude, double longitude);
        MyEventDTO GetMyEventById(int myEventId);
        bool AddMyEvent(MyEventDTO myEvent, string userId);
        bool DeleteMyEventById(int myEventId, string userId);
        bool UpdateMyEvent(MyEventDTO myEvent, string userId);
    }
}