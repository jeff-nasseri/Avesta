using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Avesta.Repository.Identity.Model;
using Avesta.Exceptions.Entity;
using Avesta.Exceptions.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Avesta.Model.Identity;
using Avesta.Data.Model;

namespace Avesta.Repository.Identity
{

    public class IdentityRepository<TUser, TRole> : IIdentityRepository<TUser, TRole>
        where TUser : AvestaUser
        where TRole : IdentityRole
    {
        readonly UserManager<TUser> _userManager;
        readonly SignInManager<TUser> _signInManager;
        readonly IMapper _mapper;
        readonly RoleManager<TRole> _roleManager;
        public IdentityRepository(UserManager<TUser> userManager
            , SignInManager<TUser> signInManager, IMapper mapper
            , RoleManager<TRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        

      

        #region role
        public async Task<IdentityResult> CreateNewRole(TRole role) => await _roleManager.CreateAsync(role);
        public async Task<IdentityResult> AddUserToRole(TUser user, string roleName)
            => !string.IsNullOrEmpty(roleName) ? await _userManager.AddToRoleAsync(user, roleName) : null;
        #endregion


        #region register and sign_in_out
        public async Task<IdentityRegisterUserReturn> RegisterUser<TModel>(TModel model, string role = null) where TModel : RegisterModelBase
        {
            var user = _mapper.Map<TUser>(model);
            user.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, model.Password);

            return new IdentityRegisterUserReturn
            {
                Succeed = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToArray(),
                AddToRoleResult = await AddUserToRole(user, role)
            };
        }
        public async Task<IdentityRepositoryReturn> SignIn<TModel>(TModel model, bool isPersistent = true) where TModel : LoginModelBase
        {
            var user = await _userManager.FindByIdAsync(model.ID);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: isPersistent, lockoutOnFailure: true);
            return new IdentityRepositoryReturn
            {
                Succeed = result.Succeeded,
            };
        }

        public async Task Signin(TUser user, bool isPersistent)
        {
            await _signInManager.SignInAsync(user, isPersistent: isPersistent);
        }


        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion

        #region security
        public async Task<string> GenerateResetPasswordToken(TUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }
        public async Task<IdentityResult> ResetUserPassword(TUser user, string resetPasswordToken, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordToken, newPassword);
            return result;
        }
        #endregion




        #region get user info
        public async Task<string> GetUserIDByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException(email);
            return user.Id;
        }
        public async Task<TUser> GetUser(ClaimsPrincipal claimsPrincipal)
        {
            return await _userManager.GetUserAsync(claimsPrincipal);
        }
        public async Task<IEnumerable<TUser>> GetUsers()
        {
            await Task.CompletedTask;
            return _userManager.Users;
        }
        public async Task<TUser> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public async Task<TUser> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }
        public async Task<TUser> GetUserByInclude(string navigationProperties, Expression<Func<TUser, bool>> exp, bool exceptionIfNotExist = false)
        {
            var result = await _userManager.Users.Include(navigationProperties).SingleOrDefaultAsync(exp);
            if (result == null && exceptionIfNotExist)
            {
                throw new CanNotFoundEntityException(exp.ToString());
            }
            return result;
        }
        public async Task<TUser> GetUser(Expression<Func<TUser, bool>> exp, bool exceptionIfNotExist = false)
        {
            var result = await _userManager.Users.SingleOrDefaultAsync(exp);
            if (result == null && exceptionIfNotExist)
            {
                throw new UserNotFoundException(exp.ToString());
            }
            return result;
        }
        #endregion

        #region account lock 
        public async Task<DateTimeOffset?> GetEndLockOutDate(string id)
        {
            var user = await GetUser(id);
            var end = await _userManager.GetLockoutEndDateAsync(user);
            return end;
        }
        public async Task<IdentityResult> SetLockOutEndForUserByEmail(string email, DateTime end)
        {
            var user = await GetUserByEmail(email);
            var result = await _userManager.SetLockoutEndDateAsync(user, end);
            return result;
        }
        public async Task<IdentityResult> ChangeUserEmail(string newEmail, string password, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            _ = user ?? throw new UserNotFoundException(id);
            if (!await _userManager.CheckPasswordAsync(user, password))
                throw new PasswordIsWorngException(id);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            return result;
        }
        public async Task<IdentityResult> ChangeUserPassword(string oldPassword, string newPassword, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result;
        }
        public async Task<IdentityResult> ChangeUserEmail(string newEmail, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            _ = user ?? throw new UserNotFoundException(id);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            return result;
        }
        #endregion



        #region delete
        public async Task<IdentityResult> DeleteUser(TUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }
        public async Task DeleteUserByInclude(string navigationProperties, Expression<Func<TUser, bool>> exp)
        {
            var user = await _userManager.Users.Include(navigationProperties).SingleOrDefaultAsync(exp);
            if (user == null)
                throw new CanNotFoundEntityException(exp.ToString());
            await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> DeleteUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            return result;
        }
        #endregion


        #region edit
        public async Task<IdentityResult> SetAccountLockOutById(string id, bool enabled)
        {
            var user = await GetUser(id);
            var result = await _userManager.SetLockoutEnabledAsync(user, enabled);
            return result;
        }
        public async Task<IdentityResult> SetLockOutEndForUser(string id, DateTime end)
        {
            var user = await GetUser(id);
            var result = await _userManager.SetLockoutEndDateAsync(user, end);
            return result;
        }
        public async Task<bool> IsAccountLocked(string id)
        {
            var user = await GetUser(id);
            var end = await _userManager.GetLockoutEndDateAsync(user);
            return end > DateTime.Now;
        }

        public async Task<IdentityResult> RegisterNewUser(TUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task UpdateUser(TUser user)
        {
            await _userManager.UpdateAsync(user);
        }

  
        #endregion

    }


}

