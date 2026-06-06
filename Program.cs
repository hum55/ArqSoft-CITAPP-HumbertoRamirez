using CitasApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<JsonData>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value ?? string.Empty;
    var isAuthPath = path.StartsWith("/Auth", StringComparison.OrdinalIgnoreCase);
    var isStaticPath = path.StartsWith("/css", StringComparison.OrdinalIgnoreCase)
        || path.StartsWith("/js", StringComparison.OrdinalIgnoreCase)
        || path.StartsWith("/lib", StringComparison.OrdinalIgnoreCase)
        || path.StartsWith("/favicon", StringComparison.OrdinalIgnoreCase);

    if (!isAuthPath && !isStaticPath && context.Session.GetString("AdminEmail") == null)
    {
        context.Response.Redirect("/Auth/Login");
        return;
    }

    await next();
});
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();