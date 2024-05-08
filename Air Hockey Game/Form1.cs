using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Air_Hockey_Game
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(50, 250, 90, 90);
        Rectangle player2 = new Rectangle(940, 250, 90, 90);
        Rectangle ball = new Rectangle(530, 280, 40, 40);
        Rectangle gameArea = new Rectangle(40, 40, 1000, 530);
        Rectangle goal1 = new Rectangle(39, 175, 2, 250);
        Rectangle goal2 = new Rectangle(1039, 175, 2, 250);

        int player1Score = 0;
        int player2Score = 0;

        int playerXSpeed = 5;
        int playerYSpeed = 5;
        int ballXSpeed = 0;
        int ballYSpeed = 0;
        int ballTimer = 0;
        int touches = 0;

        bool wPressed = false;
        bool aPressed = false;
        bool sPressed = false;
        bool dPressed = false;
        bool upPressed = false;
        bool leftPressed = false;
        bool downPressed = false;
        bool rightPressed = false;

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush blueBrush = new SolidBrush(Color.Blue);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        Pen redPen = new Pen(Color.Red, 15);
        Pen bluePen = new Pen(Color.Blue, 15);
        Pen whitePen = new Pen(Color.White, 2);
        Pen blackPen = new Pen(Color.Black, 5);
        Pen smallRedPen = new Pen(Color.Red, 5);

        SoundPlayer Applause = new SoundPlayer(Properties.Resources.Applause);
        SoundPlayer Ping = new SoundPlayer(Properties.Resources.Ping);
        public Form1()
        {
            InitializeComponent();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            MoveBall();

            TopOrBottom();

            IsItAGoal();

            MovePlayers();

            Interactions();

            WinCheck();

            SlowBall();

            Refresh();
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(smallRedPen, 5, 175, 75, 250);
            e.Graphics.DrawEllipse(smallRedPen, 1000, 175, 75, 250);
            e.Graphics.FillRectangle(whiteBrush, gameArea);
            e.Graphics.DrawLine(bluePen, 325, 40, 325, 570);
            e.Graphics.DrawLine(bluePen, 775, 40, 775, 570);
            e.Graphics.DrawRectangle(blackPen, gameArea);
            e.Graphics.DrawLine(redPen, 550, 40, 550, 570);
            e.Graphics.DrawLine(smallRedPen, 40, 175, 40, 425);
            e.Graphics.DrawLine(smallRedPen, 1040, 175, 1040, 425);
            e.Graphics.DrawEllipse(redPen, 430, 180, 240, 240);
            e.Graphics.FillEllipse(redBrush, player1);
            e.Graphics.DrawEllipse(blackPen, player1);
            e.Graphics.FillEllipse(blueBrush, player2);
            e.Graphics.DrawEllipse(blackPen, player2);
            e.Graphics.FillEllipse(blackBrush, ball);
        }

        private void Form1_KeyUp_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Up: 
                    upPressed = false;
                    break;
                case Keys.Down: 
                    downPressed = false;
                    break;
                case Keys.Left: 
                    leftPressed = false;
                    break;
                case Keys.Right: 
                    rightPressed = false;
                    break;
            }
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Up: 
                    upPressed = true;
                    break;
                case Keys.Down: 
                    downPressed = true;
                    break;
                case Keys.Left: 
                    leftPressed = true;
                    break;
                case Keys.Right: 
                    rightPressed = true;
                    break;
            }
        }

        public void MovePlayers()
        {  //move player 1
            if (wPressed == true && player1.Y > 40)
            {
                player1.Y = player1.Y - playerYSpeed;
            }
            if (sPressed == true && player1.Y < 480)
            {
                player1.Y = player1.Y + playerYSpeed;
            }
            if (aPressed == true && player1.X > 40)
            {
                player1.X = player1.X - playerXSpeed;
            }
            if (dPressed == true && player1.X < 460)
            {
                player1.X = player1.X + playerXSpeed;
            }

            //move player 2 
            if (upPressed == true && player2.Y > 40)
            {
                player2.Y = player2.Y - playerYSpeed;
            }
            if (downPressed == true && player2.Y < 480)
            {
                player2.Y = player2.Y + playerYSpeed;
            }
            if (leftPressed == true && player2.X > 550)
            {
                player2.X = player2.X - playerXSpeed;
            }
            if (rightPressed == true && player2.X < 950)
            {
                player2.X = player2.X + playerXSpeed;
            }
        }

        public void IsItAGoal()
        {
            //check to see if ball hit walls and to see if it went in the net
            if (ball.X <= 40 && (ball.Y < 175 || ball.Y > 425))
            {
                ballXSpeed = -ballXSpeed;
                ball.X = ball.X + 5;
            }
            else if (ball.X <= 40 && (ball.Y > 175 && ball.Y < 425))
            {
                player2Score++;
                p2ScoreLabel.Text = $"P2 : {player2Score}";
                scoreLabel.Visible = true;
                scoreLabel.ForeColor = Color.Black;
                Applause.Play();

                Refresh();
                Thread.Sleep(500);

                scoreLabel.Visible = false;
                ball.X = 530;
                ball.Y = 280;
                player1.X = 50;
                player1.Y = 250;
                player2.X = 940;
                player2.Y = 250;
                ballYSpeed = 0;
                touches = 0;
                ballTimer = 0;
            }
            else if (ball.X >= 1000 && (ball.Y < 175 || ball.Y > 425))
            {
                ballXSpeed = -ballXSpeed;
                ball.X = ball.X - 5;
            }
            else if (ball.X >= 1000 && ball.Y > 175 && ball.Y < 425)
            {
                player1Score++;
                p1ScoreLabel.Text = $"P1 : {player1Score}";
                scoreLabel.Visible = true;
                scoreLabel.ForeColor = Color.Black;
                Applause.Play();

                Refresh();
                Thread.Sleep(500);

                scoreLabel.Visible = false;
                ball.X = 530;
                ball.Y = 280;
                player1.X = 50;
                player1.Y = 250;
                player2.X = 940;
                player2.Y = 250;
                ballYSpeed = 0;
                touches = 0;
                ballTimer = 0;
            }
        }

        public void Interactions()
        {
            //check if ball hit player 1
            if (player1.IntersectsWith(ball) && player1.Y > ball.Y)
            {
                ballYSpeed = -10;
                ballTimer = 180;
                touches++;
            }
            else if (player1.IntersectsWith(ball) && player1.Y + 45 < ball.Y)
            {
                ballYSpeed = +10;
                ballTimer = 180;
                touches++;
            }
            if (player1.IntersectsWith(ball) && player1.X > ball.X)
            {
                ballXSpeed = -10;
                ballTimer = 180;
                touches++;
            }
            else if (player1.IntersectsWith(ball) && player1.X + 45 < ball.X)
            {
                ballXSpeed = +10;
                ballTimer = 180;
                touches++;
            }
            //check if ball hit player 2
            if (player2.IntersectsWith(ball) && player2.Y > ball.Y)
            {
                ballYSpeed = -10;
                ballTimer = 180;
                touches++;
            }
            else if (player2.IntersectsWith(ball) && player2.Y + 45 < ball.Y)
            {
                ballYSpeed = 10;
                ballTimer = 180;
                touches++;
            }
            if (player2.IntersectsWith(ball) && player2.X > ball.X)
            {
                ballXSpeed = -10;
                ballTimer = 180;
                touches++;
            }
            else if (player2.IntersectsWith(ball) && player2.X + 45 < ball.X)
            {
                ballXSpeed = 10;
                ballTimer = 180;
                touches++;
            }
        }

        public void WinCheck()
        {
            //check for a winner
            if (player1Score == 3)
            {
                scoreLabel.Visible = true;
                scoreLabel.Text = "P1 WINS";
                resetButton.Visible = true;
                resetButton.Enabled = true;
                gameTimer.Stop();
            }
            else if (player2Score == 3)
            {
                scoreLabel.Visible = true;
                scoreLabel.Text = "P2 WINS";
                resetButton.Visible = true;
                resetButton.Enabled = true;
                gameTimer.Stop();
            }
        }

        public void TopOrBottom()
        {
            //check to see if ball hit top or bottom
            if (ball.Y <= 40)
            {
                ballYSpeed = -ballYSpeed;
                ball.Y = ball.Y + 5;
            }
            else if (ball.Y >= 530)
            {
                ballYSpeed = -ballYSpeed;
                ball.Y = ball.Y - 5;
            }
        }

        public void MoveBall()
        {
            //move ball up and down
            ball.X = ball.X + ballXSpeed;
            ball.Y = ball.Y + ballYSpeed;
        }

        public void SlowBall()
        {
            if (ballTimer < 150)
            {
                if (ballXSpeed > 0)
                {
                    ballXSpeed = 7;
                }
                else if (ballXSpeed < 0)
                {
                    ballXSpeed = -7;
                }
                else
                {
                    ballXSpeed = 0;
                }
                if (ballYSpeed > 0)
                {
                    ballYSpeed = 7;
                }
                else if (ballYSpeed < 0)
                {
                    ballYSpeed = -7;
                }
                else
                {
                    ballYSpeed = 0;
                } 
            }
            if (ballTimer < 120)
            {
                if (ballXSpeed > 0)
                {
                    ballXSpeed = 6;
                }
                else if (ballXSpeed < 0)
                {
                    ballXSpeed = -6;
                }
                else
                {
                    ballXSpeed = 0;
                }
                if (ballYSpeed > 0)
                {
                    ballYSpeed = 6;
                }
                else if (ballYSpeed < 0)
                {
                    ballYSpeed = -6;
                }
                else
                {
                    ballYSpeed = 0;
                }
            }
            if (ballTimer < 90)
            {
                if (ballXSpeed > 0)
                {
                    ballXSpeed = 4;
                }
                else if (ballXSpeed < 0)
                {
                    ballXSpeed = -4;
                }
                else
                {
                    ballXSpeed = 0;
                }
                if (ballYSpeed > 0)
                {
                    ballYSpeed = 4;
                }
                else if (ballYSpeed < 0)
                {
                    ballYSpeed = -4;
                }
                else
                {
                    ballYSpeed = 0;
                }
            }
            if (ballTimer < 60)
            {
                if (ballXSpeed > 0)
                {
                    ballXSpeed = 2;
                }
                else if (ballXSpeed < 0)
                {
                    ballXSpeed = -2;
                }
                else
                {
                    ballXSpeed = 0;
                }
                if (ballYSpeed > 0)
                {
                    ballYSpeed = 2;
                }
                else if (ballYSpeed < 0)
                {
                    ballYSpeed = -2;
                }
                else
                {
                    ballYSpeed = 0;
                }
            }
            if (ballTimer <= 30)
            {
               if (ballXSpeed >= 0)
                {
                    ballXSpeed = 0;
                }
               else
                {
                    ballXSpeed = 0;
                }
                if (ballYSpeed >= 0)
                {
                    ballYSpeed = 0;
                }
                else
                {
                    ballYSpeed = 0;
                }
            }

            if (touches >= 1)
            {
                ballTimer--; 
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            scoreLabel.Visible = false;
            resetButton.Visible = false;
            resetButton.Enabled = false;
            p1ScoreLabel.Text = "P1 : 0";
            p2ScoreLabel.Text = "P2 : 0";
            player1Score = 0;
            player2Score = 0;
            ballXSpeed = 0;
            ballYSpeed = 0;
            ballTimer = 0;
            touches = 0;
            gameTimer.Start();
        }
    }

}
