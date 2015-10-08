namespace TerraPanel.Concept
{
    using System;

    class Example
    {
        public string SayHello(string myName)
        {
            if (myName == "Travis")
            {
                return "Hello Travis";
            }
            else
            {
                return "Hello SirB";
            }
        }
        
        public void MyOtherMethod()
        {
            RustPlayer player = new RustPlayer();
            player.Username = "Travis";
            player.SteamId = ulong.MaxValue;

            string helloMessage = this.SayHello(player.Username);
        }
        
        private object myObject = null;

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


// !=

