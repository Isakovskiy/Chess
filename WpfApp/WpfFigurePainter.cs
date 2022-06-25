using Domain.Models;
using Domain;
using Domain.Models.Figures;
using System.Collections.Generic;

namespace WpfApp
{
    class WpfFigurePainter : IFiguresPainter
    {
        List<int> figures = new List<int>();

        public void ChooseFigure(Cell figureCell)
        {
            throw new System.NotImplementedException();
        }

        public TransformFigures DrawFigureReplaceSelectionAndGet()
        {
            throw new System.NotImplementedException();
        }

        public void MoveFigure(Cell from, Cell to)
        {
            //var f = figures.firstOrD(f => f.x == from.X && f.y == from.y);

            //anim(f, to.X, to.Y);

            throw new System.NotImplementedException();
        }
    }
}
