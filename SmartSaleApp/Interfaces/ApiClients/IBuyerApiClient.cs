using Refit;
using SmartSaleApp.Models;

namespace SmartSaleApp.Interfaces.ApiClients;

public interface IBuyerApiClient {
    [Post("/add")]
    Task AddAsync(Buyer buyer);

    [Put("/update")]
    Task UpdateAsync(Buyer buyer);

    [Delete("/delete/{id}")]
    Task DeleteAsync(int id);

    [Get("/get/{id}")]
    Task<Buyer> GetAsync(int id);

    [Get("/get/name/{name}")]
    Task<IEnumerable<Buyer>> GetAsync(string name);

    [Get("/get")]
    Task<IEnumerable<Buyer>> GetAsync();
}