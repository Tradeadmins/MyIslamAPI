using System.Collections.Generic;
using MyIslamWebApp.Repository.Base;
using MyIslamWebApp.Models;
using MyIslamWebApp.DataTransferObjects.HajjTask;

namespace MyIslamWebApp.Repository.HajjTasks
{
    public interface IHajjTaskRepository : IRepository<HajjTask>
    {
        bool AddHajjTask(HajjTask hajjTask);
        bool DeleteHajjTask(HajjTask hajjTask);
        bool DeleteHajjTaskById(int hajjTaskId);
        bool UpdateHajjTask(HajjTask hajjTask);
        HajjTask GetHajjTaskById(int hajjTaskId);
        Result<HajjTaskListDTO> GetAllHajjTasks(int pageIndex, int pageSize, string userId);       
    }
}