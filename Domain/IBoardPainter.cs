using Domain.Models;

namespace Domain
{
    public interface IBoardPainter
    {
        void DrawAvaibleCells(List<Cell> avaibleCells);
        void ResetAvaibleCells();
        void DrawBoard(Cell[,] sells);
    }
}
