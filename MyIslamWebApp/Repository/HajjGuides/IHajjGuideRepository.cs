using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.HajjGuide;

namespace MyIslamWebApp.Repository.HajjGuides
{
    public interface IHajjGuideRepository : IRepository<HajjGuide>
    {
        bool AddHajjGuide(HajjGuide hajjGuide);
        bool DeleteHajjGuide(HajjGuide hajjGuide);
        bool DeleteHajjGuideById(int hajjGuideId);
        bool UpdateHajjGuide(HajjGuide hajjGuide);
        HajjGuide GetHajjGuideById(int hajjGuideId);
        Result<HajjGuideListDTO> GetAllHajjGuides(int pageIndex, int pageSize, string userId);
    }
}