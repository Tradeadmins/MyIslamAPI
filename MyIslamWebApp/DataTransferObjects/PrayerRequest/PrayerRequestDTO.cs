using MyIslamWebApp.DataTransferObjects.MakeDua;
using System.Collections.Generic;

namespace MyIslamWebApp.DataTransferObjects.PrayerRequest
{
    public class PrayerRequestDTO
    {
        public int PrayerRequestId { get; set; }
        public string PrayerRequestText { get; set; }
    }

    public class PrayerRequestListDTO
    {
        public int PrayerRequestId { get; set; }
        public string PrayerRequestText { get; set; }
        public string PrayerRequestTotalDuaCount { get; set; }
        public bool PrayerRequestIsLiked { get; set; }
    }

}