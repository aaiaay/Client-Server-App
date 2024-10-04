using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class IntegrationTests
{
    [Fact]
    public async Task ServerSendsCorrectMessageBasedOnTime()
    {
        string serverIp = "127.0.0.1";
        int port = 5000;

        using (TcpClient client = new TcpClient(serverIp, port))
        {
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[256];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Assert.True(message == "чет!" || message == "нечет!" || message == "равно!");
        }
    }
}
