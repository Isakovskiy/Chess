
using Domain.Models;

namespace Domain
{
    public interface IFiguresPainter
    {
        Figure DrawFigureReplaceSelection(Cell figureCell);
        void ChooseFigure(Cell figureCell);
    }
}
