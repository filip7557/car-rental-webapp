﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class CompanyVehicleService : ICompanyVehicleService
    {
        private ICompanyVehicleRepository _repository;

        public CompanyVehicleService(ICompanyVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CompanyVehicle>> GetAllCompanyVehiclesAsync(BookingSorting sorting, Paging paging, CompanyVehicleFilter filter)
        {
            return await _repository.GetAllCompanyVehiclesAsync(sorting, paging, filter);
        }

        public async Task<CompanyVehicle> GetCompanyVehicleByIdAsync(Guid id)
        {
            return await _repository.GetCompanyVehicleByIdAsync(id);
        }

        public async Task AddCompanyVehicleAsync(CompanyVehicle companyVehicle)
        {
            await _repository.AddCompanyVehicleAsync(companyVehicle);
        }

        public async Task UpdateCompanyVehicleAsync(Guid id, CompanyVehicle updatedCompanyVehicle)
        {
            await _repository.UpdateCompanyVehicleAsync(id, updatedCompanyVehicle);
        }

        public async Task DeleteCompanyVehicleAsync(Guid id)
        {
            await _repository.DeleteCompanyVehicleAsync(id);
        }
    }
}
