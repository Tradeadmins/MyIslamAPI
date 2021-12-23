using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.HajjTaskComplete
{
    public class HajjTaskCompleteDTO
    {       
        public int HajjTaskCompleteId { get; set; }       
        public int HajjTaskId { get; set; }       
        public string HajjTaskCompleteByUserId { get; set; }
    }
}