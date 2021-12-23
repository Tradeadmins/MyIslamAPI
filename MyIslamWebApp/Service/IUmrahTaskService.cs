using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyIslamWebApp.DataTransferObjects.UmrahTask;
using MyIslamWebApp.Models;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public interface IUmrahTaskService
    {
        //UmrahTask CRUD Operations
        ResultDTO<UmrahTaskListDTO> GetAllUmrahTasks(int pageIndex, int pageSize, string userId);
        UmrahTaskDTO GetUmrahTaskById(int umrahTaskId);
        bool DeleteUmrahTask(UmrahTaskDTO dua, string userId);
        bool AddUmrahTask(UmrahTaskDTO umrahTask, string userId);
        bool DeleteUmrahTaskById(int umrahTaskId, string userId);
        bool UpdateUmrahTask(UmrahTaskDTO umrahTask, string userId);
    }
}