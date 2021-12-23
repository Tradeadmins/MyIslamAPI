using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using System.Collections.Generic;
using MyIslamWebApp.DataTransferObjects.PrayerRequest;

namespace MyIslamWebApp.Repository.PrayerRequests
{
    public interface IPrayerRequestRepository : IRepository<PrayerRequest>
    {
        Result<PrayerRequestListDTO> GetAllPrayerRequests(int pageIndex, int pageSize, string userId);
        Result<PrayerRequestListDTO> GetAllPrayerRequestByUserId(string prayerRequestUserId);
        PrayerRequest GetPrayerRequestById(int prayerRequestId);
        bool AddPrayerRequest(PrayerRequest prayerRequest);
        bool DeletePrayerRequest(PrayerRequest prayerRequest);
        bool DeletePrayerRequestById(int prayerRequestId);
        bool UpdatePrayerRequest(PrayerRequest prayerRequest);
    }
}