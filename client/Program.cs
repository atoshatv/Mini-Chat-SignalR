using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Cryptography.X509Certificates;
// See https://aka.ms/new-console-template for more information

namespace client
{
    internal class Program
    {
        static HubConnection connection;
        static string name;

        static async Task Main(string[] args)
        {
            name = Console.ReadLine()!;
            Connect();
            while (true)
            {
                var message = Console.ReadLine()!;
                var position = Console.GetCursorPosition();
                Console.SetCursorPosition(0, position.Top - 1);
                await SendMessage(message);
            }



        }

        static async Task SendMessage(string message)
        {
            await connection.SendAsync("NewMessage", name, message);

        }
        public static async Task Connect()
        {
            connection = new HubConnectionBuilder()
               .WithUrl("https://localhost:44389/chat")
               .Build();

            connection.On<string, string>("newMessage", (user, message) =>
            {
                Console.WriteLine($"{user}:{message}");

            });
            connection.On<string>("user", (user) =>
            {
                Console.WriteLine($"{user}: ENTER");

            });
            connection.On<List<Message>>("previous", (messages) =>
            {
                foreach (var message in messages)
                {
                    Console.WriteLine(message);
                }


            });
            await connection.StartAsync();
            await connection.SendAsync("NewUser", name);



        }
        public class Message
        {
            public string User { get; set; }
            public string Text { get; set; }
            public override string ToString()
            {
                return $"{User}:{Text}";
            }
        }

    }

}




