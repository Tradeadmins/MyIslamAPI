using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.UmrahGuideComplete
{
    public class UmrahGuideCompleteDTO
    {
        public int UmrahGuideCompleteId { get; set; }
        public int UmrahGuideId { get; set; }
        public string UmrahGuideCompleteByUserId { get; set; }
    }
}