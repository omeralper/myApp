using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Host.SystemWeb;
using appServer.Models;
using Microsoft.Owin.Security.OAuth;
using appServer.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using appServer.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(appServer.App_Start.Startup))]

namespace appServer.App_Start
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        //public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        //public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; set; }
        //public static string PublicClientId { get; private set; }


        public void Configuration(IAppBuilder app)
        {
            //var idProvider = new CustomUserIdProvider();
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);
            
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            
            //  PublicClientId = "self";

            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    //TokenEndpointPath = new PathString("/Token"),
            //    Provider = new ApplicationOAuthProvider(PublicClientId),
            //    //AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            //    #if DEBUG
            //    AllowInsecureHttp = true
            //    #endif
            //};

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions()
            {
                //AccessTokenFormat = OAuthOptions.AccessTokenFormat,
                //AccessTokenProvider = OAuthOptions.AccessTokenProvider,
                //AuthenticationMode = OAuthOptions.AuthenticationMode,
                //AuthenticationType = OAuthOptions.AuthenticationType,
                //Description = OAuthOptions.Description,
                Provider = new CustomBearerAuthenticationProvider(),
                //SystemClock = OAuthOptions.SystemClock
            };
            OAuthBearerAuthenticationExtensions.UseOAuthBearerAuthentication(app, OAuthBearerOptions);

            app.Map("/signalr", map =>
            {
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    // EnableJSONP = true
                };
                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });
            //app.MapSignalR();
        }

        public class CustomBearerAuthenticationProvider : OAuthBearerAuthenticationProvider
        {
            public override Task ValidateIdentity(OAuthValidateIdentityContext context)
            {
                var claims = context.Ticket.Identity.Claims;
                if (claims.Count() == 0 || claims.Any(claim => claim.Issuer != "Facebook" && claim.Issuer != "LOCAL_AUTHORITY"))
                    context.Rejected();
                return Task.FromResult<object>(null);
            }
        }
    }
}
