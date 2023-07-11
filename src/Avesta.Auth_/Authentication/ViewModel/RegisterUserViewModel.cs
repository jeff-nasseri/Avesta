using Avesta.Share.Enum;
using Avesta.Share.Model.Identity;

namespace Avesta.Auth.Authentication.ViewModel
{
    public class RegisterUserViewModel : RegisterModelBase
    {
        public string? SchoolCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public string? Address { get; set; }
        public Sex Sex { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        public string? NationalCode { get; set; }
    }
}
