using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.UmrahTask;

namespace MyIslamWebApp.Repository.UmrahTasks
{
    public interface IUmrahTaskRepository : IRepository<UmrahTask>
    {
        bool AddUmrahTask(UmrahTask umrahTask);
        bool DeleteUmrahTask(UmrahTask umrahTask);
        bool DeleteUmrahTaskById(int umrahTaskId);
        bool UpdateUmrahTask(UmrahTask umrahTask);
        UmrahTask GetUmrahTaskById(int umrahTaskId);
        Result<UmrahTaskListDTO> GetAllUmrahTasks(int pageIndex, int pageSize, string userId);
    }
}