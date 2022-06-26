using Domain;
using Domain.Models;
using System.Drawing;
using System.Linq;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public const int CELLSIZE = 75;

        private Chess _chess;

        CellView[,] _cells = new CellView[Board.SIZE, Board.SIZE];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < Board.SIZE; i++)
            {
                for(int j = 0; j < Board.SIZE; j++)
                {
                    _cells[i, j] = new CellView(((i + j) % 2 == 0) ? Color.DimGray : Color.White, new Point(i * CELLSIZE, (7 - j) * CELLSIZE), button_Click, i, j);
                    Controls.Add(_cells[i, j]);
                }
            }
            _chess = new Chess(new BoardPainter(_cells), new s(_cells));

        }

        private void button_Click(int x, int y)
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
    }

}