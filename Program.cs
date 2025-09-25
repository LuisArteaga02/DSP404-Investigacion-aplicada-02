using InvestigacionAplicada02.Data;
using InvestigacionAplicada02.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<BibliotecaService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");



try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var canConnect = await context.Database.CanConnectAsync();
    Console.WriteLine(canConnect ? " Conectado a tablas existentes" : " Error de conexión");

    if (canConnect)
    {
        // Verificar que las tablas existen
        var usuariosCount = await context.Usuarios.CountAsync();
        var materialesCount = await context.Libros.CountAsync() + await context.Revistas.CountAsync();

        Console.WriteLine($"📊 Usuarios: {usuariosCount}, Materiales: {materialesCount}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($" Error: {ex.Message}");
}

app.Run();