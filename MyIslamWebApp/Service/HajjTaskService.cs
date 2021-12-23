using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.HajjTask;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class HajjTaskService : IHajjTaskService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public HajjTaskService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public HajjTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region HajjTask Methods
        public bool AddHajjTask(HajjTaskDTO hajjTask, string userId)
        {
            try
            {
                var result = false;

                if (hajjTask == null)
                    return result;

                var addHajjTask = new HajjTask();
                addHajjTask.HajjTaskId = hajjTask.HajjTaskId;
                addHajjTask.HajjTaskName = hajjTask.HajjTaskName;
                //Default Values
                addHajjTask.IsActive = true;
                addHajjTask.CreatedBy = userId;
                addHajjTask.CreatedOn = DateTime.Now;
                addHajjTask.UpdatedBy = userId;
                addHajjTask.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjTaskRepository.AddHajjTask(addHajjTask);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateHajjTask(HajjTaskDTO hajjTask, string userId)
        {
            try
            {
                var result = false;
                if (hajjTask == null)
                    return result;

                var _hajjTask = _unitOfWork.hajjTaskRepository.GetHajjTaskById(hajjTask.HajjTaskId);

                if (_hajjTask == null)
                    return result;

                var updateHajjTask = new HajjTask();
                updateHajjTask.HajjTaskId = hajjTask.HajjTaskId;
                updateHajjTask.HajjTaskName = hajjTask.HajjTaskName;

                ////Default Values
                updateHajjTask.IsActive = _hajjTask.IsActive;
                updateHajjTask.CreatedBy = _hajjTask.CreatedBy;
                updateHajjTask.CreatedOn = _hajjTask.CreatedOn;
                updateHajjTask.UpdatedBy = userId;
                updateHajjTask.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjTaskRepository.UpdateHajjTask(updateHajjTask);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteHajjTask(HajjTaskDTO hajjTask, string userId)
        {
            try
            {
                var result = false;
                if (hajjTask == null)
                    return result;

                var deleteHajjTask = new HajjTask();
                deleteHajjTask.HajjTaskId = hajjTask.HajjTaskId;
                deleteHajjTask.HajjTaskName = hajjTask.HajjTaskName;

                deleteHajjTask.IsActive = false;
                deleteHajjTask.UpdatedBy = userId;
                deleteHajjTask.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjTaskRepository.DeleteHajjTask(deleteHajjTask);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteHajjTaskById(int hajjTaskID, string userId)
        {
            try
            {
                var result = false;
                if (hajjTaskID == 0)
                    return result;

                var hajjTask = _unitOfWork.hajjTaskRepository.GetHajjTaskById(hajjTaskID);

                if (hajjTask == null)
                    return result;

                var deleteHajjTask = new HajjTask();
                deleteHajjTask = hajjTask;

                ////Default Values
                deleteHajjTask.IsActive = false;
                deleteHajjTask.UpdatedBy = userId;
                deleteHajjTask.UpdatedOn = DateTime.Now;

                result = _unitOfWork.hajjTaskRepository.DeleteHajjTask(deleteHajjTask);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HajjTaskDTO GetHajjTaskById(int hajjTaskID)
        {
            try
            {
                HajjTaskDTO hajjTaskResponse = new HajjTaskDTO();
                var hajjTask = _unitOfWork.hajjTaskRepository.GetHajjTaskById(hajjTaskID);
                if (hajjTask == null)
                    return null;

                if (hajjTask.IsActive)
                {
                    hajjTaskResponse.HajjTaskId = hajjTask.HajjTaskId;
                    hajjTaskResponse.HajjTaskName = hajjTask.HajjTaskName;
                }
                return hajjTaskResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<HajjTaskListDTO> GetAllHajjTasks(int pageIndex, int pageSize, string userId)
        {
            try
            {
                var response = new ResultDTO<HajjTaskListDTO>();
                List<HajjTaskListDTO> hajjTaskDTOList = new List<HajjTaskListDTO>();
                HajjTaskListDTO hajjTaskResponse;

                var allHajjTasks = _unitOfWork.hajjTaskRepository.GetAllHajjTasks(pageIndex, pageSize, userId);

                if (allHajjTasks == null)
                    return response;

                foreach (var hajjTask in allHajjTasks.Response)
                {
                    hajjTaskResponse = new HajjTaskListDTO();
                    hajjTaskResponse.HajjTaskId = hajjTask.HajjTaskId;
                    hajjTaskResponse.HajjTaskName = hajjTask.HajjTaskName;
                    hajjTaskResponse.HajjTaskCompleteId = hajjTask.HajjTaskCompleteId;
                    hajjTaskResponse.HajjTaskIsCompleted = hajjTask.HajjTaskIsCompleted;
                    hajjTaskDTOList.Add(hajjTaskResponse);
                }
                response.Response = hajjTaskDTOList;
                response.TotalCount = allHajjTasks.TotalCount;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}