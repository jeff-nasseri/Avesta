using Avesta.Attribute.Schema;
using Avesta.MiddleWare;
using Avesta.Model.Shema;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

var builder = WebApplication.CreateBuilder(args);





// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<TestSchema>();



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
app.UseMiddleware<CatchRequestSchema<TestSchema>>();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



public class TestSchema : HttpSchema 
{
    [RequestCookie("SSO_jwt")]
    public string SSO { get; set; }
}

