using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace appServer.Models
{

    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string firstName { get; set; }
        public byte[] photo { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    // Add custom user claims here
        //    return userIdentity;
        //}
    }

    public class FacebookUserViewModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string email { get; set; }
        public byte[] userPicture { get; set; }
    }
    
    public class MeDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string firstName { get; set; }
        public byte[] photo { get; set; }
        public string authServer { get; set; }
        public string authServerId { get; set; }
    }

    public class UserDTO
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public byte[] photo { get; set; }
        //public string authServer { get; set; }
        //public string authServerId { get; set; }
    }

    public class LoginModel
    {
        public string authServer { get; set; }
        public string authAccessToken { get; set; }
        public string authServerId { get; set; }
        public string bearerAccessToken { get; set; }
        public string expiresIn { get; set; }
        public string issued { get; set; }
        public string expires { get; set; }
    }
}
