using Avesta.Data.Entity.Model;
using Avesta.Graph.Test.Src.Data.Model;
using Avesta.Graph.Test.Src.Share.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Graph.Test.Src.Storage
{
    public class SeedStorage
    {

        #region [- API -]
        public static string FirstId(Type type)
        {
            switch (type.Name)
            {
                case "Student": return "73794880-d275-4703-a74d-fd32a338b375";
                case "Teacher": return "73794880-d275-4703-a74d-fd32a338b369";
                case "School": return "73794880-d275-4703-a74d-fd32a338b373";
                case "Teacher_School": return "13794880-d275-4703-a74d-fd32a338b373";
                default: throw new Exception("type not found !");
            }
        }
        public static string LastId(Type type)
        {
            switch (type.Name)
            {
                case "Student": return "73794880-d275-4703-a74d-fd32a338b379";
                case "Teacher": return "73794880-d275-4703-a74d-fd32a338b367";
                case "School": return "73794880-d275-4703-a74d-fd32a338b374";
                case "Teacher_School": return "33794880-d275-4703-a74d-fd32a338b373";
                default: throw new Exception("type not found !");
            }
        }

        public static string GetPath(Type type)
        {
            switch (type.Name)
            {
                case "Student": return nameof(Student.School);
                case "Teacher": return nameof(Teacher.Teacher_Schools);
                case "School": return nameof(Teacher.Teacher_Schools);
                case "Teacher_School": return $"{nameof(Teacher_School.Teacher)};{nameof(Teacher_School.School)}";
                default: throw new Exception("type not found !");
            }
        }

        public static string GetJsonOfDataInPage(int page, int perPage, Type type)
        {
            var take = perPage;
            var skip = (page - 1) * perPage;

            switch (type.Name)
            {
                case "Student": return JsonConvert.SerializeObject(Students.OrderBy(e => e.CreatedDate).Skip(skip).Take(take).ToList());
                case "Teacher": return JsonConvert.SerializeObject(Teachers.OrderBy(e => e.CreatedDate).Skip(skip).Take(take).ToList());
                case "School": return JsonConvert.SerializeObject(Schools.OrderBy(e => e.CreatedDate).Skip(skip).Take(take).ToList());
                case "Teacher_School": return JsonConvert.SerializeObject(Teacher_Schools.OrderBy(e => e.CreatedDate).Skip(skip).Take(take).ToList());
                default: throw new Exception("type not found !");
            }

        }
        #endregion





        public static IEnumerable<School> Schools = new List<School>
        {
            new School
            {
                Name = "School A",
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b373"

            },
            new School
            {
                Name = "School B",
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b374"

            }
        };





        public static IEnumerable<Student> Students = new List<Student>
        {
            new Student
            {
                Fullname = "Alireza Nasseri",
                Age = 24,
                AnyExteraNote = "My name is Alireza, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b375",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374",
                School = Schools.Single(s=>s.Id == "73794880-d275-4703-a74d-fd32a338b374")
            },
            new Student
            {
                Fullname = "Hamed Hamidi",
                Age = 44,
                AnyExteraNote = "My name is Hamed, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b376",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374",
                School = Schools.Single(s=>s.Id == "73794880-d275-4703-a74d-fd32a338b374")

            },
            new Student
            {
                Fullname = "Sahar Ziba",
                Age = 25,
                AnyExteraNote = "My name is Sahar, I'm very excited to be here",
                Gender = Gender.Woman,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b377",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374",
                School = Schools.Single(s=>s.Id == "73794880-d275-4703-a74d-fd32a338b374")
            },
            new Student
            {
                Fullname = "Sara Boiler",
                Age = 26,
                AnyExteraNote = "My name is Sara, I'm very excited to be here",
                Gender = Gender.Woman,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b378",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374",
                School = Schools.Single(s=>s.Id == "73794880-d275-4703-a74d-fd32a338b374")
            },
            new Student
            {
                Fullname = "Jack Varkat",
                Age = 27,
                AnyExteraNote = "My name is Jack, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b379",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374",
                School = Schools.Single(s=>s.Id == "73794880-d275-4703-a74d-fd32a338b374")

            },
            new Student
            {
                Fullname = "Biden Boiler",
                Age = 28,
                AnyExteraNote = "My name is Biden, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b371",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b373",
                School = Schools.Single(s=>s.Id == "73794880-d275-4703-a74d-fd32a338b373")
            },
            new Student
            {
                Fullname = "Reza Asqari",
                Age = 23,
                AnyExteraNote = "My name is Reza, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b372",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b373",
                School = Schools.Single(s=>s.Id == "73794880-d275-4703-a74d-fd32a338b373")
            }
        };




        public static IEnumerable<Teacher> Teachers = new List<Teacher>
        {
            new Teacher
            {
                Fullname = "Ahmad Ahmadi",
                Age = 67,
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b369"

            },
            new Teacher
            {
                Fullname = "Arezo Ahmadi",
                Age = 40,
                Gender = Gender.Woman,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b368"

            },
            new Teacher
            {
                Fullname = "Jef Varkat",
                Age = 56,
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                Id = "73794880-d275-4703-a74d-fd32a338b367"

            }
        };


        public static IEnumerable<Teacher_School> Teacher_Schools = new List<Teacher_School>
        {
            new Teacher_School
            {
                TeacherId = "73794880-d275-4703-a74d-fd32a338b367",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b373",
                Id = "13794880-d275-4703-a74d-fd32a338b373"
            },
            new Teacher_School
            {
                TeacherId = "73794880-d275-4703-a74d-fd32a338b368",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374",
                Id = "23794880-d275-4703-a74d-fd32a338b373"
            },
            new Teacher_School
            {
                TeacherId = "73794880-d275-4703-a74d-fd32a338b369",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b373",
                Id = "33794880-d275-4703-a74d-fd32a338b373"
            }
        };


    }
}
