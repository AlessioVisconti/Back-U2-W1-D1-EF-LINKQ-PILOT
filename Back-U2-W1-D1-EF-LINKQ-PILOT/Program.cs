using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;
using Back_U2_W1_D1_EF_LINKQ_PILOT.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add service to DI
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BorrowingService>();


builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer( // connect to SQL Server
        builder.Configuration.GetConnectionString("DefaultConnection") // get connection string from configuration
    )
);

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

app.Run();
