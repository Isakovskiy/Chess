using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Figures
{
    public class Pawn : Figure
    {
        public Pawn(Cell sell, IFiguresPainter figuresPainter, FigureColor color = FigureColor.Black) : base(sell, figuresPainter, color)
        {
        }
        public override string Name => "Pawn";
        public override List<Cell> GetAvaibleCells(Cell[,] boardSells)
        {
            var list = new List<Cell>();
            var dir = Color == FigureColor.Black ? -1 : 1;

            var x = CurrentCell.X;
            var y = CurrentCell.Y;

            if (CanMoveTo(x, y + dir, boardSells) && boardSells[x, y + dir].Figure == null)
            {
                list.Add(boardSells[x, y + dir]);

                if(!Moved && CanMoveTo(x, y + dir * 2, boardSells) && boardSells[x, y + dir * 2].Figure == null)
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
                var newFigureType = FiguresPainter.DrawFigureReplaceSelectionAndGet();
                switch (newFigureType)
                {
                    case TransformFigures.Rook:
                        new Rook(newCell, FiguresPainter, Color);
                        break;
                    case TransformFigures.Queen:
                        new Queen(newCell, FiguresPainter, Color);
                        break;
                    case TransformFigures.Bishop:
                        new Bishop(newCell, FiguresPainter, Color);
                        break;
                    case TransformFigures.Knight:
                        new Knight(newCell, FiguresPainter, Color);
                        break;
                    default:
                        break;
                }

            }
        }

    }


}
