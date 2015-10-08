namespace TerraPanel.Concept
{
    using System;
    using System.Linq;
    using System.Threading;

    using TerraPanel.Concept.Commands;

    class Program
    {
        private static void Main(string[] startParams)
        {
            PerformInitialSetup();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("TP > ");
                Console.ForegroundColor = ConsoleColor.White;
                var args = new[] { Console.ReadLine() };

                // Try cast args to command
                var baseCommand = TryCastToCommand(args);

                if (baseCommand != null)
                {
                    // Handle command + return result
                    TryHandleCommand(baseCommand);
                }
            }
        }

        private static void PerformInitialSetup()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "TerraPanel CLI";

            Console.WriteLine("TerraPanel CLI 1.0-5612a");
            Console.WriteLine("Copyright (c) 2015 Prybar Logic. All Rights Reserved.");

            Console.WriteLine();

            var port = new Random().Next(15000, 45000);
            Console.WriteLine($"Connected to TerraPanel service running on localhost:{port}");
            Console.WriteLine("System is live. Type 'help' to view available commands.");

            Console.WriteLine();
            Console.WriteLine();
            
            Console.WriteLine("NOTE: THE TERRAPANEL CLI IS A PROOF OF CONCEPT. NOTHING IS REALLY HAPPENING");
            Console.WriteLine("WHEN YOU RUN COMMANDS.");

            Console.WriteLine();
        }

        private static BaseCommand TryCastToCommand(string[] args)
        {
            var first = args[0];
            var split = first.Split(' ');

            try
            {
                var serverCommand = (ServerCommand)Enum.Parse(typeof(ServerCommand), split[0], true);
                return new BaseCommand(serverCommand, split.Skip(1).ToArray());
            }
            catch
            {
                // Write error message
                Console.WriteLine();
                WriteError($"Command '{split[0]}' not found");
                Console.WriteLine();
                return null;
            }
        }

        private static void TryHandleCommand(BaseCommand baseCommand)
        {
            Console.WriteLine();

            switch (baseCommand.ServerCommand)
            {
                case ServerCommand.Install:
                    InstallGame(baseCommand.Args[0]);
                    break;
                case ServerCommand.Update:
                    UpdateGame(baseCommand.Args[0]);
                    break;
                case ServerCommand.Remove:
                    RemoveGame(baseCommand.Args[0]);
                    break;

                case ServerCommand.Start:
                    StartServer(baseCommand.Args[0]);
                    break;
                case ServerCommand.Stop:
                    StopServer(baseCommand.Args[0]);
                    break;
                case ServerCommand.Restart:
                    RestartServer(baseCommand.Args[0]);
                    break;

                case ServerCommand.Exit:
                case ServerCommand.Quit:
                    Environment.Exit(0);
                    break;

                case ServerCommand.Ls:
                    ShowInstalledServers();
                    break;

                //case ServerCommand.Status:
                //    break;

                case ServerCommand.Help:
                    ShowHelp();
                    break;
            }

            Console.WriteLine();
        }

        #region Commands

        private static void InstallGame(string gameName)
        {
            WriteInfo($"Searching TGD for '{gameName}' ...");
            Thread.Sleep(200);
            WriteInfo($"Game server '{gameName}' found in TGD!");
            Thread.Sleep(200);
            WriteInfo($"Downloading SteamCMD to C:\\TerraPanel\\Games ... for '{gameName}'...");
            Thread.Sleep(200);
            WriteInfo($"Downloading SteamCMD for '{gameName}' successfull...");
            Thread.Sleep(200);
            WriteInfo($"Downloading {gameName} server via SteamCMD to C:\\TerraPanel\\Games ...");
            Thread.Sleep(5500);
            WriteInfo("Installing server ...");
            Thread.Sleep(1500);
            WriteInfo("Setting up default configuration ...");
            Thread.Sleep(500);
            WriteInfo("Configuring firewall ...");
            Thread.Sleep(500);
            Console.WriteLine();
            WriteSuccess("Install complete!");
            Console.WriteLine();
            WriteInfo($"File path: C:\\TerraPanel\\Games\\{gameName}\\");
            WriteInfo($"Use 'start {gameName}' to start the server");
        }

        private static void UpdateGame(string gameName)
        {
            WriteInfo($"Checking TGD for update to '{gameName}' ...");
            Thread.Sleep(500);
            WriteInfo("An update is available!");
            WriteInfo("Game server is currently running - saving, then stopping ...");
            Thread.Sleep(2500);
            WriteInfo("Downloading update via SteamCMD ...");
            Thread.Sleep(5500);
            Console.WriteLine();
            WriteSuccess("Update complete!");
            Console.WriteLine();
            WriteInfo($"File path: C:\\TerraPanel\\Games\\{gameName}\\");
            WriteInfo($"Use 'start {gameName}' to start the server");
        }

        private static void RemoveGame(string gameName)
        {
            WriteInfo($"Removing '{gameName}' ...");
            WriteInfo("Game server is currently running - saving, then stopping ...");
            Thread.Sleep(2500);
            WriteInfo("Deleting files ...");
            Thread.Sleep(2500);
            WriteInfo("Configuring firewall ...");
            Thread.Sleep(500);
            Console.WriteLine();
            WriteSuccess("Uninstall complete!");
        }


        private static void StartServer(string serverId)
        {
            WriteInfo($"Starting game server id {serverId} ...");
            Thread.Sleep(2500);
            Console.WriteLine();

            var port = new Random().Next(15000, 45000);
            WriteSuccess($"Server now running on port {port}");
        }

        private static void RestartServer(string serverId)
        {
            WriteInfo("Saving ...");
            WriteInfo($"Stopping game server id {serverId} ...");
            Thread.Sleep(2500);
            WriteInfo($"Starting game server id {serverId} ...");
            Thread.Sleep(2500);
            Console.WriteLine();

            var port = new Random().Next(15000, 45000);
            WriteSuccess($"Server now running on port {port}");
        }

        private static void StopServer(string serverId)
        {
            WriteInfo("Saving ...");
            WriteInfo($"Stopping game server id {serverId} ...");
            Thread.Sleep(2500);
            Console.WriteLine();

            WriteSuccess("Server stopped");
        }


        private static void ShowInstalledServers()
        {
            Console.WriteLine("Installed servers:");
            Console.WriteLine();
            Console.WriteLine("GAME               PORT        ID               STATUS");
            Console.WriteLine();
            Console.WriteLine("rust               28721       rust-hy4os9a     RUNNING");
            Console.WriteLine("dayz-standalone    24886       dayz-8b7n6g8     STOPPED");
            Console.WriteLine("csgo               26622       csgo-ge6s2fe     RUNNING");
        }


        private static void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("INSTALL / UPDATE / REMOVE <GAME>");
            Console.WriteLine("  Download, update or remove the specified game server");
            Console.WriteLine();

            Console.WriteLine("START / RESTART / STOP <GAME SERVER ID>");
            Console.WriteLine("  Starts, restarts or stops the specified game server");
            Console.WriteLine();

            Console.WriteLine("LS");
            Console.WriteLine("  Show information about installed game servers - including their server id");
            Console.WriteLine();

            Console.WriteLine("STATUS <GAME SERVER ID>");
            Console.WriteLine("  Show the status (eg. running, stopped) of the specified game server");
            Console.WriteLine();
        }


        #endregion

        private static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"[ERROR] {message}");

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void WriteSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine($"[SUCCESS] {message}");

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void WriteInfo(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }
    }
}
