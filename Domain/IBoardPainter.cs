using Domain.Models;

namespace Domain
{
    public interface IBoardPainter
    {
        void DrawAvaibleSells(List<Sell> avaibleSells);
        void ResetAvaibleSells();
        void Draw(Sell[,] sells);
    }
}
