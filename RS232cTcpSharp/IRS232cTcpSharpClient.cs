using System.Threading.Tasks;

namespace RS232cTcpSharp
{
    public interface IRS232cTcpSharpClient
    {
        string Get(string command);

        string Get(Commands command);

        string Set(string command, string value);

        string Set(Commands command, int value);

        bool IsConnected();

        Task Start(string ipAddress, int port, string? username = null, string? password = null);

        void Stop();
    }
}