using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Data;
using TI_Grupo7.Areas.Identity.Data;
using TI_Projeto_Grupo7.Helpers;
using Microsoft.Extensions.Options;
using TI_Projeto_Grupo7.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BankDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BankDbContextConnection' not found.");

builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BankDbContext>();

// Add services to the container.
builder.Services.AddScoped<UsersService>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.Configure<MyOptions>(myOptions =>
{
    myOptions.ConnString = connectionString;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
