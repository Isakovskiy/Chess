using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Models;

namespace WpfApp
{
    class WpfBoardPainter : IBoardPainter
    {
        ObservableCollection<CellViem> _cells;

        public WpfBoardPainter(ObservableCollection<CellViem> cells)
        {
            _cells = cells;
        }

        public void DrawAvaibleCells(List<Cell> avaibleCells)
        {
            throw new NotImplementedException();
        }

        public void DrawBoard(Cell[,] sells)
        {
            for(int i = 0; i < sells.GetLength(0); i++)
            {
                for(int j = 0; j < sells.GetLength(1); j++)
                {
                    var cell = _cells.FirstOrDefault(c => c.X == i && c.Y == j);
                    if (sells[i, j].Figure?.Name != cell?.FigureName && sells[i, j].Figure?.Color != cell?.FigureColor)
                    {
                        cell?.ChangeImage(sells[i, j].Figure?.Name + ((sells[i, j].Figure.Color == FigureColor.Black) ? "B" : "W"));
                    }
                }
            }
        }

        public void ResetAvaibleCells()
        {
            throw new NotImplementedException();
        }
    }
}
