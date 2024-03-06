using Microsoft.EntityFrameworkCore;

namespace SongStore.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var dBContext = scope.ServiceProvider.GetRequiredService<SongStoreContext>();
        await dBContext.Database.MigrateAsync();
    }
}
