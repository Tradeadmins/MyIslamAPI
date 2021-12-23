using MyIslamWebApp.DataTransferObjects.HajjGuideComplete;
using System.Collections.Generic;

namespace MyIslamWebApp.Service
{
    public interface IHajjGuideCompleteService
    {
        //HajjGuideComplete CRUD Operations
        bool AddHajjGuideComplete(HajjGuideCompleteDTO HajjGuideComplete, string userId);
        bool DeleteHajjGuideComplete(int hajjGuideCompleteId);
        IEnumerable<HajjGuideCompleteDTO> GetAllHajjGuideComplete();
        IEnumerable<HajjGuideCompleteDTO> GetAllHajjGuideCompleteByUser(string hajjGuideCompleteUser);
      
    }
}