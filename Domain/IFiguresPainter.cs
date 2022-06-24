using Domain.Models;
using Domain.Models.Figures;

namespace Domain
{
    public interface IFiguresPainter
    {
        /// <summary>
        /// Не бейте, мы пытались придумать что-то нормальное, но получилось это...
        /// </summary>
        /// <returns>Тип фигуры, на которую нажали</returns>
        TransformFigures DrawFigureReplaceSelectionAndGet();
        void ChooseFigure(Cell figureCell);
        void MoveFigure(Cell from, Cell to);
    }
}
