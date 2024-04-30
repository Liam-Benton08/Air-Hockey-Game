using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Air_Hockey_Game
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(175, 40, 60, 60);
        Rectangle player2 = new Rectangle(175, 460, 60, 60);
        Rectangle ball = new Rectangle(190, 250, 30, 30);
        Rectangle gameArea = new Rectangle(30, 30, 350, 500);

        int player1Score = 0;
        int player2Score = 0;

        int playerXSpeed = 3;
        int playerYSpeed = 3;
        int ballXSpeed = 0;
        int ballYSpeed = 0;

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

            //move player 1
            if (wPressed == true && player1.Y > 0)
            {
                player1.Y = player1.Y - playerYSpeed;
            }
            if (sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y = player1.Y + playerYSpeed;
            }
            if (aPressed == true && player1.X > 10)
            {
                player1.X = player1.X - playerXSpeed;
            }
            if (dPressed == true && player1.X < this.Width - player1.Width)
            {
                player1.X = player1.X + playerXSpeed;
            }

            //move player 2 
            if (upPressed == true && player2.Y >= 30)
            {
                player2.Y = player2.Y - playerYSpeed;
            }
            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y = player2.Y + playerYSpeed;
            }
            if (leftPressed == true && player2.X > 10)
            {
                player2.X = player2.X - playerXSpeed;
            }
            if (rightPressed == true && player2.X < this.Width - player2.Width)
            {
                player2.X = player2.X + playerXSpeed;
            }

            //check if ball went in on left goal

            //check if ball went in on right goal

            //check for a winner
            
            
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(pinkBrush, player1);
            e.Graphics.FillEllipse(greenBrush, player2);
            e.Graphics.FillEllipse(whiteBrush, ball);
            e.Graphics.DrawRectangle(whitePen, gameArea);
            e.Graphics.DrawLine(whitePen, 30, 265, 380, 265);
            e.Graphics.DrawLine(whitePen, 205, 30, 205, 530);
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
    }
}
