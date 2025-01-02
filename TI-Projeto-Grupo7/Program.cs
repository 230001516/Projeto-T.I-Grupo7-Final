using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Data;
using TI_Grupo7.Areas.Identity.Data;
using TI_Projeto_Grupo7.Helpers;
using Microsoft.Extensions.Options;
using TI_Projeto_Grupo7.Services;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BankDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BankDbContextConnection' not found.");

builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<IEmailService>(new EmailService(
    smtpHost: "smtp.gmail.com",
    smtpPort: 587,
    fromEmail: "noreplytigrupo7@gmail.com",
    password: "xzzz jimj adtu pufr"
));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BankDbContext>();

var sinkOptions = new MSSqlServerSinkOptions
{
    TableName = "Logs",
    AutoCreateSqlTable = true 
};

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Temporario, apenas para debug
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: sinkOptions
    )
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Add services to the container.
builder.Services.AddScoped<UsersService>();
builder.Services.AddTransient<TransferService>();
builder.Services.AddScoped<PendingAccountsService>();
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
