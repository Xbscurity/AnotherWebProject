using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Data;
using MyFirstWebApp.Seeds;
using MyFirstWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false; // Отключаем требование цифры
    options.Password.RequireLowercase = false; // Отключаем требование для нижнего регистра
    options.Password.RequireUppercase = false; // Отключаем требование для верхнего регистра
    options.Password.RequireNonAlphanumeric = false; // Отключаем требование для неалфавитных символов
    options.Password.RequiredLength = 6; // Минимальная длина пароля
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    ApplicationDbInitializer.InitializeProductCategory(context); // Инициализация категорий
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
