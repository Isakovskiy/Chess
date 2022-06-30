using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MoveRecord
    {
        public MoveRecord(Figure figure, Cell from, Cell to)
        {
            Figure = figure;
            From = from;
            To = to;
        }
        public readonly Figure Figure;
        public readonly Cell From;
        public readonly Cell To;
    }
}
