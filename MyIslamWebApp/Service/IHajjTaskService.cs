using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.HajjTask;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IHajjTaskService
    {
        //HajjTask CRUD Operations
        ResultDTO<HajjTaskListDTO> GetAllHajjTasks(int pageIndex, int pageSize, string userId);      
        HajjTaskDTO GetHajjTaskById(int hajjTaskId);
        bool DeleteHajjTask(HajjTaskDTO dua, string userId);
        bool AddHajjTask(HajjTaskDTO hajjTask, string userId);
        bool DeleteHajjTaskById(int hajjTaskId, string userId);
        bool UpdateHajjTask(HajjTaskDTO hajjTask, string userId);
    }
}