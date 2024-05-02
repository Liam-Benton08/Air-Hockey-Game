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
        int touches = 1;

        bool wPressed = false;
        bool aPressed = false;
        bool sPressed = false;
        bool dPressed = false;
        bool upPressed = false;
        bool leftPressed = false;
        bool downPressed = false;
        bool rightPressed = false;

        SolidBrush pinkBrush = new SolidBrush(Color.HotPink);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.LimeGreen);
        Pen whitePen = new Pen(Color.White, 2);
        public Form1()
        {
            InitializeComponent();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball up and down
            ball.X = ball.X + ballXSpeed;
            ball.Y = ball.Y + ballYSpeed;
            
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
                scoreLabel.ForeColor = Color.LimeGreen;

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
            }
            else if (ball.X >= 1000 && (ball.Y < 175 || ball.Y > 425))
            {
                ballXSpeed = -ballXSpeed;
                ball.X = ball.X -5;
            }
            else if (ball.X >= 1000 && ball.Y > 175 && ball.Y < 425)
            {
                player1Score++;
                p1ScoreLabel.Text = $"P1 : {player1Score}";
                scoreLabel.Visible = true;
                scoreLabel.ForeColor = Color.HotPink;

                Refresh();
                Thread.Sleep(500);

                scoreLabel.Visible= false;
                ball.X = 530;
                ball.Y = 280;
                player1.X = 50;
                player1.Y = 250;
                player2.X = 940;
                player2.Y = 250;
                ballYSpeed = 0;
                touches = 0;
              
            }
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
            

            //move player 1
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

            //check if ball hit player 1
            if (player1.IntersectsWith(ball) && player1.Y > ball.Y)
            {
                ballYSpeed = -10;
                touches++;
            }
            else if (player1.IntersectsWith(ball) && player1.Y + 45 < ball.Y)
            {
                ballYSpeed = +10;
                touches++;
            }
            
            if (player1.IntersectsWith(ball) && player1.X > ball.X)
            {
                ballXSpeed = -10;
                touches++;
            }
            else if (player1.IntersectsWith(ball) && player1.X + 45 < ball.X)
            {
                ballXSpeed = +10;
                touches++;
            }

            //check if ball hit player 2
            if (player2.IntersectsWith(ball) && player2.Y > ball.Y)
            {
                ballYSpeed = -10;
                touches++;
            }
            else if (player2.IntersectsWith(ball) && player2.Y + 45 < ball.Y)
            {
                ballYSpeed = 10;
                touches++; 
            }
            if (player2.IntersectsWith(ball) && player2.X > ball.X)
            {
                ballXSpeed = -10;
                touches++;
            }
            else if (player2.IntersectsWith(ball) && player2.X + 45 < ball.X)
            {
                ballXSpeed = 10;
                touches++;
            }

            //check for a winner
            if (player1Score == 3)
            {
                scoreLabel.Visible = true;
                scoreLabel.Text = "P1 WINS";
                gameTimer.Stop();
            }
            else if(player2Score == 3)
            {
                scoreLabel.Visible = true;
                scoreLabel.Text = "P2 WINS";
                gameTimer.Stop();
            }

            Refresh();
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(whitePen, gameArea);
            e.Graphics.DrawLine(whitePen, 550, 40, 550, 570);
            e.Graphics.FillRectangle(greenBrush, goal1);
            e.Graphics.FillRectangle(pinkBrush, goal2);
            e.Graphics.DrawEllipse(whitePen, 430, 180, 240, 240);
            e.Graphics.FillEllipse(pinkBrush, player1);
            e.Graphics.FillEllipse(greenBrush, player2);
            e.Graphics.FillEllipse(whiteBrush, ball);
            
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
                case Keys.I: // change this to up
                    upPressed = false;
                    break;
                case Keys.K: // change this to down
                    downPressed = false;
                    break;
                case Keys.J: // change this to left
                    leftPressed = false;
                    break;
                case Keys.L: //change this to right
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
                case Keys.I: //change this to up
                    upPressed = true;
                    break;
                case Keys.K: // change this to down
                    downPressed = true;
                    break;
                case Keys.J: // change this to left
                    leftPressed = true;
                    break;
                case Keys.L: //change this to right
                    rightPressed = true;
                    break;

            }
        }
    }
}
