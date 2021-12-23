using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyIslamWebApp.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.DataTransferObjects.Users;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Models;

namespace MyIslamWebApp
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;
        private UserManager<ApplicationUser> _userManager;        


        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserRegistrationDTO userModel, string encryptedPassword)
        {

            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,               
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Password = encryptedPassword,
               
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            if(result.Succeeded)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AuthContext()));
                var _user = _userManager.FindByName(user.UserName);
                _userManager.AddToRole(_user.Id, "User");
            }

            return result; 
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            
            ApplicationUser user = await _userManager.FindAsync(userName, password);            
            return user;
        }

        public string GetRole(string userId)
        {
            var userRole= _userManager.GetRoles(userId).FirstOrDefault();
            return userRole;
        }

        public bool GetSubscriptionEndDate(string userId)
        {             
            var result = _ctx.Subscriptions.Where(x =>x.CreatedBy == userId).FirstOrDefault();
           
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
        public bool IsSubscriptionComplete(string userId)
        {
            var result = _ctx.Users.Where(x => x.SubscriptionComplete == true).FirstOrDefault();

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ApplicationUser> FindUserByUserId(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);
            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).FirstOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }

        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }
    }
}