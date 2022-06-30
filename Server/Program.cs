using Domain.Models;
using Domain;
using System.Net;
using System.Net.Sockets;
using Domain.Models.Figures;
using System.Text;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace Server
{
	static class Program
	{
		private static TcpListener server = null;
		private static TcpListener secondPlayerListener = null;
		private static NetworkStream stream1;
		private static NetworkStream stream2;
		private static TcpClient client1;
		private static TcpClient client2;

		private static Chess _chess;
		private static BoardPainter _borderPointer;
		private static FigurePainter _figurePointer;

		private static Packet[] _packets = new Packet[64];
		private static void Getting(NetworkStream stream, FigureColor color)
		{
			try
			{
				while (true)
				{
					byte[] data = new byte[256];
					StringBuilder response = new StringBuilder();

					do
					{
						int bytes = stream.Read(data, 0, data.Length);
						response.Append(Encoding.UTF8.GetString(data, 0, bytes));
					}
					while (stream.DataAvailable);
					string input = response.ToString();

					if (input != null && _chess.GoingPlayer == color)
					{
						int x = int.Parse(input.Split(' ').ToArray()[0]);
						int y = int.Parse(input.Split(' ').ToArray()[1]);

						if (_chess.AvaibledCells?.FirstOrDefault(c => c.X == x && c.Y == y) != null)
						{
							GameResult result = _chess.Move(x, y);
							if (result != GameResult.Going)
							{
								//game the end
							}
							Send(new[] { stream1, stream2 }, result);
						}
						else
						{
							_chess.ChooseFigure(x, y);
							Send(new[] { stream }, GameResult.Going);
						}
					}
				}
			}
			catch
			{
				Console.WriteLine("CLOSE!");
				stream1.Close();
				stream2.Close();
				client1.Close();
				client2.Close();
				if (server != null)
				{
					server.Stop();
				}
				if (secondPlayerListener != null)
				{
					secondPlayerListener.Stop();
				}
			}
		}
		public static void Main()
		{
			_borderPointer = new BoardPainter(_packets);
			_figurePointer = new FigurePainter(_packets);
			_chess = new Chess(_borderPointer, _figurePointer);

			IPAddress localAddr = IPAddress.Parse("26.167.190.81");
			server = new TcpListener(localAddr, 8888);
			server.Start();

			Console.WriteLine("Ожидание подключений...");

			client1 = server.AcceptTcpClient();
			Console.WriteLine("Подключен 1 клиент");

			client2 = server.AcceptTcpClient();
			Console.WriteLine("Подключен 2 клиент");

			stream1 = client1.GetStream();
			stream2 = client2.GetStream();

			Thread t1 = new Thread(() => Getting(stream1, FigureColor.White));
			t1.Start();
			Thread t2 = new Thread(() => Getting(stream2, FigureColor.Black));
			t2.Start();

			StartSend();
		}
		private static void StartSend()
		{
			try
			{
				Package[] packeges = new[]
				{
					new Package("White", GameResult.Going.ToString(), _packets),
					new Package("Black", GameResult.Going.ToString(), _packets)
				};
				NetworkStream[] streams = new[] { stream1, stream2 };

				for (int i = 0; i < 2; i++)
				{
					string jsonStr = JsonSerializer.Serialize<Package>(packeges[i]);
					byte[] data = Encoding.UTF8.GetBytes(jsonStr);
					streams[i].Write(data, 0, data.Length);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		public static void Send(NetworkStream[] streams, GameResult result)
		{
			try
			{
				Package packege = new Package(_chess.GoingPlayer.ToString(), result.ToString(), _packets);
				string jsonStr = JsonSerializer.Serialize<Package>(packege);
				byte[] data = Encoding.UTF8.GetBytes(jsonStr);

				foreach(var s in streams)
					s.Write(data, 0, data.Length);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}


