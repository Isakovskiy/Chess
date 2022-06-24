using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
    public class Pawn : Figure
    {
        public Pawn(string image, Cell sell, IFiguresPainter figuresPainter, FigureColor color = FigureColor.Black) : base(image, sell, figuresPainter, color)
        {
        }
        public override List<Cell> GetAvaibleCells(Cell[,] boardSells)
        {
            var list = new List<Cell>();
            var dir = Color == FigureColor.Black ? -1 : 1;

            var x = CurrentCell.X;
            var y = CurrentCell.Y;

            if (CanMoveTo(x, y + dir, boardSells))
            {
                list.Add(boardSells[x, y + dir]);

                if(!Moved && CanMoveTo(x, y + dir * 2, boardSells))
                {
                    list.Add(boardSells[x, y + dir * 2]);
                }
            }
            for(int i = -1; i <= 1; i += 2)
            {
                if (CanMoveTo(x + i, y + dir, boardSells) && boardSells[x + i, y + dir].Figure != null)
                {
                    list.Add(boardSells[x + i, y + dir]);
                }
            }

            return list;
        }

        public override void Move(Cell newCell)
        {
            base.Move(newCell);

            if(newCell.Y == 0 || newCell.Y == Board.SIZE - 1)
            {
                var newFigure = FiguresPainter.DrawFigureReplaceSelection(newCell);
                //newFigure.
            }
        }

    }


}
