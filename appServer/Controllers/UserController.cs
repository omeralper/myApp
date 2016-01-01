using appServer.App_Start;
using appServer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
//using Omu.ValueInjecter;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace appServer.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private ApplicationUserManager _userManager;
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

        [ResponseType(typeof(MeDTO))]
        public async Task<IHttpActionResult> GetUser()
        {
            ApplicationUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            MeDTO dto = new MeDTO() {
                UserId = User.Identity.GetUserId(),
                firstName = user.firstName,
                photo = user.photo,
                UserName = user.UserName,
                authServer = user.Logins.First().LoginProvider,
                authServerId = user.Logins.First().ProviderKey
            };
            //dto.InjectFrom(user);
            return Ok(dto);
        }

        public async Task<IHttpActionResult> GetUser(string userId)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(userId);
            UserDTO dto = new UserDTO()
            {
                //userId = User.Identity.GetUserId(),
                firstName = user.firstName,
                photo = user.photo,
                userName = user.UserName,
                //authServer = user.Logins.First().LoginProvider,
                //authServerId = user.Logins.First().ProviderKey
            };

            return Ok(dto);
        }
    }
}
