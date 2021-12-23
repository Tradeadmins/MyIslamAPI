using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using MyIslamWebApp.DataContext;
using MyIslamWebApp.DataTransferObjects.Users;
using MyIslamWebApp.Entities;
using MyIslamWebApp.Enums;
using MyIslamWebApp.Filter;
using MyIslamWebApp.Models;
using MyIslamWebApp.Providers;
using Newtonsoft.Json.Linq;

namespace MyIslamWebApp.Controllers
{
    /// <summary>
    /// Account Apis List 
    /// </summary>
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        #region Properties
        private AuthRepository _repo = null;
        private AuthContext _ctx;

        private UserManager<ApplicationUser> _userManager;

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor: 
        /// Call Auth Repository
        /// </summary>
        public AccountController()
        {
            _repo = new AuthRepository();
            _ctx = new AuthContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Register new User
        /// </summary>
        /// <remarks>
        /// Add a new User
        /// </remarks>
        /// <param name="userModel">User to add</param>
        /// <returns></returns>
        /// <response code="200">User created</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserRegistrationDTO userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                EncryptDecryptHelper ecryptDecryptHelper = new EncryptDecryptHelper();

                string encryptedPassword = ecryptDecryptHelper.Encrypt(userModel.Password);

                IdentityResult result = await _repo.RegisterUser(userModel, encryptedPassword);

                IHttpActionResult errorResult = GetErrorResult(result);
                if (errorResult != null)
                {
                    return errorResult;
                }

                ApplicationUser user = await _repo.FindUser(userModel.UserName, userModel.Password);
                var userRole = _repo.GetRole(user.Id);

                if (userRole.ToLower() == "user")
                {
                    //generate access token response
                    var _accessTokenResponse = GenerateLocalAccessTokenResponse(user.UserName, user.Id, user.SubscriptionComplete);
                    return Ok(_accessTokenResponse);
                }

                return Ok("User Created Succesfully");                
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Register Social User
        /// </summary>
        /// <remarks>
        /// Add a new User
        /// </remarks>
        /// <param name="model">User to add</param>
        /// <returns>accessTokenResponse</returns>
        /// <response code="200">User created</response>
        [AllowAnonymous]
        [Route("ExternalRegister")]
        public async Task<IHttpActionResult> ExternalRegister(RegisterExternalBindingModel model)
        {
            try
            {
                ApplicationUser user = await _repo.FindAsync(new UserLoginInfo(model.Provider, model.ExternalUserId));

                bool hasRegistered = user != null;

                if (hasRegistered)
                {
                    //generate access token response
                    var _accessTokenResponse = GenerateLocalAccessTokenResponse(model.UserName, user.Id, user.SubscriptionComplete);

                    return Ok(_accessTokenResponse);
                }

                user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    SubscriptionComplete = model.SubscriptionComplete,
                };

                IdentityResult result = await _repo.CreateAsync(user);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                var info = new ExternalLoginInfo()
                {
                    DefaultUserName = model.UserName,
                    Login = new UserLoginInfo(model.Provider, model.ExternalUserId)
                };

                result = await _repo.AddLoginAsync(user.Id, info.Login);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                //generate access token response
                var accessTokenResponse = GenerateLocalAccessTokenResponse(model.UserName, user.Id, user.SubscriptionComplete);

                return Ok(accessTokenResponse);
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Login Social User
        /// </summary>
        /// <remarks>
        /// Login Social User
        /// </remarks>
        /// <param name="model">Social User instance for Login</param>
        /// <returns>accessTokenResponse</returns>
        /// <response code="200">Access Token</response>
        [AllowAnonymous]
        [Route("ExternalLogin")]
        public async Task<IHttpActionResult> ExternalLogin(LoginExternalBindingModel model)
        {
            try
            {
                ApplicationUser user = await _repo.FindAsync(new UserLoginInfo(model.Provider, model.ExternalUserId));

                bool hasRegistered = user != null;

                if (hasRegistered)
                {

                    //if (user.SubscriptionEndDate < DateTime.Now)
                    //{
                    //    return BadRequest("User's Subscription has Expired.");
                    //}

                    //generate access token response
                    var _accessTokenResponse = GenerateLocalAccessTokenResponse(user.UserName, user.Id, user.SubscriptionComplete);

                    return Ok(_accessTokenResponse);
                }
                else
                {
                    return BadRequest("User Not Found");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Change Password of User
        /// </summary>
        /// <remarks>
        /// Change Password of User
        /// </remarks>
        /// <param name="model">Change Password instance for Login</param>
        /// <returns></returns>
        /// <response code="200">Password Change Successfully.</response>
        [Authorize]
        [Route("ChangePassword")]
        [ModelValidator]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordDTO model)
        {
            try
            {
                IdentityResult result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                EncryptDecryptHelper ecryptDecryptHelper = new EncryptDecryptHelper();
                string encryptedPassword = ecryptDecryptHelper.Encrypt(model.NewPassword);
                var user = _userManager.FindById(User.Identity.GetUserId());
                user.Password = encryptedPassword;
                IdentityResult _result = await _userManager.UpdateAsync(user);

                return Ok("Password Change Successfully.");
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Send Password on User's Email if User Forgot Password 
        /// </summary>
        /// <remarks>
        /// Send Password on User's Email. 
        /// </remarks>
        /// <param name="email">User's Email to send Password</param>
        /// <returns></returns>
        /// <response code="200">Please check your email. Password has been sent.</response>
        [AllowAnonymous]
        [Route("ForgotPassword")]
        [ModelValidator]
        public async Task<IHttpActionResult> ForgotPassword(string email)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(email);
                if (user != null)
                {
                    EncryptDecryptHelper ecryptDecryptHelper = new EncryptDecryptHelper();
                    var password = ecryptDecryptHelper.Decrypt(user.Password);
                    _userManager.EmailService = new EmailService();
                    await _userManager.SendEmailAsync(user.Id, "Your Password", "Your Password is <strong>" + password + "</strong>");
                    return Ok("Please check your email. Password has been sent.");
                }
                else
                {
                    return BadRequest("You are not registered with us. Please check your email.");
                }
            }
            catch (Exception ex)
            {
                string innerMessage = (ex.InnerException != null) ? ((ex.InnerException.InnerException != null) ? ex.InnerException.InnerException.Message : ex.InnerException.Message) : ex.Message;
                return BadRequest(innerMessage);
            }
        }

        /// <summary>
        /// Check User's Email if User Is Already Registered 
        /// </summary>
        /// <remarks>
        /// Check User's Email if User Is Already Registered 
        /// </remarks>
        /// <param name="email">User's Email to Check</param>
        /// <returns></returns>
        /// <response code="200">Please check your email. Password has been sent.</response>
        [AllowAnonymous]
        [Route("CheckUnique")]
        [HttpGet]
        public IHttpActionResult CheckUnique(string email)
        {
            try
            {                
                var uniqueEmail = _userManager.FindByEmail(email);

                if(uniqueEmail!=null)
                {
                    return Ok("Email Already Registered");
                }
                else
                {
                    return Ok("Email Not Registered");
                }
            }
            catch (Exception)
            {
                return BadRequest("Email Not Registered");
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Helpers

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private string GetQueryString(HttpRequestMessage request, string key)
        {
            var queryStrings = request.GetQueryNameValuePairs();

            if (queryStrings == null) return null;

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            if (string.IsNullOrEmpty(match.Value)) return null;

            return match.Value;
        }

        private JObject GenerateLocalAccessTokenResponse(string userName, string userId, bool subscriptioncomplete)
        {
            var tokenExpiration = TimeSpan.FromDays(7);

            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            identity.AddClaim(new Claim("role", "user"));
            identity.AddClaim(new Claim("subscriptioncomplete", subscriptioncomplete.ToString()));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var ticket = new AuthenticationTicket(identity, props);

            var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            JObject tokenResponse = new JObject(
                                        new JProperty("userName", userName),
                                         new JProperty("userId", userId),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()),
                                        new JProperty("subscriptioncomplete", subscriptioncomplete));

            return tokenResponse;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }
            public string ExternalAccessToken { get; set; }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name),
                    ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
                };
            }
        }

        #endregion
    }
}


