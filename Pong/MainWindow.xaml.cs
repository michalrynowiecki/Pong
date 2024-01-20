using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;

namespace Pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool goUp, goDown;
        int playerSpeed = 4;
        int speed = 1;

        DispatcherTimer gameTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            background.Focus();

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

        }

        //Movement
        private void GameTimerEvent(object sender, EventArgs e)
        {
            //Player Movement
            if (goDown==true && Canvas.GetBottom(playerBox) > 5)
            {
                Canvas.SetBottom(playerBox, Canvas.GetBottom(playerBox) - playerSpeed);
            }

            
            if (goUp == true && Canvas.GetBottom(playerBox) < 350)
            {
                Canvas.SetBottom(playerBox, Canvas.GetBottom(playerBox) + playerSpeed);
            }

            //Enemy movement
            if (Canvas.GetBottom(enemyBox) > Canvas.GetBottom(playerBox) && Canvas.GetBottom(enemyBox) > 5)
            {
                Canvas.SetBottom(enemyBox, Canvas.GetBottom(playerBox) - speed);
            }

                //Canvas.SetBottom(enemyBox, Canvas.GetBottom(playerBox) + speed);
            
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                goDown = true;
            }
            if (e.Key == Key.Up) 
            {
                goUp = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                goDown = false;
            }
            if (e.Key == Key.Up)
            {
                goUp = false;
            }
        }
    }
}