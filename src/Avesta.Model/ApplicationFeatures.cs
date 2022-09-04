using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model
{

    //from 200 to 1000
    public enum FeaturesNeedAuthorizedAccess
    {

        SeeUsers = 200,
        EditUser = 202,
        DeleteUser = 203,
        DisableUser = 204,
        ChangePassword = 205,
        ChangeEmail = 206,
        RegisterAdmin = 604,


        SeeAuthorizeGroupUsers = 307,
        SeeAuthorizeGroup = 308,
        EditAuthorizeGroupUsers = 309,
        EditAuthorizeGroup = 310,
        DeleteUserFromAuthorizeGroup = 311,
        DeleteAuthorizeGroup = 312,
        AddNewUserToAuthorizaGroup = 313,
        CreateAuthorizeGroup = 314,

        BlogPost = 401,
        SeeAllPosts = 402,
        CreateNewPost = 410,
        DeletePost = 411,
        EditPost = 412,

        Group = 404,
        SeeGroups = 405,
        CreateNewGroup = 406,
        EditGroup = 407,
        DeleteGroup = 408,

        Tag = 501,
        SeeTags = 502,
        CreateNewTag = 503,
        EditTag = 504,
        DeleteTag = 505,


        SeeActivityLog = 601,
        DeleteActivityLog = 602,


        ProfileSetting = 603,
        SystemSetting = 701,


        ContactUsForm = 814,


        Clinic = 900,
        SeeAllClinic = 901,
        EditClinic = 902,
        CreateClinic = 903,
        DeleteClinic = 904,


        MessageTemplate = 905,
        EditMessageTemplate = 906,
        AddMessageTemplate = 907,
        TemplateSetting = 910,
        GetAllMessageTemplate = 908,
        DeleteMessageTemplate = 909,





        Paitent = 954,
        SeePaitent = 955,
        CreatePaitent = 956,
        EditPaitent = 957,



        MedicalService = 911,
        EditMedicalService = 912,
        CreateMedicalService = 913,
        DeleteMedicalService = 914,
        SeeAllMedicalService = 915,

        Appointment = 916,
        EditAppointment = 917,
        CreateAppointment = 918,
        DeleteAppointment = 919,
        SeeAllAppointment = 920,


        Advertise = 929,
        EditAdvertise = 930,
        CreateAdvertise = 931,
        DeleteAdvertise = 932,
        SeeAllAdvertise = 933,


        HtmlPage = 934,
        EditHtmlPage = 935,
        DeleteHtmlPage = 936,
        CreateHtmlPage = 937,
        SeeAllHtmlPage = 938,




        AppointmentNotification = 939,
        EditAppointmentNotification = 940,
        CreateAppointmentNotification = 941,
        DeleteAppointmentNotification = 942,
        SeeAllAppointmentNotification = 943,


        Invoice = 944,
        SeeAllInvoice = 945,
        InvoiceDetail = 946,

        QAndA = 947,
        SeeAllQAndA = 948,
        EditQAndA = 949,
        CreateQAndA = 950,



        UserFile = 951,
        SeeAllUserFile = 952,
        EditUserFile = 953,
        CreateUserFile = 954,

    }

}
