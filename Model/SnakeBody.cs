using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using snake.ViewModel;
namespace snake.Model
{
    class SnakeBody
    {
        private Point position;
        private Rectangle part;
        private string use;
        private double bodySequence;

        public SnakeBody(Point position, Rectangle part, string use, double newSequence)
        {
            this.position = new Point(position.PointX , position.PointY, part.Width);
            this.part = part;
            this.Use = use;
            this.BodySequence = newSequence;
            
        }

        public Rectangle Part { get => part;  }
        public Point Position { get => position; }
        public string Use { get => use; set => use = value; }
        public double BodySequence { get => bodySequence; set => bodySequence = value; }
        
    }
}
