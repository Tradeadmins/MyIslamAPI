using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.UmrahGuide
{
    public class UmrahGuideDTO
    {
        public int UmrahGuideId { get; set; }
        public string UmrahGuideName { get; set; }
    }

    public class UmrahGuideListDTO
    {
        public int UmrahGuideId { get; set; }
        public string UmrahGuideName { get; set; }
        public int UmrahGuideCompleteId { get; set; }
        public bool UmrahGuideIsCompleted { get; set; }
    }
}