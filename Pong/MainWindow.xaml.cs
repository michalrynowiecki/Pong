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
        float horizontalBallSpeed = 8.0f;
        float verticalBallSpeed = 2.0f;


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
        private async void GameTimerEvent(object sender, EventArgs e)
        {
            //Player Movement
            if (goDown==true && Canvas.GetBottom(playerBox) > 5)
            {
                Canvas.SetBottom(playerBox, Canvas.GetBottom(playerBox) - playerSpeed);
            }

            
            if (goUp == true && Canvas.GetBottom(playerBox) < 340)
            {
                Canvas.SetBottom(playerBox, Canvas.GetBottom(playerBox) + playerSpeed);
            }

            //Enemy movement
            if (Canvas.GetBottom(enemyBox) > Canvas.GetBottom(playerBox) && Canvas.GetBottom(enemyBox) > 5)
            {
                Canvas.SetBottom(enemyBox, Canvas.GetBottom(playerBox) - speed);
            }

            if (Canvas.GetBottom(enemyBox) < Canvas.GetBottom(playerBox) && Canvas.GetBottom(enemyBox) < 340)
            {
                Canvas.SetBottom(enemyBox, Canvas.GetBottom(playerBox) + speed);
            }

            Random ranGen = new Random();

            //Ball movement
            Canvas.SetLeft(ball, Canvas.GetLeft(ball) + horizontalBallSpeed);     // This is the ball's horizontal speed
            Canvas.SetBottom(ball, Canvas.GetBottom(ball) + verticalBallSpeed);  //This is the ball's vertical speed

            //Change direction of the ball if it hits the box
            if (Canvas.GetLeft(ball) < 40 && Canvas.GetBottom(ball) > Canvas.GetBottom(playerBox) && Canvas.GetBottom(ball) < Canvas.GetBottom(playerBox) + 75)
            {
                //Reverse direction of the ball
                horizontalBallSpeed *= -1;
                verticalBallSpeed *= -1.2f;

                //Player score                
                playerScore.Content = (int.Parse((string)playerScore.Content) + 1).ToString();
            }

            if ((Canvas.GetLeft(ball) > 735 && Canvas.GetLeft(ball) < 740) && Canvas.GetBottom(ball) > Canvas.GetBottom(enemyBox) && Canvas.GetBottom(ball) < Canvas.GetBottom(enemyBox) + 75)
            {
                //Reverse direction of the ball
                horizontalBallSpeed *= -1;
                verticalBallSpeed *= -1.2f;

                //Enemy score
                enemyScore.Content = (int.Parse((string)enemyScore.Content) + 1).ToString();
            }

            if(Canvas.GetBottom(ball) < 10 || Canvas.GetBottom(ball) > 410)
            {
                verticalBallSpeed *= -1;
            }

            //Respawn ball in the middle when it goes out of the field
            if(Canvas.GetLeft(ball) < 10 || Canvas.GetLeft(ball) > 780)
            {
                await Task.Delay(20);
                Canvas.SetLeft(ball, 395);
                Canvas.SetBottom(ball, 220);

                horizontalBallSpeed = ranGen.Next(-2, 2) * 5.2f;
                verticalBallSpeed = ranGen.Next(-2, 2) * 1.2f;
            }
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