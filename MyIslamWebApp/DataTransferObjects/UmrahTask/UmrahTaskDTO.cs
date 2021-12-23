using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyIslamWebApp.DataTransferObjects.UmrahTask
{
    public class UmrahTaskDTO
    {
        public int UmrahTaskId { get; set; }
        public string UmrahTaskName { get; set; }
    }

    public class UmrahTaskListDTO
    {
        public int UmrahTaskId { get; set; }
        public string UmrahTaskName { get; set; }
        public int UmrahTaskCompleteId { get; set; }
        public bool UmrahTaskIsCompleted { get; set; }
    }
}