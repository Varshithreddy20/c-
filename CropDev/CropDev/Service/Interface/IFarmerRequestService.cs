﻿using CropDev.Models.FarmerRequest;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CropDev.Service.Interface
{
    public interface IFarmerRequestService
    {
        Task<ResultStatus> SoftDelete(int Id, string updatedBy);
        Task<ResultStatus> Restore(int Id, string updatedBy);
        Task<FarmerRequest> GetById(int farmerRequestId);
        Task<ResultStatus> Update(UpdateFarmerRequest updateFarmerRequest);
        Task<ResultStatus> Create(CreateFarmerRequest createFarmerRequest);
        Task<List<FarmerRequest>> GetAll();
    }
}
