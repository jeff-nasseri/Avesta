using Avesta.Data.Model;
using Avesta.Repository.Test.Src.Data.Model;
using Avesta.Repository.Test.Src.Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.Test.Src.Storage
{
    public class SeedStorage
    {

        public static BaseEntity First(Type type) 
        {
            switch (type.Name)
            {
                case "Student": return Students.First();
                case "Teacher": return Teachers.First();
                case "School": return Schools.First();
                default:throw new Exception("type not found !");
            }
        }
        public static BaseEntity Last(Type type)
        {
            switch (type.Name)
            {
                case "Student": return Students.Last();
                case "Teacher": return Teachers.Last();
                case "School": return Schools.Last();
                default: throw new Exception("type not found !");
            }
        }


        public static IEnumerable<Student> Students = new List<Student>
        {
            new Student
            {
                Fullname = "Alireza Nasseri",
                Age = 24,
                AnyExteraNote = "My name is Alireza, I'm very excited to be here",
                Gender = Gender.Man,
                ID = Guid.NewGuid().ToString()
            },
            new Student
            {
                Fullname = "Hamed Hamidi",
                Age = 44,
                AnyExteraNote = "My name is Hamed, I'm very excited to be here",
                Gender = Gender.Man,
                ID = Guid.NewGuid().ToString()
            },
            new Student
            {
                Fullname = "Sahar Ziba",
                Age = 25,
                AnyExteraNote = "My name is Sahar, I'm very excited to be here",
                Gender = Gender.Woman,
                ID = Guid.NewGuid().ToString()
            },
            new Student
            {
                Fullname = "Sara Boiler",
                Age = 20,
                AnyExteraNote = "My name is Sara, I'm very excited to be here",
                Gender = Gender.Woman,
                ID = Guid.NewGuid().ToString()
            },
            new Student
            {
                Fullname = "Jack Varkat",
                Age = 20,
                AnyExteraNote = "My name is Jack, I'm very excited to be here",
                Gender = Gender.Man,
                ID = Guid.NewGuid().ToString()
            },
            new Student
            {
                Fullname = "Biden Boiler",
                Age = 22,
                AnyExteraNote = "My name is Biden, I'm very excited to be here",
                Gender = Gender.Man,
                ID = Guid.NewGuid().ToString()
            },
            new Student
            {
                Fullname = "Reza Asqari",
                Age = 23,
                AnyExteraNote = "My name is Reza, I'm very excited to be here",
                Gender = Gender.Man,
                ID = Guid.NewGuid().ToString()
            }
        };


        public static IEnumerable<School> Schools = new List<School>
        {
            new School
            {
                Name = "School A",
                ID = Guid.NewGuid().ToString()
            },
            new School
            {
                Name = "School B",
                ID = Guid.NewGuid().ToString()
            }
        };


        public static IEnumerable<Teacher> Teachers = new List<Teacher>
        {
            new Teacher
            {
                Fullname = "Ahmad Ahmadi",
                Age = 67,
                Gender = Gender.Man,
                ID = Guid.NewGuid().ToString()
            },
            new Teacher
            {
                Fullname = "Arezo Ahmadi",
                Age = 40,
                Gender = Gender.Woman,
                ID = Guid.NewGuid().ToString()
            },
            new Teacher
            {
                Fullname = "Jef Varkat",
                Age = 56,
                Gender = Gender.Man,
                ID = Guid.NewGuid().ToString()
            }
        };


    }
}
