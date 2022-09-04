using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Storage.Constant
{

    public class ExceptionKeys
    {
        public const string ErrorKey = "Error";
        public const string SuccessKey = "Success";
    }

    public class ExceptionConstant
    {


        #region system
        public const int AProblemOccureAtSystem = 0;
        #endregion

        #region Entity exception
        public const int EntityException = 1;
        public const int CanNotFoundEntityException = 2;
        public const int ThereIsNoEntityWithCurrentPredicate = 3;
        public const int UserNotFoundException = 4;
        public const int PasswordIsWorngException = 5;
        #endregion

        #region Identity exception
        public const int IdentityException = 6661;
        public const int DefaultError = 6662;
        public const int ConcurrencyFailure = 6663;
        public const int PasswordMismatch = 6664;
        public const int InvalidToken = 6665;
        public const int LoginAlreadyAssociated = 6666;
        public const int InvalidUserName = 6667;
        public const int InvalidEmail = 6668;
        public const int DuplicateUserName = 6669;
        public const int DuplicateEmail = 6670;
        public const int InvalidRoleName = 6671;
        public const int DuplicateRoleName = 6672;
        public const int UserAlreadyHasPassword = 6673;
        public const int UserLockoutNotEnabled = 6674;
        public const int UserAlreadyInRole = 6675;
        public const int UserNotInRole = 6676;
        public const int PasswordTooShort = 6677;
        public const int PasswordRequiresNonAlphanumeric = 6678;
        public const int PasswordRequiresDigit = 6679;
        public const int PasswordRequiresLower = 6680;
        public const int PasswordRequiresUpper = 6681;
        public const int CanNotFoundAnyUserWithThisUsernameAndPassword = 6682;
        #endregion


        #region phone
        public const int PhoneNumberAlreadyExist = 52;
        public const int PhoneNumberNotFound = 53;
        public const int SMSVerificationCodeIsWrong = 51;
        #endregion


        #region ReflectionException
        public const int ReflectionException = 301;
        public const int CanNotFoundPropertyWithCurrentNameInCurrentType = 302;
        #endregion


        #region license exception
        public const int CurrentEntityAlreadyExist = 10;
        public const int CurrentUsernameAndPasswordAlreadyExist = 11;
        public const int USpendTooMuchTimeOnGetWayURLicenseAlreadySold = 14;
        public const int LicenseAlreadySold = 141;
        public const int LicenseAlreadyHaveInvoicePleaseRemoveThatFirst = 141;
        #endregion

        #region product
        public const int ProductAlreadySoldCanNotDeleteProduct = 12;
        public const int ProductAlreadyHaveUnSuccessFullInvoicePlreaseRemoveThem = 121;
        public const int ProductAlreadyHaveLicensePleaseRemoveThemFirst = 122;
        #endregion


        #region security
        public const int TooManyRequestException = 13;
        #endregion



        #region invoice
        public const int CurrentInvoiceAlreadyHavePaymentResultException = 161;
        #endregion


        #region global
        public const string ErrorDescription = "مشکلی پش امده لطفا یا از منوی بالا خانه را انتخاب کنید و یا بعدا دوباره سعی کنید";
        public const string NotFoundDescription = "متاسفانه صفحه ای که به دنبال ان هستید پیدا نشد برای بازگشت به صفحه اصلی بر روی خانه در منو بالا کلیک کنید";
        public const string NotFoundTitle = "صفحه مورد نظر شما پیدا نشد";
        #endregion


        #region Max sms 
        public const int ExceptionAtSMSProvider = 14;
        #endregion


        #region license template
        public const int PleaseRemoveAllLicensesWithSpecificLicenseTemplate = 201;
        #endregion


        #region features need message template
        public const int CurrentMessageTemplateIsAlreadyUseByAnotherTemplate = 202;
        #endregion

    }


}
