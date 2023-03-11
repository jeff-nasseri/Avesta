using Avesta.Data.Model;
using Avesta.Repository.Test.Src.Data.Model;
using Avesta.Repository.Test.Src.Share.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.Test.Src.Storage
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
                case "Student": return "[{\"Fullname\":\"test\",\"Gender\":0,\"Age\":0,\"AnyExteraNote\":null,\"SchoolId\":null,\"School\":null,\"Counter\":0,\"ID\":null,\"IsLock\":false,\"ModifiedDate\":null,\"CreatedDate\":\"2023-03-06T16:39:42.5010541Z\",\"DeletedDate\":null}]";
                case "Teacher": return "[{\"Fullname\":\"Ahmad Ahmadi\",\"Gender\":0,\"Age\":67,\"Teacher_Schools\":null,\"Counter\":0,\"ID\":\"73794880-d275-4703-a74d-fd32a338b369\",\"IsLock\":false,\"ModifiedDate\":null,\"CreatedDate\":\"2023-03-06T16:42:01.6871323Z\",\"DeletedDate\":null},{\"Fullname\":\"Arezo Ahmadi\",\"Gender\":1,\"Age\":40,\"Teacher_Schools\":null,\"Counter\":0,\"ID\":\"73794880-d275-4703-a74d-fd32a338b368\",\"IsLock\":false,\"ModifiedDate\":null,\"CreatedDate\":\"2023-03-06T16:42:01.6871771Z\",\"DeletedDate\":null},{\"Fullname\":\"Jef Varkat\",\"Gender\":0,\"Age\":56,\"Teacher_Schools\":null,\"Counter\":0,\"ID\":\"73794880-d275-4703-a74d-fd32a338b367\",\"IsLock\":false,\"ModifiedDate\":null,\"CreatedDate\":\"2023-03-06T16:42:01.6871772Z\",\"DeletedDate\":null}]";
                case "School": return "[{\"Name\":\"School A\",\"Students\":null,\"Teacher_Schools\":null,\"Counter\":0,\"ID\":\"73794880-d275-4703-a74d-fd32a338b373\",\"IsLock\":false,\"ModifiedDate\":null,\"CreatedDate\":\"2023-03-06T16:42:01.6870896Z\",\"DeletedDate\":null},{\"Name\":\"School B\",\"Students\":null,\"Teacher_Schools\":null,\"Counter\":0,\"ID\":\"73794880-d275-4703-a74d-fd32a338b374\",\"IsLock\":false,\"ModifiedDate\":null,\"CreatedDate\":\"2023-03-06T16:42:01.6871091Z\",\"DeletedDate\":null}]";
                case "Teacher_School": return "[{\"TeacherId\":\"73794880-d275-4703-a74d-fd32a338b367\",\"Teacher\":null,\"SchoolId\":\"73794880-d275-4703-a74d-fd32a338b373\",\"School\":null,\"Counter\":0,\"ID\":\"13794880-d275-4703-a74d-fd32a338b373\",\"IsLock\":false,\"ModifiedDate\":null,\"CreatedDate\":\"2023-03-06T16:42:01.6871986Z\",\"DeletedDate\":null},{\"TeacherId\":\"73794880-d275-4703-a74d-fd32a338b368\",\"Teacher\":null,\"SchoolId\":\"73794880-d275-4703-a74d-fd32a338b374\",\"School\":null,\"Counter\":0,\"ID\":\"23794880-d275-4703-a74d-fd32a338b373\",\"IsLock\":false,\"ModifiedDate\":null,\"CreatedDate\":\"2023-03-06T16:42:01.6872276Z\",\"DeletedDate\":null},{\"TeacherId\":\"73794880-d275-4703-a74d-fd32a338b369\",\"Teacher\":null,\"SchoolId\":\"73794880-d275-4703-a74d-fd32a338b373\",\"School\":null,\"Counter\":0,\"ID\":\"33794880-d275-4703-a74d-fd32a338b373\",\"IsLock\":false,\"ModifiedDate\":null,\"CreatedDate\":\"2023-03-06T16:42:01.6872277Z\",\"DeletedDate\":null}]";
                default: throw new Exception("type not found !");
            }

        }
        #endregion










        public static IEnumerable<Student> Students = new List<Student>
        {
            new Student
            {
                Fullname = "Alireza Nasseri",
                Age = 24,
                AnyExteraNote = "My name is Alireza, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b375",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374"
            },
            new Student
            {
                Fullname = "Hamed Hamidi",
                Age = 44,
                AnyExteraNote = "My name is Hamed, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b376",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374"


            },
            new Student
            {
                Fullname = "Sahar Ziba",
                Age = 25,
                AnyExteraNote = "My name is Sahar, I'm very excited to be here",
                Gender = Gender.Woman,
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b377",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374"


            },
            new Student
            {
                Fullname = "Sara Boiler",
                Age = 20,
                AnyExteraNote = "My name is Sara, I'm very excited to be here",
                Gender = Gender.Woman,
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b378",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374"


            },
            new Student
            {
                Fullname = "Jack Varkat",
                Age = 20,
                AnyExteraNote = "My name is Jack, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b379",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374"


            },
            new Student
            {
                Fullname = "Biden Boiler",
                Age = 22,
                AnyExteraNote = "My name is Biden, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b371",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b373"

            },
            new Student
            {
                Fullname = "Reza Asqari",
                Age = 23,
                AnyExteraNote = "My name is Reza, I'm very excited to be here",
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b372",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b373"


            }
        };


        public static IEnumerable<School> Schools = new List<School>
        {
            new School
            {
                Name = "School A",
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b373"

            },
            new School
            {
                Name = "School B",
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b374"

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
                ID = "73794880-d275-4703-a74d-fd32a338b369"

            },
            new Teacher
            {
                Fullname = "Arezo Ahmadi",
                Age = 40,
                Gender = Gender.Woman,
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b368"

            },
            new Teacher
            {
                Fullname = "Jef Varkat",
                Age = 56,
                Gender = Gender.Man,
                //ID = Guid.NewGuid().ToString()
                ID = "73794880-d275-4703-a74d-fd32a338b367"

            }
        };


        public static IEnumerable<Teacher_School> Teacher_Schools = new List<Teacher_School>
        {
            new Teacher_School
            {
                TeacherId = "73794880-d275-4703-a74d-fd32a338b367",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b373",
                ID = "13794880-d275-4703-a74d-fd32a338b373"
            },
            new Teacher_School
            {
                TeacherId = "73794880-d275-4703-a74d-fd32a338b368",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b374",
                ID = "23794880-d275-4703-a74d-fd32a338b373"
            },
            new Teacher_School
            {
                TeacherId = "73794880-d275-4703-a74d-fd32a338b369",
                SchoolId = "73794880-d275-4703-a74d-fd32a338b373",
                ID = "33794880-d275-4703-a74d-fd32a338b373"
            }
        };


    }
}
