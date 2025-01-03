using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Data;
using TI_Grupo7.Areas.Identity.Data;
using TI_Projeto_Grupo7.Helpers;
using Microsoft.Extensions.Options;
using TI_Projeto_Grupo7.Services;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using TI_Projeto_Grupo7.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BankDbContextConnection") ?? throw new InvalidOperationException("Connection string 'BankDbContextConnection' not found.");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login"; 
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Worker", policy => policy.RequireRole("Worker"));
});

builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<IEmailService>(new EmailService(
    smtpHost: "smtp.gmail.com",
    smtpPort: 587,
    fromEmail: "noreplytigrupo7@gmail.com",
    password: "xzzz jimj adtu pufr"
));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<BankDbContext>();

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

using (var scope = app.Services.CreateScope()){

    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try{

        var context = services.GetRequiredService<BankDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await ContextSeed.SeedRolesAsync(userManager, roleManager);
        await ContextSeed.SeedAdminAsync(userManager, roleManager);
    
    }catch(Exception ex){

        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured seeding the DataBase.");
    }
}

app.Run();
