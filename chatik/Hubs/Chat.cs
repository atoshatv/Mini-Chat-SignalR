using chatik.Models;
using Microsoft.AspNetCore.SignalR;

namespace chatik.Hubs
{
    public class Chat : Hub
    {

        static List<Message> messages = new List<Message>();
        
        
       

        

        public async Task NewMessage(Message message)
        {
           await Clients.All.SendAsync("newMessage", message);
            messages.Add(message);
            
        }
        public async Task NewUser(string user)
        {
            await Clients.Caller.SendAsync("previous", messages);
            await Clients.All.SendAsync("user", user);
          
        }
        
    }
}
