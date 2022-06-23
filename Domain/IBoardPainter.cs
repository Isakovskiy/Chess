using Domain.Models;

namespace Domain
{
    public interface IBoardPainter
    {
        void DrawAvaibleSells(List<Sell> avaibleSells);
        void Draw(Sell[,] sells);
    }
}
