using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace SnakeGame
{
    public class Snake
    {
        public List<Ellipse> Ellipses { get; set; }
        public Direction Direction { get; set; }
        public int Speed{ get; set; }
    }
}
