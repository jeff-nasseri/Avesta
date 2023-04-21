using Avesta.Graph;
using Avesta.Graph.Test.Src.Data.Context;
using Avesta.Graph.Test.Src.Storage;
using Avesta.Repository;
using Avesta.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Avesta.Graph.Test.Src
{
    public class Program
    {

        public static ServiceProvider Builder;

        public static void Start() => Main(new string[] { "runing from start point" });

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.RegisterAvestaGraph();
            builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.RegisterRepository<string, ApplicationDbContext>(Assembly.GetExecutingAssembly().ManifestModule.Name);
            builder.Services.RegisterService<string>(Assembly.GetExecutingAssembly().ManifestModule.Name);
            builder.Services.RegisterRepository<string, ApplicationDbContext>(Assembly.GetExecutingAssembly().ManifestModule.Name);




            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());


            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();



            var dbContext = Builder.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();


            //init school
            foreach (var school in SeedStorage.Schools)
            {
                dbContext.Schools.Add(school);
            }

            //init student
            foreach (var student in SeedStorage.Students)
            {
                dbContext.Students.Add(student);
            }


            //init teacher
            foreach (var teacher in SeedStorage.Teachers)
            {
                dbContext.Teachers.Add(teacher);
            }

            //assign teacher to school
            foreach (var teacher_school in SeedStorage.Teacher_Schools)
            {
                dbContext.Teacher_Schools.Add(teacher_school);
            }

            dbContext.SaveChanges();


        }
    }
}