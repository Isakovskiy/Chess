using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
            foreach(var cell in avaibleCells)
            {
                var viewCell = _cells.FirstOrDefault(c => c.X == cell.X && c.Y == cell.Y);
                if(viewCell != null)
                {
                    viewCell.ChangeBg(Brushes.White);
                    viewCell.AvaibleForMove = true;
                }
            }
        }

        public void DrawBoard(Cell[,] sells)
        {

            for (int i = 0; i < sells.GetLength(0); i++)
            {
                for (int j = 0; j < sells.GetLength(1); j++)
                {
                    var cell = _cells.FirstOrDefault(c => c.X == i && c.Y == j);
                    cell.ChangeImage(sells[i, j].Figure?.Name + sells[i, j].Figure?.Color.ToString()[0]);
                }
            }
        }

        public void ResetAvaibleCells()
        {

            for(int i = 0; i < _cells.Count; i++)
            {
                _cells[i].ChangeBg(Brushes.Brown);
                _cells[i].AvaibleForMove = false;
            }
        }
    }
}
