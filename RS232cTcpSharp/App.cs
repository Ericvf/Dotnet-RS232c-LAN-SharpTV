using System;
using System.Threading.Tasks;

namespace RS232cTcpSharp
{
    public class App
    {
        private readonly IRS232cTcpSharpClient rs232cClient;

        public App(IRS232cTcpSharpClient rs232CTcpClient)
        {
            this.rs232cClient = rs232CTcpClient;
        }

        public async Task RunAsync(string[] args)
        {
            Console.WriteLine("RS232cTcpSharp started");

            if (args.Length != 2)
            {
                Console.WriteLine("Please provide a hostname/ipaddress and port.");
                Console.WriteLine("e.g. `RS232cTcpSharp.exe 192.168.1.3 10008`");
                Environment.Exit(1);
            }

            var hostname = args[0];
            var port = Convert.ToInt32(args[1]);
            var response = default(string);

            while (true)
            {
                // Start the connection and login
                await rs232cClient.Start(hostname, port);

                // Execute some default commands
                var model = await rs232cClient.Get(Commands.Model);
                Console.Write($"Model: {model}");

                var serialNo = await rs232cClient.Get(Commands.SerialNo);
                Console.Write($"SerialNo: {serialNo}");

                var powerControl = await rs232cClient.Get(Commands.PowerControl);
                Console.Write($"Power control: {powerControl}");

                var inputSelection = await rs232cClient.Get(Commands.InputModeSelection);
                Console.Write($"Input Selection Mode: {inputSelection}");

                var brightness = await rs232cClient.Get(Commands.Brightness);
                Console.Write($"Brightness: {brightness}");

                // REPL
                while (rs232cClient.IsConnected())
                {
                    Console.Write("> ");

                    // Read
                    var command = Console.ReadLine();
                    if (command == "!")
                    {
                        break;
                    }
                    else if (!string.IsNullOrEmpty(command))
                    {
                        var subCmd = command.Split(' ');
                        if (subCmd.Length == 2)
                        {
                            response = await rs232cClient.Set(subCmd[0], subCmd[1]);
                        }
                        else
                        {
                            response = await rs232cClient.Get(command!);
                        }

                        // Print
                        Console.WriteLine(response);
                    }
                }

                await rs232cClient.Stop();
            }
        }
    }
}