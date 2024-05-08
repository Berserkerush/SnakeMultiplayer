using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using snake.ViewModel;
namespace snake.Model
{
    class Snake:ViewModelBase
    {
        private ObservableCollection<SnakeBody> body;

        private Point Head;
        private string direction;
        private double ancho = 10;
        private int snakeId;
        public Snake(int snakeId, string direction)
        {
            this.SnakeId = snakeId;
            this.body = new ObservableCollection<SnakeBody>();
            addBody(1);
            this.Direction = direction;
        }

        public ObservableCollection<SnakeBody> Body { get => body; set { body = value; OnPropertyChanged(nameof(Body));  } }
        public Point Head1 { get => Head; set => Head = value; }
        public string Direction { get => direction; set => direction = value; }
        public int SnakeId { get => snakeId; set => snakeId = value; }

        internal Point newPoint(Point lastPoint = null)
        {
            Point NextPoint;
            double posX = 250;
            double posY = 250;

            if (lastPoint != null)
            {

                switch (direction)
                {
                    case "Up":
                        if (lastPoint.PointY > 0 /*&& lastPoint.PointY < 500*/)
                        { 
                            posX = lastPoint.PointX;
                            posY = lastPoint.PointY - 11;
                        }
                        else { posX = 0; posY = 0; }
                        break;
                    case "Down":
                        if (lastPoint.PointY < 490 /*&& lastPoint.PointY > 0*/)
                        {
                            posX = lastPoint.PointX;
                            posY = lastPoint.PointY + 11;
                        }
                        else { posX = 0; posY = 0; }

                        break;
                    case "Left":
                        if (/*lastPoint.PointX < 500 &&*/ lastPoint.PointX > 0)
                        {
                            posX = lastPoint.PointX - 11;
                            posY = lastPoint.PointY;
                        }
                        else { posX = 0; posY = 0; }
                        break;
                    case "Right":
                        if (/*lastPoint.PointX > 0 &&*/ lastPoint.PointX < 490)
                        {
                            posX = lastPoint.PointX + 11;
                            posY = lastPoint.PointY;
                        }
                        else { posX = 0; posY = 0; }
                        break;
                }
                
            }

            NextPoint = new Point(posX, posY, ancho);
            return NextPoint;
        }
        internal bool addBody(double bodyCant)
        {
            double sequence = 0 ;
            if (bodyCant != 1) bodyCant = bodyCant / 2.0;
            bool canAdd = false;
            System.Windows.Shapes.Rectangle Rectangulo;

            for (int i = 1; i <= bodyCant; i++)
            {
                Rectangulo = new System.Windows.Shapes.Rectangle();
                Rectangulo.Width = 10;
                Rectangulo.Height = 10;
                Rectangulo.Stroke = System.Windows.Media.Brushes.Black;
                Rectangulo.Fill = System.Windows.Media.Brushes.SkyBlue;
                Point newHead = newPoint(Head);
                if (newHead.PointX == 0 && newHead.PointY == 0) 
                {
                    canAdd = false;
                    break;
                }
                else { 
                    if (body.Count != 0) sequence = Body.Last().BodySequence + i ;
                    SnakeBody newPart = new SnakeBody(newHead, Rectangulo, "Snake", sequence);
                    body.Add(newPart);
                    Head = newHead;
                    canAdd = true;
                }
            }
    
            return canAdd;
          
        }
        internal void moveTail()
        {
            body.Remove(body.First());

        }
        internal void moveSnake()
        {   


            if (addBody(1)) moveTail();
            else { }


        }
        private bool canChange(string newDirection)
        {
            bool itCan = false;


            switch (newDirection)
            {
                case "Up":
                    if (direction != "Down") itCan = true;
                    break;
                case "Down":
                    if (direction != "Up") itCan = true;
                    break;
                case "Left":
                    if (direction != "Right") itCan = true;
                    break;
                case "Right":
                    if (direction != "Left") itCan = true;
                    break;
            }

            return itCan;

        }
        public void changeDir(string directions)
        {
            if(canChange(directions)) this.Direction = directions;
        }


    }
}
