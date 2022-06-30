using Domain;
using Domain.Models;
using System.Drawing;
using System.Linq;
using Server;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Text.Json;
using System.IO;

namespace WinFormsApp
{
	public partial class ChessForm : Form
	{
		private TcpClient client;
		NetworkStream stream;

		public const int CELLSIZE = 75;

		private Chess _chess;
		private Packet[]? _packets;

		private FigureColor? myColor = null;

		CellView[,] _cells = new CellView[Board.SIZE, Board.SIZE];

		public ChessForm()
		{
			InitializeComponent();
		}

		private void CreateCellButtons(Action<int, int> click)
		{
			for(int i = 0; i < Board.SIZE; i++)
			{
				for(int j = 0; j < Board.SIZE; j++)
				{
					_cells[i, j] = new CellView(((i + j) % 2 == 0) ? Color.DimGray : Color.White, new Point(i * CELLSIZE, (7 - j) * CELLSIZE), click, i, j);
					Controls.Add(_cells[i, j]);
				}
			}
		}

		private void button_Click_Solo(int x, int y)
		{
			if (_chess.AvaibledCells?.FirstOrDefault(c => c.X == x && c.Y == y) != null)
			{
				var res = _chess.Move(x, y);
				if (res != GameResult.Going)
				{
					MessageBox.Show(res.ToString());
				}
			}
			else
			{
				_chess.ChooseFigure(x, y);
			}
		}
		private void button_Click_Multi(int x, int y)
		{
			if(myColor == FigureColor.Black)
			{
				x = 7 - x;
				y = 7 - y;
			}
			byte[] data = Encoding.UTF8.GetBytes($"{x} {y}");

			stream.Write(data, 0, data.Length);
		}
		private void Getting()
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
					Package package = JsonSerializer.Deserialize<Package>(response.ToString());
					if(myColor == null)
					{
						myColor = (package?.WhoGoing == "White") ? FigureColor.White : FigureColor.Black;
						package.WhoGoing = "White";
					} 
					_packets = package?.Packets;

					if (_packets != null)
					{
						foreach (Packet p in _packets)
						{
							int x = p.X;
							int y = p.Y;
							if(myColor == FigureColor.Black)
							{
								x = 7 - x;
								y = 7 - y;
							}
							if (p.FigureName != null)
								_cells[x, y].Image = new Bitmap(Image.FromFile($@"..\..\..\Figures\{p.FigureName}{p.FigureColor.ToString()[0]}.png"), new Size(75, 75));
							else
								_cells[x, y].Image = null;
							switch (p.CellColor)
							{
								case "White":
									_cells[x, y].BackColor = Color.White;
									break;
								case "Yellow":
									_cells[x, y].BackColor = Color.FromArgb(50, Color.Yellow);
									break;
								case "Blue":
									_cells[x, y].BackColor = Color.FromArgb(50, Color.Blue);
									break;
								case "DimGray":
									_cells[x, y].BackColor = Color.DimGray;
									break;
								default:
									break;
							}
						}
					}
				}
			}
			catch
			{
				Application.Exit();
			}
			finally
			{
				client.Close();
				stream.Close();
			}
		}

		private void SoloButton_Click(object sender, EventArgs e)
		{
			Controls.Remove(this.soloButton);
			Controls.Remove(this.multiButton);
			CreateCellButtons(button_Click_Solo);
			_chess = new Chess(new BoardPainter(_cells), new s(_cells));
		}

		private void MultiButton_Click(object sender, EventArgs e)
		{
			Controls.Remove(this.soloButton);
			Controls.Remove(this.multiButton);
			CreateCellButtons(button_Click_Multi);

			client = new TcpClient();
			client.Connect("26.167.190.81", 8888);
      
			stream = client.GetStream();
			Thread myThread1 = new Thread(Getting);
			myThread1.Start();
		}
	}
}