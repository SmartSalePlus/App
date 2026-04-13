using SmartSaleApp.Models.Entities;
using SQLite;

namespace SmartSaleApp.Helpers;

public static class JwtHelper {
    private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, "SmartSale.db");

    public static void InitializeDatabase() {
        using var connection = new SQLiteConnection(DbPath);
        connection.CreateTable<Token>();
    }

    public static void SetToken(string value) {
        var token = new Token() { Id = 1, Value = value};
        using var connection = new SQLiteConnection(DbPath);
        //connection.DeleteAll<Token>();
        connection.InsertOrReplace(token);
    }

    public static string? GetToken() {
        using var connection = new SQLiteConnection(DbPath);
        return connection.Table<Token>().FirstOrDefault()?.Value;
    }

    //public static async Task<string?> GetTokenAsync() {
    //    return await SecureStorage.Default.GetAsync("JwtToken");
    //}

    //public static async Task SetTokenAsync(string token) {
    //    await SecureStorage.Default.SetAsync("JwtToken", token);
    //}
}