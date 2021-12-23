using MyIslamWebApp.DataTransferObjects.HajjTaskComplete;
using System.Collections.Generic;

namespace MyIslamWebApp.Service
{
    public interface IHajjTaskCompleteService
    {
        //HajjTaskComplete CRUD Operations
        IEnumerable<HajjTaskCompleteDTO> GetAllHajjTaskComplete();
        IEnumerable<HajjTaskCompleteDTO> GetAllHajjTaskCompleteByUser(string hajjTaskCompleteUser);
        bool AddHajjTaskComplete(HajjTaskCompleteDTO hajjTaskComplete, string userId);
        bool DeleteHajjTaskComplete(int hajjTaskCompleteId);
    }
}