using Domain.Models;

namespace Persistence
{
    public interface IBoardPainter
    {
        void Draw(Sell[,] sells);
        void DrawAvaibleSells(List<Sell> avaibleSells);
    }
}