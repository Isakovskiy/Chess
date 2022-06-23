using Domain.Models;

namespace Domain
{
    public interface IBoardPainter
    {
        void DrawAvaibleSells(List<Sell> avaibleSells);
        void ResetAvaibleSells();
        void DrawBoard(Sell[,] sells);
        void ChooseFigure(Sell figureSell);
    }
}
