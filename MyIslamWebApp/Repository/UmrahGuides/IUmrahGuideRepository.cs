using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.UmrahGuide;

namespace MyIslamWebApp.Repository.UmrahGuides
{
    public interface IUmrahGuideRepository : IRepository<UmrahGuide>
    {
        bool AddUmrahGuide(UmrahGuide umrahGuide);
        bool DeleteUmrahGuide(UmrahGuide umrahGuide);   
        bool DeleteUmrahGuideById(int umrahGuideId);
        bool UpdateUmrahGuide(UmrahGuide umrahGuide);
        UmrahGuide GetUmrahGuideById(int umrahGuideId);
        Result<UmrahGuideListDTO> GetAllUmrahGuides(int pageIndex, int pageSize, string userId);
    }
}