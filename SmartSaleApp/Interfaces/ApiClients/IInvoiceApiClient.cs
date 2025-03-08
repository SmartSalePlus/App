using Refit;
using SmartSaleApp.Models;

namespace SmartSaleApp.Interfaces.ApiClients;

public interface IInvoiceApiClient {
    [Post("/add")]
    Task AddAsync(Invoice invoice);

    [Put("/update")]
    Task UpdateAsync(Invoice invoice);

    [Delete("/delete/{id}")]
    Task DeleteAsync(int id);

    [Get("/get/{id}")]
    Task<Invoice> GetAsync(int id);

    [Get("/get/{date}")]
    Task<IEnumerable<Invoice>> GetAsync(DateOnly date);

    [Get("/getByBuyer/{buyerId}")]
    Task<IEnumerable<Invoice>> GetByBuyerAsync(int buyerId);

    [Get("/get")]
    Task<IEnumerable<Invoice>> GetAsync();
}