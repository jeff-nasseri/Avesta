using Avesta.Repository.Test.Src.Data.Context;
using Avesta.Repository.Test.Src.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Avesta.Repository.Test.Src
{

    public class Program
    {

        public static ServiceProvider Builder;

        public static void Start() => Main(new string[] { "runing from start point" });

        public static void Main(string[] args)
        {

            Builder = new ServiceCollection()
                 .AddDbContext<ApplicationDbContext>()
                 //.RegisterRepository<string, ApplicationDbContext>(Assembly.GetExecutingAssembly().ManifestModule.Name)
                 .BuildServiceProvider();


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
