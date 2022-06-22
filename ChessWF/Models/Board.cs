using ChessWF.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models
{
    public class Board
    {
        public const int SIZE = 8;
        public Sell[,] Sells { get; set; }

        public List<Figure> Figures { get; set; }
        public Board()
        {
            Sells = GetEmptyBoard();

            Figures = new List<Figure>();
            Figures.Add(new Bishop(image: "im"));

            Sells[4, 4].Figure = Figures[0];
            Sells[3, 3].Figure = Figures[0];
            Sells[3, 5].Figure = Figures[0];
            Sells[5, 3].Figure = Figures[0];
            
        }

        /// <summary>
        /// Возаращает список клеток на которые может пойти фигугра, находящаяся в выбранной координате
        /// </summary>
        /// <param name="x">Выбранная координата</param>
        /// <param name="y">Выбранная координата</param>
        /// <returns></returns>
        public List<Sell> FindAvaibleSells(int x, int y, out Figure choosedFigure)
        {
            choosedFigure = null;

            try
            {
                choosedFigure = Sells[x, y].Figure;
            }
            catch
            {
                throw new Exception("Out of board bounds");
            }

            return choosedFigure?.GetAvaibleSells(Sells[x, y], Sells);
        }


        public static Sell[,] GetEmptyBoard()
        {
            Sell[,] sells = new Sell[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    sells[i, j] = new Sell() { X = i, Y = j };
                }
            }

            return sells;
        }
    }
}
