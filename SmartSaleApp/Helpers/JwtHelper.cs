namespace SmartSaleApp.Helpers;

public static class JwtHelper {
    public static async Task<string?> GetTokenAsync() {
        return await SecureStorage.Default.GetAsync("JwtToken");
    }

    public static async Task SetTokenAsync(string token) {
        await SecureStorage.Default.SetAsync("JwtToken", token);
    }
}