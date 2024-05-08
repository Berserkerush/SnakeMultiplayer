
using System;
using System.Windows.Threading;
using snake.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

using System.Collections.ObjectModel;
using System.Linq;

namespace snake.ViewModel
{
    class SnakeViewModel : ViewModelBase
    {

        private ObservableCollection<Snake> playerSnakes;
        private ObservableCollection<Estadistica> playerStat;
        private DispatcherTimer timer;
        private ObservableCollection<SnakeBody> food;
        public SnakeViewModel()
        {
            PlayerSnakes = new ObservableCollection<Snake>();
            KeyPressCommand = new RelayCommand<string>(HandleKeyPress);
            KeyPressCommand2 = new RelayCommand<string>(HandleKeyPress2);
            AddSnakeCommand = new RelayCommand(CreateSnake);
            Food = new ObservableCollection<SnakeBody>();
            PlayerStat = new ObservableCollection<Estadistica>();
            moveStart();

        }

        public ObservableCollection<Snake> PlayerSnakes { get => playerSnakes; private set { playerSnakes = value;} }
        public RelayCommand<string> KeyPressCommand { get; private set; }
        public RelayCommand<string> KeyPressCommand2 { get; private set; }
        public ICommand AddSnakeCommand { get; private set; }
        public ObservableCollection<SnakeBody> Food { get => food; set { food = value; OnPropertyChanged("Food"); } }

        public ObservableCollection<Estadistica> PlayerStat { get => playerStat; set { playerStat = value; OnPropertyChanged(nameof(PlayerStat)); } }

        private void CreateSnake()
        {
            if (PlayerSnakes.Count < 2)
            {
                PlayerSnakes.Add( new Snake(PlayerSnakes.Count, "Left"));
                PlayerStat.Add(new Estadistica(PlayerSnakes.Count));
                
            }
        }
        private void addFood()
        {
            Random randnum = new Random();
            int rando;
            rando = randnum.Next(1, 20);
            int radio = randnum.Next(5, 10);

            System.Windows.Shapes.Rectangle foodbody = new System.Windows.Shapes.Rectangle();
            foodbody.Width = radio;
            foodbody.Height = radio;
            foodbody.Stroke = System.Windows.Media.Brushes.Black;
            foodbody.Fill = System.Windows.Media.Brushes.OrangeRed;
            Point posFood = new Point(randnum.Next(1, 490), randnum.Next(1, 490), radio);
            if (rando == 5)
            {
                Food.Add(new SnakeBody(posFood, foodbody, "Food", 1));
                OnPropertyChanged("Food");
            }
        }

        private void moveStart()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(50)
            };

            // Manejar el evento Tick del temporizador
            timer.Tick += Timer_Tick;

            // Iniciar el temporizador
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            bool comio = false;
            for (int i = 0; i < PlayerSnakes.Count; i++)
            {
                PlayerSnakes[i].moveSnake();
                addFood();
                /*Col with Food*/
                SnakeBody colPart = CheckCol(Food, PlayerSnakes[i].Head1);

                if (colPart != null)
                {
                    PlayerSnakes[i].addBody(colPart.Part.Width);
                    Food.Remove(colPart);
                    playerStat[i].Puntos = playerStat[i].Puntos + (int)colPart.Part.Width;
            

                }
                /*Col with himself*/
                colPart = null;
                colPart = CheckCol(PlayerSnakes[i].Body, PlayerSnakes[i].Head1);
                if ((colPart != null) && (colPart.BodySequence != PlayerSnakes[i].Body.Last().BodySequence))
                {

                    PlayerSnakes[i] = new Snake(PlayerSnakes[i].SnakeId,"Up");
                    playerStat[i].Puntos = 0;
             
                }

                /*Col with other snakes*/
                for (int j = 0; j < PlayerSnakes.Count; j++)
                {
                    comio = false;
                    colPart = null;
                    colPart = CheckCol(PlayerSnakes[j].Body, PlayerSnakes[i].Head1);

                    if ((colPart != null) && (colPart.BodySequence != PlayerSnakes[i].Body.Last().BodySequence))
                    {
                        
                        PlayerStat[playerSnakes[j].SnakeId].Wins++;
                        playerStat[i].Puntos = 0;
     
                        foreach (SnakeBody newFood in PlayerSnakes[i].Body)
                        {  
                            Food.Add(newFood);
                            comio = true;
                        }
                    }
                    if (comio == true) PlayerSnakes[i] = new Snake(PlayerSnakes[i].SnakeId,"Up");
                }
             
            }
        }
        private SnakeBody CheckCol(ObservableCollection<SnakeBody> partCollection, Point head)
        {
            SnakeBody returnColisionPoint = null;
            foreach (SnakeBody CollPoint in partCollection)
            { 
                if (pointDis(head, CollPoint.Position) == true)
                {
                    
                    returnColisionPoint = CollPoint;
                    break;
                }
            }
            /**/
            return returnColisionPoint;

        }
        private bool pointDis(Point point1 , Point point2)
        {
            bool touchedPoint = false;

            double distanciaX; 
            double distanciaY;
            double disRadial;

            /* este if es para no revisar por que es el mismo punto.*/
            
            distanciaX = Math.Abs(point2.PointX - point1.PointX);
            distanciaY = Math.Abs(point2.PointY - point1.PointY);
            /*if (!(distanciaX == distanciaY && distanciaY == 0)) 
            {*/
                
                disRadial = ((point1.Rad + point2.Rad) / 2) ;


                if (distanciaX < disRadial && distanciaY < disRadial)
                {
                    touchedPoint = true;
                }
          //  }
               



            return touchedPoint;
        }
        private void addBody(Snake snakes)
        {
            snakes.addBody(1);


        }
       private void HandleKeyPress(string key)
        {
            PlayerSnakes.First().changeDir(key);
        }
        private void HandleKeyPress2(string key)
        {
            PlayerSnakes.Last().changeDir(key);
        }

    }
}
