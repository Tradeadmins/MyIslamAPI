using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.HajjGuideComplete
{
    public class HajjGuideCompleteDTO
    {
        public int HajjGuideCompleteId { get; set; }
        public int HajjGuideId { get; set; }
        public string HajjGuideCompleteByUserId { get; set; }
    }
}