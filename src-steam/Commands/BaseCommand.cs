namespace TerraPanel.Concept.Commands
{
    internal class BaseCommand
    {
        public ServerCommand ServerCommand { get; set; }

        public string[] Args { get; set; }

        public BaseCommand(ServerCommand serverCommand, string[] args)
        {
            this.ServerCommand = serverCommand;
            this.Args = args;
        }
    }
}
