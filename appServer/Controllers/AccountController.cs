using appServer.App_Start;
using appServer.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace appServer.Controllers
{

    public class AccountController : ApiController
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private async Task<FacebookUserViewModel> VerifyFacebookAccessToken(string accessToken)
        {
            FacebookUserViewModel fbUser = null;
            var path = "https://graph.facebook.com/me?fields=email,name,first_name,picture&access_token=" + accessToken;
            var client = new HttpClient();
            var uri = new Uri(path);
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                fbUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUserViewModel>(content);

                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                var url = (obj as JObject)["picture"]["data"]["url"].ToString();
                response = await client.GetAsync(new Uri(url));
                if (response.IsSuccessStatusCode)
                {
                    fbUser.userPicture = await response.Content.ReadAsByteArrayAsync();
                }
            }

            
            return fbUser;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Post(LoginModel loginModel) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tokenExpirationTimeSpan = TimeSpan.FromDays(14);
            ApplicationUser user = null; 

            var fbUser = await VerifyFacebookAccessToken(loginModel.authAccessToken);
            if (fbUser == null)
            {
                return BadRequest("Invalid OAuth access token");
            }


            UserLoginInfo loginInfo = new UserLoginInfo("Facebook", fbUser.id);

            user = await UserManager.FindAsync(loginInfo);

            if(user == null)
            {
                user = new ApplicationUser() {  UserName = fbUser.name, Email = fbUser.email, firstName = fbUser.first_name, photo = fbUser.userPicture };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, loginInfo);
                    if (!result.Succeeded)
                        return BadRequest("cannot add facebook login");
                }
                else
                {
                    return BadRequest("cannot create user");
                }
            }
            
            var identity = new ClaimsIdentity(Startup.OAuthBearerOptions.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Id, null, "Facebook"));
            // This claim is used to correctly populate user id
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id, null, "LOCAL_AUTHORITY"));
            AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(tokenExpirationTimeSpan);
            var accesstoken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
            Authentication.SignIn(identity);

            LoginModel loginDto = new LoginModel() {
                authServer = "facebook",
                authServerId = fbUser.id,
                bearerAccessToken = accesstoken,
                expiresIn = tokenExpirationTimeSpan.TotalSeconds.ToString(),
                issued = ticket.Properties.IssuedUtc.ToString(),
                expires = ticket.Properties.ExpiresUtc.ToString()
            };

            return Ok(loginDto);
        }


    }
}
