using InvestigacionAplicada02.Data;
using InvestigacionAplicada02.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

try
{
    // Configurar Entity Framework con manejo de errores
    var connectionString = builder.Configuration.GetConnectionString("AzureBibliotecaConnection");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new Exception("No se encontró la cadena de conexión 'AzureBibliotecaConnection'");
    }

    Console.WriteLine($"🔗 Cadena de conexión: {connectionString}");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        });

        // Log para desarrollo
        if (builder.Environment.IsDevelopment())
        {
            options.LogTo(Console.WriteLine, LogLevel.Information);
        }
    });
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error configurando la base de datos: {ex.Message}");
    throw;
}


builder.Services.AddScoped<BibliotecaService, BibliotecaService2>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Probar conexión al iniciar
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        var canConnect = await context.Database.CanConnectAsync();
        if (canConnect)
        {
            Console.WriteLine("✅ Conexión a Azure SQL Database exitosa");
            await context.Database.MigrateAsync();
        }
        else
        {
            Console.WriteLine("❌ No se pudo conectar a la base de datos");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error de conexión: {ex.Message}");
    }
}

app.Run();