using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Avesta.Storage.Constant
{

 


    //from 1056 to 1076
    public class UserFileOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Representative = 1056;
                public const int GeneralCondition = 1058;
                public const int TheProceedings = 1060;
                public const int FatherName = 1062;
            }
        }

        public class Error
        {
            public class Required
            {
                public const int Representative = 1057;
                public const int GeneralCondition = 1059;
                public const int TheProceedings = 1061;
                public const int FatherName = 1063;
            }
        }
    }



    //from 1045 to 1055
    public class AppointmentNotificationOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Title = 1045;
                public const int NotificationDate = 1046;
                public const int Description = 1047;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int Title = 1048;
                public const int NotificationDate = 1049;
                public const int Description = 1050;
            }
        }
    }



    //from 1033 to 1044
    public class HtmlPageOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Link = 1033;
                public const int Html = 1034;
                public const int NameOfHtml = 1035;
            }
        }

        public class Error
        {
            public class Required
            {
                public const int Link = 1036;
                public const int Html = 1037;
                public const int NameOfHtml = 1038;
            }
        }
    }



    //from 1022 to 1032
    public class AdvertiseOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Title = 1022;
                public const int Link = 1023;
                public const int FilePath = 1024;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int Title = 1025;
                public const int Link = 1026;
                public const int FilePath = 1027;
            }
        }
    }




    //from 1001 to 1021
    public class AppointmentOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int DoctorId = 1001;
                public const int ClinicId = 1002;
                public const int MedicalServiceId = 1003;
                public const int StartDate = 1004;
                public const int Price = 1005;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int DoctorId = 1006;
                public const int ClinicId = 1007;
                public const int MedicalServiceId = 1008;
                public const int StartDate = 1009;
                public const int Price = 1010;
            }
        }
    }


    //from 971 to 1000
    public class MedicalServiceOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int ServiceName = 971;
                public const int Description = 972;
                public const int Price = 975;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int ServiceName = 973;
                public const int Description = 974;
                public const int Price = 976;
            }
        }
    }





    //from 951 to 970
    public class TemplateOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int TemplateName = 932;
                public const int TemplateTitle = 933;
                public const int TemplateContent = 934;
                public const int TemplateFeature = 938;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int TemplateName = 935;
                public const int TemplateTitle = 936;
                public const int TemplateContent = 937;
                public const int TemplateFeature = 939;
            }
        }
    }



    //from 931 to 950
    public class SMSOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Code = 931;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int Code = 932;
            }
        }
    }



    //from 901 to 930
    public class SMTPOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Email = 901;
                public const int Password = 902;
                public const int SMTPHost = 903;
                public const int DisplayName = 904;
                public const int Port = 905;
                public const int SMTPUsername = 906;
                public const int SSL = 907;
                public const int TLS = 908;
            }
        }

        public class Error
        {
            public class Required
            {
                public const int Email = 908;
                public const int Password = 909;
                public const int SMTPHost = 910;
                public const int DisplayName = 911;
                public const int Port = 912;
                public const int SMTPUsername = 913;
            }
        }
    }


    //from 801 to 900
    public class ClinicOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int NameOfClinic = 801;
                public const int Description = 802;
                public const int Provicense = 803;
                public const int City = 804;
                public const int Address = 805;
                public const int InstagramLink = 811;
                public const int Phonenumber = 812;

            }
        }

        public class Error
        {
            public class Required
            {
                public const int NameOfClinic = 806;
                public const int Description = 807;
                public const int Provicense = 808;
                public const int City = 809;
                public const int Address = 810;
                public const int InstagramLink = 813;
                public const int Phonenumber = 814;
            }
        }
    }



    //from 701 to 800
    public class PaitentOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int DateOfHospitalization = 701;
                public const int FileNumber = 702;
                public const int NationalCode = 703;
                public const int FirstName = 704;
                public const int LastName = 705;
                public const int DateOfBirth = 706;
                public const int Sex = 707;
                public const int Job = 708;
                public const int EducationRate = 709;
                public const int Address = 710;
                public const int Provinces = 740;
                public const int City = 741;
                public const int CauseOfHospitalization = 711;
                public const int PreviousHospitalizationHistory = 712;
                public const int TheInitialPartOfPatientAdmission = 713;
                public const int InpatientPhysician = 714;
                public const int Doctor = 715;
                public const int InpatientDepartment = 716;
                public const int LevelOfConsciousness = 717;
                public const int Age = 718;


                public const int BeingEmergencyOrElective = 719;
                public const int ConsultationsPerformed = 720;
                public const int ConsultationRequestHours = 721;
                public const int HoursOfConsultation = 722;
                public const int ConsultingRequestService = 723;
                public const int ConsultingService = 724;
                public const int TypeOfAdvice = 725;
                public const int DateOfRequestForConsultation = 726;
                public const int DateOfConsultation = 727;
                public const int InpatientReferralInIQ = 728;
                public const int Consultant = 742;



                public const int SurgeryPerformed = 729;
                public const int DateOfSurgery = 730;
                public const int HistoryOfAnesthesia = 731;
                public const int TypeOfAnesthesia = 732;
                public const int AnesthesiaTime = 733;
                public const int NumberOfDaysInTheEmergencyRoom = 734;
                public const int NumberOfDaysOfHospitalizationInTheGeneralWard = 735;
                public const int NumberOfDaysHospitalizedInIntensiveCare = 736;
                public const int CostOfEmergencyDayBed = 737;
                public const int TheCostOfADayBedInTheGeneralWard = 738;
                public const int TheCostOfADayCareUnitBed = 739;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int DateOfHospitalization = 751;
                public const int FileNumber = 752;
                public const int NationalCode = 753;
                public const int FirstName = 754;
                public const int LastName = 755;
                public const int DateOfBirth = 756;
                public const int Sex = 757;
                public const int Job = 758;
                public const int EducationRate = 759;
                public const int Address = 760;
                public const int CauseOfHospitalization = 761;
                public const int PreviousHospitalizationHistory = 762;
                public const int TheInitialPartOfPatientAdmission = 763;
                public const int InpatientPhysician = 764;
                public const int Doctor = 765;
                public const int InpatientDepartment = 766;
                public const int LevelOfConsciousness = 767;
                public const int Provinces = 768;
                public const int City = 769;
            }
        }
    }



    //from 601 to 700
    public class ContactUsFormOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Title = 601;
                public const int Fullname = 602;
                public const int Email = 603;
                public const int ContactNumber = 604;
                public const int Description = 605;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int Title = 606;
                public const int Fullname = 607;
                public const int Email = 608;
                public const int ContactNumber = 609;
                public const int Description = 610;
            }
        }
    }


    //from 501 to 600
    public class SystemSettingOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int DynamicContent = 501;
                public const int ContentValue = 502;
                public const int IsSystemActive = 532;
                public const int AccessToOtherCountryIP = 533;
                public const int SupportNumber = 534;
                public const int SupportWorkingHours = 535;
                public const int RequestLinkIFrame = 536;
                public const int AboutUsTXT = 537;
                public const int ContactUsTXT = 538;
                public const int TelegramLink = 539;
                public const int InstagramLink = 540;
                public const int WhatsAppLink = 541;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int DynamicContent = 503;
                public const int ContentValue = 504;
            }
        }
    }


    //from 401 to 500
    public class SeoOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public class Schema
                {
                    public class QAP
                    {
                        public const int Name = 401;
                        public const int Text = 402;
                        public const int Answer = 403;
                    }
                }

            }
        }
        public class Error
        {
            public class Required
            {
                public class Schema
                {
                    public class QAP
                    {
                        public const int Name = 404;
                        public const int Text = 405;
                        public const int Answer = 406;
                    }
                }
            }
        }

    }


    //from 301 to 400
    public class PostOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Link = 301;
                public const int Title = 302;
                public const int Body = 303;
                public const int SeoTagTitle = 304;
                public const int SeoTagDescription = 305;
                public const int Group = 306;
                public const int IndexPhoto = 307;
                public const int Show = 315;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int Link = 308;
                public const int Title = 309;
                public const int Body = 310;
                public const int SeoTagTitle = 311;
                public const int SeoTagDescription = 312;
                public const int Group = 313;
                public const int IndexPhoto = 314;
                public const int Show = 316;
            }
        }
    }


    //from 201 to 300
    public class TagOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Title = 201;
                public const int Description = 202;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int Title = 203;
                public const int Description = 204;
            }
        }
    }



    //from 101 to 200
    public class GroupOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int GroupName = 101;
                public const int ChooseParent = 102;
                public const int Index = 105;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int GroupName = 103;
                public const int ChooseParent = 104;
                public const int Index = 106;
            }
        }
    }


    //from 1 to 100
    public class AccountOperationMessages
    {
        public class Field
        {
            public class Name
            {
                public const int Username = 1;
                public const int Email = 2;
                public const int Password = 3;
                public const int RepeatPassword = 4;
                public const int FullName = 5;
                public const int NationalCode = 56;
                public const int FirstName = 100;
                public const int LastName = 99;
                public const int PhoneNumber = 6;
                public const int NewPassword = 7;
                public const int AuthorizeGroupName = 9;
            }
        }
        public class Error
        {
            public class Required
            {
                public const int Username = 11;
                public const int Email = 21;
                public const int Password = 31;
                public const int RepeatPassword = 41;
                public const int FullName = 51;
                public const int PhoneNumber = 61;
                public const int NewPassword = 71;
                public const int AuthorizeGroupName = 91;
                public const int FirstName = 98;
                public const int LastName = 97;
                public const int NationalCode = 35;

            }
            public class DataType
            {
                public const int Email = 22;
            }
            public class Compair
            {
                public const int PasswordAndNewPassword = 81;
            }
        }

    }
}
