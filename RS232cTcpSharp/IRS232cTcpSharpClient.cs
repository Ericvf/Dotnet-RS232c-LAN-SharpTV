using System.Threading.Tasks;

namespace RS232cTcpSharp
{
    public interface IRS232cTcpSharpClient
    {
        Task<string> Get(string command);

        Task<string> Get(Commands command);

        Task<string> Set(Commands command, int value);

        bool IsConnected();

        Task Start(string ipAddress, int port, string? username = null, string? password = null);

        Task Stop();

    }
}