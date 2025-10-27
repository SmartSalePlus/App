using Refit;
using SmartSaleApp.Models.Data;

namespace SmartSaleApp.Interfaces.ApiClients;

public interface ISecurityApiClient {
    [Post("/login")]
    Task<string> LoginAsync(User user);

    [Get("/validateToken")]
    Task ValidateTokenAsync();
}