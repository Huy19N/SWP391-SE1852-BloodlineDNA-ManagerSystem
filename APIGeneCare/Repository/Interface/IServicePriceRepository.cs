﻿using APIGeneCare.Data;

namespace APIGeneCare.Repository.Interface
{
    public interface IServicePriceRepository
    {
        IEnumerable<ServicePrice> GetAllServicePrices(string? typeSearch, string? search, string? sortBy, int? page);
        ServicePrice? GetServicePriceById(int id);
        bool CreateServicePrice(ServicePrice servicePrice);
        bool UpdateServicePrice(ServicePrice servicePrice);
        bool DeleteServicePrice(int id);
    }
}
