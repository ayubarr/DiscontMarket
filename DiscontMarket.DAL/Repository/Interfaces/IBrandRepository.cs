﻿using DiscontMarket.ApiModels.DTO.EntityDTOs.Brand;
using DiscontMarket.Domain.Models.Entities;

namespace DiscontMarket.DAL.Repository.Interfaces
{
    public interface IBrandRepository : IBrandRepository<Brand>
    {
        Dictionary<string, Dictionary<string, List<FilterAtributeAndBrandDTO>>> GetAllBrandsGroupedByCategory();

    }
}
