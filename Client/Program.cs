using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        string serverIp = "127.0.0.1";
        int port = 5000;

        try
        {
            TcpClient client = new TcpClient(serverIp, port);
            Console.WriteLine("Подключение к серверу...");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Получено сообщение: {message}");
            }

            stream.Close();
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
