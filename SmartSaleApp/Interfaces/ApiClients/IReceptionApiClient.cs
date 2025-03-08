using Refit;
using SmartSaleApp.Models;

namespace SmartSaleApp.Interfaces.ApiClients;

internal interface IReceptionApiClient {
    [Post("/add")]
    Task AddAsync(Reception reception);

    [Put("/update")]
    Task UpdateAsync(Reception reception);

    [Delete("/delete/{id}")]
    Task DeleteAsync(int id);

    [Get("/get/{id}")]
    Task<Reception> GetAsync(int id);

    [Get("/get/{date}")]
    Task<IEnumerable<Reception>> GetAsync(DateOnly date);

    [Get("/getByProduct/{productId}")]
    Task<IEnumerable<Reception>> GetByProductAsync(int productId);

    [Get("/get")]
    Task<IEnumerable<Reception>> GetAsync();
}