namespace snake.Model
{
    internal class Point
    {
        private double pointX;
        private double pointY;
        private double rad;

        public Point(double pointX, double pointY, double rad )
        {
            this.pointX = pointX;
            this.pointY = pointY;
            this.rad = rad;
            
        }

        public double PointX { get => pointX;  }
        public double PointY { get => pointY;  }
        public double Rad { get => rad;  }
    }
}