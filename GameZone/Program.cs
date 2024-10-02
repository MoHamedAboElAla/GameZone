var builder = WebApplication.CreateBuilder(args);

var ConnStr = builder.Configuration.GetConnectionString("Constr") ??
        throw new InvalidOperationException("No Connection was found");

builder.Services.AddDbContext<AppDbContext>(
options=>options.UseSqlServer(ConnStr));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
builder.Services.AddScoped<IDevicesServices, DevicesServices>();
builder.Services.AddScoped<IGameServices, GameServices>();

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
