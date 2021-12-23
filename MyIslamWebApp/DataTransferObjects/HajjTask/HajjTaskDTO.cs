using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.HajjTask
{
    public class HajjTaskDTO
    {
        public int HajjTaskId { get; set; }        
        public string HajjTaskName { get; set; }
    }

    public class HajjTaskListDTO
    {
        public int HajjTaskId { get; set; }
        public string HajjTaskName { get; set; }
        public int HajjTaskCompleteId { get; set; }
        public bool HajjTaskIsCompleted { get; set; }
    }
}