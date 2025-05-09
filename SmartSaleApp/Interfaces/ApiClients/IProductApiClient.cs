using Refit;
using SmartSaleApp.Models.Data;

namespace SmartSaleApp.Interfaces.ApiClients;

public interface IProductApiClient {
    [Post("/add")]
    Task AddAsync(Product product);

    [Put("/update")]
    Task UpdateAsync(Product product);

    [Delete("/delete/{id}")]
    Task DeleteAsync(int id);

    [Get("/get/{id}")]
    Task<Product> GetAsync(int id);

    [Get("/get/{name}")]
    Task<IEnumerable<Product>> GetAsync(string name);

    [Get("/get")]
    Task<IEnumerable<Product>> GetAsync();
}