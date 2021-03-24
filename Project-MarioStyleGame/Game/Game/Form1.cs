
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Game
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, hasKey;
        int jumpSpeed = 10;
        int force = 8;
        int score = 0;

        int playerSpeed = 10;
        int backgroundSpeed = 8;
        public Form1()
        {
            InitializeComponent();
        }

      

       
       

        private void MainTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            player.Top += jumpSpeed;
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left + player.Width < this.ClientSize.Width)
            {
                player.Left += playerSpeed;
            }

            if (goLeft == true && background.Left < 0)
            {
                background.Left += backgroundSpeed;
                MoveGameElements("forward");
            }
            if (goRight == true && background.Left > -172)
            {
                background.Left -= backgroundSpeed;
                MoveGameElements("back");
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && jumping == false)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                        jumpSpeed = 0;

                    }

                    x.BringToFront();
                }

                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if(player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score++;
                    }
                }

                if(player.Bounds.IntersectsWith(key.Bounds))
                {
                    hasKey = true;
                    key.Visible = false;
                }
                if (player.Bounds.IntersectsWith(door.Bounds) && hasKey==true)
                {
                    door.Image = Properties.Resources.door_open;
                    GameTimer.Stop();
                    MessageBox.Show("Well done, you passed first level" + Environment.NewLine + "Click continue for next level");
                    RestartGame();
                }

                if(player.Top + player.Height > this.ClientSize.Height)
                {
                    GameTimer.Stop();
                    MessageBox.Show("You Died" + Environment.NewLine + "Click ok to play again");
                    RestartGame();
                }
            }
             
            
        }

        private void KeyisDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;

            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                jumping = true;
            }
        }

        

   

        private void KeyisUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                jumping = false;
            }
        }

        

        private void CloseGame(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RestartGame()
        {
            Form1 newWindow = new Form1();
            newWindow.Show();
            this.Hide();
        }

        private void MoveGameElements(string direction)
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "platform" || 
                    x is PictureBox && (string)x.Tag =="coin" ||
                    x is PictureBox && (string)x.Tag == "door" ||
                    x is PictureBox && (string)x.Tag == "key")
                {
                    if(direction == "back")
                    {
                        x.Left -= backgroundSpeed;
                    }
                    else if(direction == "forward")
                    {
                        x.Left += backgroundSpeed;
                    }
                }
            }
        }
    }
}
