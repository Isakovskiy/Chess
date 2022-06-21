using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessWF.Models.Figures
{
    class Knight : Figure
    {
        public Knight(string image, bool isBlack = false) : base(image, isBlack)
        {
        }

        public override List<Sell> GetAvaibleSells(Sell figureSell, Sell[,] boardSells)
        {
            var list = new List<Sell>();
            Tuple<int, int>[] tuple = new Tuple<int, int>[8]
            {
                new Tuple<int, int>(-2, 1),
                new Tuple<int, int>(-2, -1),
                new Tuple<int, int>(2, 1),
                new Tuple<int, int>(2, -1),
                new Tuple<int, int>(-1, 2),
                new Tuple<int, int>(-1, -2),
                new Tuple<int, int>(1, 2),
                new Tuple<int, int>(1, -2),

            };

            for(int i = 0; i < tuple.Length; i++)
            {
                int x = figureSell.X + tuple[i].Item1;
                int y = figureSell.Y + tuple[i].Item2;

                if (CanMoveTo(x, y, boardSells))
                {
                    list.Add(boardSells[x, y]);
                }
            }
            return list;
        }
    }
}
