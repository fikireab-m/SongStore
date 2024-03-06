using Microsoft.EntityFrameworkCore;

namespace SongStore.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var dBContext = scope.ServiceProvider.GetRequiredService<SongStoreContext>();
        dBContext.Database.Migrate();
    }
}
