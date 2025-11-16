using BibliotecaDigital.Application.Interfaces;
using BibliotecaDigital.Application.Services;
using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Infrastructure.Data;
using BibliotecaDigital.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Configurar DbContext
builder.Services.AddDbContext<BibliotecaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependências - Repositórios
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();

// Injeção de Dependências - Serviços
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ILivroService, LivroService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// REMOVIDO: app.UseAuthorization(); ← Esta linha causava o erro 401

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
