using Avesta.Attribute.Schema;
using Avesta.MiddleWare;
using Avesta.Model.Shema;
using Avesta.Share.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;






var viewModel = new User
{
    Email = "test1@yahoo.com",
    UserName = "username_test1"
};
var user = new User
{
    Email = "user@yahoo.com",
    UserName = "user",
    Adress = "adress",
    Phone = "-00389493",
    Age = 12
};


var result = user.UpdateBy<User>(viewModel);
Console.WriteLine();


public class User
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string? Phone { get; set; }
    public string? Adress { get; set; }
    public int? Age { get; set; }
}

public class UserViewModel
{
    public string Email { get; set; }
    public string UserName { get; set; }
}




//var builder = WebApplication.CreateBuilder(args);





//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<TestSchema>();



//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();
//app.UseMiddleware<CatchRequestSchema<TestSchema>>();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();



//public class TestSchema : HttpSchema 
//{
//    [RequestCookie("SSO_jwt")]
//    public string SSO { get; set; }
//}

