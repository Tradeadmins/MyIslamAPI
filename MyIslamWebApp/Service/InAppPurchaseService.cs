using System;
using System.Collections.Generic;
using MyIslamWebApp.Models;
using MyIslamWebApp.UnitOfWork;
using MyIslamWebApp.DataTransferObjects.Result;
using MyIslamWebApp.DataTransferObjects.InAppPurchase;

namespace MyIslamWebApp.Service
{
    public class InAppPurchaseService : IInAppPurchaseService
    {
        #region Properties
        //Replace with an IOC container (Instance created using Unity Container at UnityConfig.cs in App_Start)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public InAppPurchaseService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public InAppPurchaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region InAppPurchase Methods
        public bool AddInAppPurchase(InAppPurchaseDTO inAppPurchase, string userId)
        {
            try
            {
                var result = false;
                if (inAppPurchase == null)
                    return result;

                var addInAppPurchase = new InAppPurchase();
                addInAppPurchase.InAppPurchaseId = inAppPurchase.InAppPurchaseId;
                addInAppPurchase.InAppPurchaseByUserId = inAppPurchase.InAppPurchaseByUserId;
                addInAppPurchase.InAppPurchaseTotalAmount = inAppPurchase.InAppPurchaseTotalAmount;
                addInAppPurchase.InAppPurchaseOwnerAmount = inAppPurchase.InAppPurchaseOwnerAmount;
                addInAppPurchase.InAppPurchaseUserAmount = inAppPurchase.InAppPurchaseUserAmount;

                //Default Values
                addInAppPurchase.IsActive = true;
                addInAppPurchase.CreatedBy = userId;
                addInAppPurchase.CreatedOn = DateTime.Now;
                addInAppPurchase.UpdatedBy = userId;
                addInAppPurchase.UpdatedOn = DateTime.Now;
                result = _unitOfWork.inAppPurchaseRepository.AddInAppPurchase(addInAppPurchase);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool UpdateInAppPurchase(InAppPurchaseDTO inAppPurchase, string userId)
        {
            try
            {
                var result = false;
                if (inAppPurchase == null)
                    return result;

                var _inAppPurchases = _unitOfWork.inAppPurchaseRepository.GetInAppPurchaseById(inAppPurchase.InAppPurchaseId);

                if (_inAppPurchases == null)
                    return result;

                var updateInAppPurchase = new InAppPurchase();
                updateInAppPurchase.InAppPurchaseId = inAppPurchase.InAppPurchaseId;
                updateInAppPurchase.InAppPurchaseByUserId = inAppPurchase.InAppPurchaseByUserId;
                updateInAppPurchase.InAppPurchaseTotalAmount = inAppPurchase.InAppPurchaseTotalAmount;
                updateInAppPurchase.InAppPurchaseOwnerAmount = inAppPurchase.InAppPurchaseOwnerAmount;
                updateInAppPurchase.InAppPurchaseUserAmount = inAppPurchase.InAppPurchaseUserAmount;

                result = _unitOfWork.inAppPurchaseRepository.UpdateInAppPurchase(updateInAppPurchase);

                ////Default Values
                updateInAppPurchase.IsActive = _inAppPurchases.IsActive;
                updateInAppPurchase.CreatedBy = _inAppPurchases.CreatedBy;
                updateInAppPurchase.CreatedOn = _inAppPurchases.CreatedOn;
                updateInAppPurchase.UpdatedBy = userId;
                updateInAppPurchase.UpdatedOn = DateTime.Now;

                result = _unitOfWork.inAppPurchaseRepository.UpdateInAppPurchase(updateInAppPurchase);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteInAppPurchase(InAppPurchaseDTO inAppPurchase, string userId)
        {
            try
            {
                var result = false;
                if (inAppPurchase == null)
                    return result;

                var deleteInAppPurchase = new InAppPurchase();
                deleteInAppPurchase.InAppPurchaseId = inAppPurchase.InAppPurchaseId;
                deleteInAppPurchase.InAppPurchaseByUserId = inAppPurchase.InAppPurchaseByUserId;
                deleteInAppPurchase.InAppPurchaseTotalAmount = inAppPurchase.InAppPurchaseTotalAmount;
                deleteInAppPurchase.InAppPurchaseOwnerAmount = inAppPurchase.InAppPurchaseOwnerAmount;
                deleteInAppPurchase.InAppPurchaseUserAmount = inAppPurchase.InAppPurchaseUserAmount;


                ////Default Values
                deleteInAppPurchase.IsActive = false;
                deleteInAppPurchase.UpdatedBy = userId;
                deleteInAppPurchase.UpdatedOn = DateTime.Now;

                result = _unitOfWork.inAppPurchaseRepository.DeleteInAppPurchase(deleteInAppPurchase);
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteInAppPurchaseById(int inAppPurchaseID, string userId)
        {
            try
            {
                var result = false;
                if (inAppPurchaseID == 0)
                    return result;

                var inAppPurchase = _unitOfWork.inAppPurchaseRepository.GetInAppPurchaseById(inAppPurchaseID);

                if (inAppPurchase == null)
                    return result;

                var deleteinAppPurchase = new InAppPurchase();
                deleteinAppPurchase = inAppPurchase;
                ////Default Values
                deleteinAppPurchase.IsActive = false;
                deleteinAppPurchase.UpdatedBy = userId;
                deleteinAppPurchase.UpdatedOn = DateTime.Now;

                result = _unitOfWork.inAppPurchaseRepository.DeleteInAppPurchase(deleteinAppPurchase);
                _unitOfWork.Commit();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InAppPurchaseDTO GetInAppPurchaseById(int inAppPurchaseID)
        {
            try
            {
                InAppPurchaseDTO inAppPurchaseResponse = new InAppPurchaseDTO();
                var inAppPurchase = _unitOfWork.inAppPurchaseRepository.GetInAppPurchaseById(inAppPurchaseID);
                if (inAppPurchase == null)
                    return null;

                if (inAppPurchase.IsActive)
                {
                    inAppPurchaseResponse.InAppPurchaseId = inAppPurchase.InAppPurchaseId;
                    inAppPurchaseResponse.InAppPurchaseByUserId = inAppPurchase.InAppPurchaseByUserId;
                    inAppPurchaseResponse.InAppPurchaseTotalAmount = inAppPurchase.InAppPurchaseTotalAmount;
                    inAppPurchaseResponse.InAppPurchaseOwnerAmount = inAppPurchase.InAppPurchaseOwnerAmount;
                    inAppPurchaseResponse.InAppPurchaseUserAmount = inAppPurchase.InAppPurchaseUserAmount;

                }
                return inAppPurchaseResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDTO<InAppPurchaseDTO> GetAllInAppPurchases(int pageIndex, int pageSize)
        {
            try
            {
                var response = new ResultDTO<InAppPurchaseDTO>();
                var inAppPurchasesList = new List<InAppPurchaseDTO>();
                InAppPurchaseDTO inAppPurchaseResponse;
                var allInAppPurchases = _unitOfWork.inAppPurchaseRepository.GetAllInAppPurchases(pageIndex, pageSize);

                if (allInAppPurchases.TotalCount == 0)
                    return response;

                foreach (var inAppPurchase in allInAppPurchases.Response)
                {
                    if (inAppPurchase.IsActive)
                    {
                        inAppPurchaseResponse = new InAppPurchaseDTO();
                        inAppPurchaseResponse.InAppPurchaseId = inAppPurchase.InAppPurchaseId;
                        inAppPurchaseResponse.InAppPurchaseByUserId = inAppPurchase.InAppPurchaseByUserId;
                        inAppPurchaseResponse.InAppPurchaseTotalAmount = inAppPurchase.InAppPurchaseTotalAmount;
                        inAppPurchaseResponse.InAppPurchaseOwnerAmount = inAppPurchase.InAppPurchaseOwnerAmount;
                        inAppPurchaseResponse.InAppPurchaseUserAmount = inAppPurchase.InAppPurchaseUserAmount;

                        inAppPurchasesList.Add(inAppPurchaseResponse);
                    }
                }
                response.Response = inAppPurchasesList;
                response.TotalCount = allInAppPurchases.TotalCount;
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