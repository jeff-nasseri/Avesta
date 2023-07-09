using Microsoft.AspNetCore.Identity;
using Avesta.Repository.IdentityCore.Model;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Avesta.Share.Model.Identity;

namespace Avesta.Repository.IdentityCore
{
    public interface IIdentityRepository<TUser, TRole>
    {

        #region role
        Task<IdentityResult> CreateNewRole(TRole role);
        Task<IdentityResult> AddUserToRole(TUser user, string roleName);
        #endregion


        #region register and sign_in_out
        Task<IdentityResult> RegisterNewUser(TUser user, string password);
        Task<IdentityRegisterUserReturn> RegisterUser<TModel>(TModel model, string role = null) where TModel : RegisterModelBase;
        Task<IdentityRepositoryReturn> SignIn<TModel>(TModel model, bool isPersistent = true) where TModel : LoginModelBase;
        Task Signin(TUser user, bool isPersistent);
        Task SignOut();
        #endregion



        #region get user info
        Task<string> GetUserIDByEmail(string email);
        Task<TUser> GetUser(ClaimsPrincipal claimsPrincipal);
        Task<IEnumerable<TUser>> GetUsers();
        Task<TUser> GetUser(string id);
        Task<TUser> GetUserByEmail(string email);
        Task<TUser> GetUserByEmail(string email, string navigationPropertyPath);
        Task<TUser> GetUserByInclude(string navigationProperties, Expression<Func<TUser, bool>> exp, bool exceptionRaiseIfNotExist = false);
        Task<TUser> GetUser(Expression<Func<TUser, bool>> exp, bool exceptionRaiseIfNotExist = false);

        #endregion




        #region account lock 
        Task<IdentityResult> SetAccountLockOutById(string id, bool enabled);
        Task<IdentityResult> SetLockOutEndForUser(string id, DateTime end);
        Task<bool> IsAccountLocked(string id);
        Task<DateTimeOffset?> GetEndLockOutDate(string id);
        Task<IdentityResult> SetLockOutEndForUserByEmail(string email, DateTime end);

        #endregion

        #region security
        Task<string> GenerateResetPasswordToken(TUser user);
        Task<IdentityResult> ResetUserPassword(TUser user, string resetPasswordToken, string newPassword);
        #endregion



        #region delete
        Task<IdentityResult> DeleteUserById(string id);
        Task DeleteUserByInclude(string navigationProperties, Expression<Func<TUser, bool>> exp);
        Task<IdentityResult> DeleteUser(TUser user);
        #endregion


        #region edit 
        Task<IdentityResult> ChangeUserEmail(string newEmail, string password, string id);
        Task<IdentityResult> ChangeUserEmail(string newEmail, string id);
        Task<IdentityResult> ChangeUserPassword(string oldPassword, string newPassword, string id);
        Task UpdateUser(TUser user);
        #endregion
    }
}
