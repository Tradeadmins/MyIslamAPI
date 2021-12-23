using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.HajjGuide
{
    public class HajjGuideDTO
    {
        public int HajjGuideId { get; set; }
        public string HajjGuideName { get; set; }
    }

    public class HajjGuideListDTO
    {
        public int HajjGuideId { get; set; }
        public string HajjGuideName { get; set; }
        public int HajjGuideCompleteId { get; set; }
        public bool HajjGuideIsCompleted { get; set; }
    }
}