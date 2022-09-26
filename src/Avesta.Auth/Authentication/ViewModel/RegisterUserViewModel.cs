using Avesta.Model;
using Avesta.Model.Enum;
using Avesta.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Authentication.ViewModel
{
    public class RegisterUserViewModel : RegisterModelBase
    {
        public string? SchoolCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public Sex Sex { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string NationalCode { get; set; }
    }
}
