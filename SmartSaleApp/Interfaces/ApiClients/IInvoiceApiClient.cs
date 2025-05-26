using Refit;
using SmartSaleApp.Models.Data;
using SmartSaleApp.Models.InputParameters;
using SmartSaleApp.Models.View;

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

    [Post("/get")]
    Task<IEnumerable<InvoiceDto>> GetAsync(InvoiceInputParameter parameter);

    [Get("/get")]
    Task<IEnumerable<Invoice>> GetAsync();
}