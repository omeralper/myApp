using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using appServer.Models;
using appServer.App_Start;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace appServer.Hubs
{
    public class ChatHub : Hub
    {
        //public void SendMessage(string message,string userId)
        //{
        //    // Clients.All.SendMessage(message);
        //    Clients.User(userId).SendMessage(message);
        //}

        //public override Task OnConnected()
        //{
        //    AssignToSecurityGroup();
        //    Greet();

        //    return base.OnConnected();
        //}

        //private void AssignToSecurityGroup()
        //{
        //    if (Context.User.Identity.IsAuthenticated)
        //        Groups.Add(Context.ConnectionId, "authenticated");
        //    else
        //        Groups.Add(Context.ConnectionId, "anonymous");
        //}

        //private void Greet()
        //{
        //    var greetedUserName = Context.User.Identity.IsAuthenticated ?
        //        Context.User.Identity.Name :
        //        "anonymous";

        //    Clients.Client(Context.ConnectionId).OnMessage(
        //        "[server]", "Welcome to the chat room, " + greetedUserName);
        //}
    }

  

    public class CustomUserIdProvider : IUserIdProvider
    {

        public string GetUserId(IRequest request)
        {
            ApplicationUserManager userManager = request.GetHttpContext().GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (request.User == null)
                return "";
            var user = userManager.FindByName(request.User.Identity.Name);
            return user.Id.ToString();
        }
    }
}