using System.Threading.Tasks;

namespace DotnetRS232cLAN
{
    public interface IRS232cTcpClient
    {
        Task<string> Get(string command);

        Task<string> Get(Commands command);

        Task<string> Set(Commands command, int value);

        bool IsConnected();

        Task Start(string ipAddress, int port, string? username = null, string? password = null);

        Task Stop();

    }
}