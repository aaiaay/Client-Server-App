using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main(string[] args)
    {
        IPAddress ip = IPAddress.Any;
        int port = 5000;
        TcpListener listener = new TcpListener(ip, port);

        listener.Start();
        Console.WriteLine($"Сервер запущен на порту {port}");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Клиент подключен");

            NetworkStream stream = client.GetStream();

            while (client.Connected)
            {
                DateTime now = DateTime.Now;
                string message = GetMessage(now);

                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Отправлено сообщение: {message}");

                System.Threading.Thread.Sleep(1000);
            }

            stream.Close();
            client.Close();
        }
    }

    static string GetMessage(DateTime now)
    {
        string timeString = $"{now:yyyyMMddHHmmss}";
        int evenCount = 0, oddCount = 0;

        foreach (char c in timeString)
        {
            int digit = c - '0';
            if (digit % 2 == 0)
                evenCount++;
            else
                oddCount++;
        }

        if (evenCount > oddCount)
            return "чет!";
        else if (oddCount > evenCount)
            return "нечет!";
        else
            return "равно!";
    }
}
