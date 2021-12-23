using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.UmrahTaskComplete
{
    public class UmrahTaskCompleteDTO
    {
        public int UmrahTaskCompleteId { get; set; }
        public int UmrahTaskId { get; set; }
        public string UmrahTaskCompleteByUserId { get; set; }
    }
}