namespace Example
{
    using System;
    using System.Net.Http;

    class Program
    {
        public static void Main(string[] arguments)
        {
            string message = MyOtherMethod();

            Console.WriteLine(message);

            Console.ReadKey();
        }

        public static string MyOtherMethod()
        {
            var steamId = ulong.MaxValue;

            RustPlayer player = new RustPlayer();
            player.SteamId = steamId;
            player.Username = GetSteamName(steamId);

            var helloMessage = SayHello(player.Username);
            
            return helloMessage;
        }

        public static string GetSteamName(ulong steamId)
        {
            // TODO -- Call steam API to get name

            // TODO -- replace this with the actual steam name
            return "El Travito";
        }

        public static string SayHello(string myName)
        {
            string message;

            if (myName == "Travis")
            {
                message = "Maaaaybe";
            }
            else if (myName == "SirB")
            {
                message = "Doh herro";
            }
            else
            {
                message = $"Hello {myName}";
            }

            return message;
        }
        
        public string name { get; set; }
    }

    public class RustPlayer
    {
        public string Username { get; set; }

        public ulong SteamId { get; set; }

        public RandomClass RandomClass { get; set; }
    }

    public class RandomClass
    {
        public string Name { get; set; }
    }
}
