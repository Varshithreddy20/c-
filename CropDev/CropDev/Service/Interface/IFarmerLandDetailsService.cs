﻿using CropDev.Models;
using CropDev.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using CropDev.Models.FarmerLadDetails;

namespace CropDev.Service.Interface
{
    public interface IFarmerLandDetailsService
    {
        Task<ResultStatus> SoftDelete(int farmerLandDetailsId, string updatedBy);
        Task<ResultStatus> Restore(int farmerLandDetailsId, string updatedBy);
        Task<FarmerLandDetails> GetById(int farmerLandDetailsId);
        Task<ResultStatus> Update(UpdateFarmerLandDetails updateFarmerLandDetails);
        Task<ResultStatus> Create(CreateFarmerLandDetails createFarmerLandDetails);
        Task<List<FarmerLandDetails>> GetAll();
    }
}
