using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using snake.ViewModel;
namespace snake.Model
{
    class Estadistica:ViewModelBase
    {
        int vibId = 0;
        int puntos = 0;
        int wins = 0;
        SolidColorBrush statColor = new SolidColorBrush();


     
        public int VibId { get => vibId; set { vibId = value; OnPropertyChanged(nameof(VibId)); } }
        public int Puntos { get => puntos; set { puntos = value; OnPropertyChanged(nameof(Puntos)); } }
        public int Wins { get => wins; set { wins = value; OnPropertyChanged(nameof(Wins)); } }
        public SolidColorBrush StatColor { get => statColor; set => statColor = value; }

        public Estadistica(int vibId)
        {
            this.VibId = vibId;
        }
        public void colorChange()
        {
 
        }
    }
}
