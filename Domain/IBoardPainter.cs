using Domain.Models;

namespace Domain
{
    public interface IBoardPainter
    {
        void DrawAvaibleSells(List<Cell> avaibleSells);
        void ResetAvaibleSells();
        void DrawBoard(Cell[,] sells);
        void ChooseFigure(Cell figureSell);
    }
}
