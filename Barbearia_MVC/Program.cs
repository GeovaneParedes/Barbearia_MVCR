using Microsoft.EntityFrameworkCore;
using Barbearia_MVC.Data;

// 1. Importa as suas pastas de Repositórios organizada por subpastas
using Barbearia_MVC.Repositories.Appointment;
using Barbearia_MVC.Repositories.Barber;
using Barbearia_MVC.Repositories.Customer;
using Barbearia_MVC.Repositories.Payment;
using Barbearia_MVC.Repositories.Service;

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// CONFIGURAÇÃO DOS SERVIÇOS DA APLICAÇÃO (CONTAINER DE INJEÇÃO DE DEPENDÊNCIA)
// =========================================================================

// Adiciona o suporte para Controladores e Views do padrão MVC
builder.Services.AddControllersWithViews();

// 2. Configura o Entity Framework Core para usar o SQL Server (Ou mude para UseMySql / UseSqlite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. INJEÇÃO DE DEPENDÊNCIA: Liga as suas Interfaces (Menus) às Classes Reais (Chefs)
// O padrão Scoped cria uma instância por requisição web, garantindo alta performance.
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IBarberRepository, BarberRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

var app = builder.Build();

// =========================================================================
// CONFIGURAÇÃO DO PIPELINE DE REQUISIÇÕES HTTP (MIDDLEWARES)
// =========================================================================

// Configura o comportamento do ambiente (Produção vs Desenvolvimento)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Permite carregar o Bootstrap, CSS e JS das suas Views

app.UseRouting();
app.UseAuthorization();

// 4. MAPEAMENTO DE ROTAS: Define qual tela abre primeiro quando o sistema inicia
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Appointment}/{action=Index}/{id?}"); // Inicia direto no painel da Agenda!

app.Run();
