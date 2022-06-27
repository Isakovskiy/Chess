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
							var res = _chess.Move(x, y);
							if (res != GameResult.Going)
							{
//								MessageBox.Show(res.ToString());
							}
							Send(new[] { stream1, stream2 });
						}
						else
						{
							_chess.ChooseFigure(x, y);
							Send(new[] { stream });
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

			IPAddress localAddr = IPAddress.Parse("26.0.154.116");
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

			Send(new NetworkStream[] {stream1, stream2});

			//check chei hod
			//get x, y
			//choose figure (x, y)
			//send to (1)
			//get x, y
			//move(x, y)
			//send to (all)
		}
		public static void Send(NetworkStream[] stream)
		{
			try
			{
				var x = JsonSerializer.Serialize<Packet[]>(_packets);
				byte[] data = Encoding.UTF8.GetBytes(x);

				foreach(var s in stream)
					s.Write(data, 0, data.Length);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
	class BoardPainter : IBoardPainter
	{
		private Packet[] packets;
		public BoardPainter(Packet[] pack)
		{
			packets = pack;
		}
		public void DrawAvaibleCells(List<Cell> avaibleCells)
		{
			foreach (Cell cell in avaibleCells)
			{
				packets.First(p => p.X == cell.X && p.Y == cell.Y).CellColor = "Blue";
			}
		}

		public void DrawBoard(Cell[,] sells)
		{
			int i = 0;
			for(int x = 0; x < sells.GetLength(0); x++)
			for (int y = 0; y < sells.GetLength(1); y++)
			{
				if(x == 5 && y == 2)
					{

					}
				packets[i] = new Packet(
					x:x,
					y:y,
					figureName: sells[x, y].Figure?.Name,
					figureColor: sells[x, y].Figure?.Color.ToString(),
					cellColor: ((x + y) % 2 == 0) ? "DimGray" : "White");
				i++;
			}
		}

		public void ResetAvaibleCells()
		{
			foreach (Packet p in packets.Where(p => p.CellColor == "Blue"))
				p.CellColor = (p.X + p.Y) % 2 == 0 ? "DimGray" : "White";
		}
	}

	class FigurePainter : IFiguresPainter
	{
		private Packet[] packets;
		public FigurePainter(Packet[] pack)
		{
			packets = pack;
		}
		public void CancelFigure(Figure figure)
		{
			Packet p = packets.First(p => p.Y == figure.CurrentCell.Y && p.X == figure.CurrentCell.X);
			p.CellColor = (p.X + p.Y) % 2 == 0 ? "DimGray" : "White";
		}

		public void ChooseFigure(Cell figureCell)
		{
			packets.First(p => p.X == figureCell.X && p.Y == figureCell.Y).CellColor = "Yellow";
		}

		public TransformFigures DrawFigureReplaceSelectionAndGet()
		{
			return TransformFigures.Queen;
		}

		public void MoveFigure(Cell from, Cell to)
		{
			
		}
	}
}


