using System.Net;
using System.Net.Sockets;
using System.Text;
using WinFormsApp;
using System.Text.Json;
using Server;

namespace Client
{
    internal static class Program
    {
        private const string server = "127.0.0.1";
        private const int port = 8888;

        [STAThread]
        static void Main()
        {
            //var host = System.Net.Dns.GetHostName(); 
            //var ip = System.Net.Dns.GetHostByName(host).AddressList[0];

            TcpClient client = new TcpClient();

            client.Connect(server, port);

            byte[] data = new byte[256];
            StringBuilder response = new StringBuilder();

            NetworkStream stream = client.GetStream();

            do
            {
                int bytes = stream.Read(data, 0, data.Length);
                response.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable); // пока данные есть в потоке

            var x = response.ToString();
			Packet[]? packets = JsonSerializer.Deserialize<Packet[]>(response.ToString());

            MessageBox.Show("errror");

            stream.Close();
            client.Close();

            ApplicationConfiguration.Initialize();

            //Application.Run(new ChessForm());
        }
    }


}