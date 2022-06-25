using Domain.Models;
using Domain;
using Domain.Models.Figures;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace WpfApp
{
    class WpfFigurePainter : IFiguresPainter
    {
        ObservableCollection<CellViem> _cells;
        public WpfFigurePainter(ObservableCollection<CellViem> cells)
        {
            _cells = cells;
        }
        public void ChooseFigure(Cell figureCell)
        {
            foreach(var cell in _cells)
            {
                if (cell.X == figureCell.X && cell.Y == figureCell.Y)
                {
                    cell.ChangeBg(Brushes.Brown);
                    return;
                }
            }
        }

        public TransformFigures DrawFigureReplaceSelectionAndGet()
        {
            return TransformFigures.Queen;
        }

        public void MoveFigure(Cell from, Cell to)
        {
            //var f = figures.firstOrD(f => f.x == from.X && f.y == from.y);

            //anim(f, to.X, to.Y);

            
        }
    }
}
