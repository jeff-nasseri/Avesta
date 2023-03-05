﻿using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.Test.Src.Data.Context;
using Avesta.Repository.Test.Src.Data.Model;
using Avesta.Repository.Test.Src.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;


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
                 .RegisterRepository<ApplicationDbContext>(Assembly.GetExecutingAssembly().ManifestModule.Name)
                 .BuildServiceProvider();


            var dbContext = Builder.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();


            //init student
            foreach (var student in SeedStorage.Students)
            {
                dbContext.Students.Add(student);
            }


            //init school
            foreach (var school in SeedStorage.Schools)
            {
                dbContext.Schools.Add(school);
            }

            //init teacher
            foreach (var teacher in SeedStorage.Teachers)
            {
                dbContext.Teachers.Add(teacher);
            }

            dbContext.SaveChanges();
        }
    }
}
