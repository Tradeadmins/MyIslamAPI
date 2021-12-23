using System;
using System.Collections.Generic;
using System.Linq;
using MyIslamWebApp.DataTransferObjects.UmrahTask;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;

namespace MyIslamWebApp.Service
{
    public class UmrahTaskService : IUmrahTaskService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public UmrahTaskService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public UmrahTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region UmrahTask Methods
        public bool AddUmrahTask(UmrahTaskDTO umrahTask, string userId)
        {
            try
            {
                var result = false;

                if (umrahTask == null)
                    return result;

                var addUmrahTask = new UmrahTask();
                addUmrahTask.UmrahTaskId = umrahTask.UmrahTaskId;
                addUmrahTask.UmrahTaskName = umrahTask.UmrahTaskName;
                //Default Values
                addUmrahTask.IsActive = true;
                addUmrahTask.CreatedBy = userId;
                addUmrahTask.CreatedOn = DateTime.Now;
                addUmrahTask.UpdatedBy = userId;
                addUmrahTask.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahTaskRepository.AddUmrahTask(addUmrahTask);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateUmrahTask(UmrahTaskDTO umrahTask, string userId)
        {
            try
            {
                var result = false;
                if (umrahTask == null)
                    return result;

                var _umrahTask = _unitOfWork.umrahTaskRepository.GetUmrahTaskById(umrahTask.UmrahTaskId);

                if (_umrahTask == null)
                    return result;

                var updateUmrahTask = new UmrahTask();
                updateUmrahTask.UmrahTaskId = umrahTask.UmrahTaskId;
                updateUmrahTask.UmrahTaskName = umrahTask.UmrahTaskName;

                ////Default Values
                updateUmrahTask.IsActive = _umrahTask.IsActive;
                updateUmrahTask.CreatedBy = _umrahTask.CreatedBy;
                updateUmrahTask.CreatedOn = _umrahTask.CreatedOn;
                updateUmrahTask.UpdatedBy = userId;
                updateUmrahTask.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahTaskRepository.UpdateUmrahTask(updateUmrahTask);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUmrahTask(UmrahTaskDTO umrahTask, string userId)
        {
            try
            {
                var result = false;
                if (umrahTask == null)
                    return result;

                var deleteUmrahTask = new UmrahTask();
                deleteUmrahTask.UmrahTaskId = umrahTask.UmrahTaskId;
                deleteUmrahTask.UmrahTaskName = umrahTask.UmrahTaskName;

                deleteUmrahTask.IsActive = false;
                deleteUmrahTask.UpdatedBy = userId;
                deleteUmrahTask.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahTaskRepository.DeleteUmrahTask(deleteUmrahTask);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteUmrahTaskById(int umrahTaskID, string userId)
        {
            try
            {
                var result = false;
                if (umrahTaskID == 0)
                    return result;

                var umrahTask = _unitOfWork.umrahTaskRepository.GetUmrahTaskById(umrahTaskID);

                if (umrahTask == null)
                    return result;

                var deleteUmrahTask = new UmrahTask();
                deleteUmrahTask = umrahTask;

                ////Default Values
                deleteUmrahTask.IsActive = false;
                deleteUmrahTask.UpdatedBy = userId;
                deleteUmrahTask.UpdatedOn = DateTime.Now;

                result = _unitOfWork.umrahTaskRepository.DeleteUmrahTask(deleteUmrahTask);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UmrahTaskDTO GetUmrahTaskById(int umrahTaskID)
        {
            try
            {
                UmrahTaskDTO umrahTaskResponse = new UmrahTaskDTO();
                var umrahTask = _unitOfWork.umrahTaskRepository.GetUmrahTaskById(umrahTaskID);
                if (umrahTask == null)
                    return null;

                if (umrahTask.IsActive)
                {
                    umrahTaskResponse.UmrahTaskId = umrahTask.UmrahTaskId;
                    umrahTaskResponse.UmrahTaskName = umrahTask.UmrahTaskName;
                }
                return umrahTaskResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<UmrahTaskListDTO> GetAllUmrahTasks(int pageIndex, int pageSize, string userId)
        {
            try
            {
                var response = new ResultDTO<UmrahTaskListDTO>();
                List<UmrahTaskListDTO> umrahTaskDTOList = new List<UmrahTaskListDTO>();
                UmrahTaskListDTO umrahTaskResponse;

                var allUmrahTasks = _unitOfWork.umrahTaskRepository.GetAllUmrahTasks(pageIndex, pageSize, userId);

                if (allUmrahTasks == null)
                    return response;

                foreach (var umrahTask in allUmrahTasks.Response)
                {
                    umrahTaskResponse = new UmrahTaskListDTO();
                    umrahTaskResponse.UmrahTaskId = umrahTask.UmrahTaskId;
                    umrahTaskResponse.UmrahTaskName = umrahTask.UmrahTaskName;
                    umrahTaskResponse.UmrahTaskCompleteId = umrahTask.UmrahTaskCompleteId;
                    umrahTaskResponse.UmrahTaskIsCompleted = umrahTask.UmrahTaskIsCompleted;
                    umrahTaskDTOList.Add(umrahTaskResponse);
                }
                response.Response = umrahTaskDTOList;
                response.TotalCount = allUmrahTasks.TotalCount;
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