using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace BattleShip
{
    public partial class Form1 : Form
    {
        List<Button> playerPositions;
        List<Button> enemyPositions;

        Random rand = new Random();

        int totalShips = 3;
        int round = 10;
        int playerScore;
        int enemyScore;
        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

      

        private void EnemyPlayTimerEvent(object sender, EventArgs e)
        {
            if (playerPositions.Count > 0 && round > 0)
            {
                --round;
                txtRounds.Text = "Round: " + round;
                int index = rand.Next(playerPositions.Count);
                if ((string)playerPositions[index].Tag == "playerShip")
                {
                    playerPositions[index].Enabled = false;
                    txtMove.Text = playerPositions[index].Text;
                    playerPositions[index].BackgroundImage = Properties.Resources.fireIcon;
                    playerPositions[index].BackColor = Color.DarkBlue;
                    playerPositions.RemoveAt(index);
                    ++enemyScore;
                    txtEnemy.Text = enemyScore.ToString();
                    EnemyPlayTimer.Stop();
                }
                else
                {
                    txtMove.Text = playerPositions[index].Text;
                    playerPositions[index].BackgroundImage = Properties.Resources.missIcon;
                    playerPositions[index].BackColor = Color.DarkBlue;
                    playerPositions.RemoveAt(index);
                    EnemyPlayTimer.Stop();
                }
            }
            
            if(round < 1)
            {
                if (playerScore > enemyScore)
                {
                    MessageBox.Show("You won!");
                    RestartGame();
                }
                else if (enemyScore > playerScore)
                {
                    MessageBox.Show("You lost!");
                    RestartGame();
                }
                else
                {
                    MessageBox.Show("It's a draw!");
                    RestartGame();
                }
            }
        }

        private void AttackBtnEvent(object sender, EventArgs e)
        {
            if (EnemyLocationBox.Text != "")
            {
                var attackPosition = EnemyLocationBox.Text.ToLower();
                int index = enemyPositions.FindIndex(position => position.Name == attackPosition);
                if(enemyPositions[index].Enabled && round > 0)
                {
                    --round;
                    txtRounds.Text = "Round: " + round;
                    if((string)enemyPositions[index].Tag == "enemyShip")
                    {
                        enemyPositions[index].Enabled = false;
                        enemyPositions[index].BackgroundImage = Properties.Resources.fireIcon;
                        enemyPositions[index].BackColor = Color.DarkBlue;
                        ++playerScore;
                        txtPlayer.Text = playerScore.ToString();
                        EnemyPlayTimer.Start();
                    }
                    else
                    {
                        enemyPositions[index].Enabled = false;
                        enemyPositions[index].BackgroundImage = Properties.Resources.missIcon;
                        enemyPositions[index].BackColor = Color.DarkBlue;
                        EnemyPlayTimer.Start();
                    }

                }
            }
            else
            {
                MessageBox.Show("Choose a location from the drop down first!");
            }
        }

        private void PlayerPositionButtonEvent(object sender, EventArgs e)
        {
            if(totalShips > 0)
            {
                var button = (Button)sender;
                button.Enabled = false;
                button.Tag = "playerShip";
                button.BackColor = Color.Orange;
                --totalShips;
            }
            if (totalShips == 0)
            {
                BtnAttack.Enabled = true;
                BtnAttack.BackColor = Color.Red;
                BtnAttack.ForeColor = Color.White;
                txtHelp.Text = "2) Now pick the attack position from the drop down";
            }


        }
        
        private void RestartGame()
        {
            playerPositions = new List<Button> { w1, w2, w3, w4, x1, x2, x3, x4, y1, y2, y3, y4, z1, z2, z3, z4 };
            enemyPositions = new List<Button> { a1, a2, a3, a4, b1, b2, b3, b4, c1, c2, c3, c4, d1, d2, d3, d4 };
            EnemyLocationBox.Items.Clear();
            EnemyLocationBox.Text = null;
            txtHelp.Text = "1) Select the fields for your ships";

            for(int index=0; index < enemyPositions.Count; ++index)
            {
                enemyPositions[index].Enabled = true;
                enemyPositions[index].Tag = null;
                enemyPositions[index].BackColor = Color.White;
                enemyPositions[index].BackgroundImage = null;
                EnemyLocationBox.Items.Add(enemyPositions[index].Text);
            }

            for (int index = 0; index < playerPositions.Count; ++index)
            {
                playerPositions[index].Enabled = true;
                playerPositions[index].Tag = null;
                playerPositions[index].BackColor = Color.White;
                playerPositions[index].BackgroundImage = null;
               
            }
            playerScore = 0;
            enemyScore = 0;
            round = 10;
            totalShips = 3;

            txtPlayer.Text = playerScore.ToString();
            txtEnemy.Text = enemyScore.ToString();
            txtMove.Text = "";

            BtnAttack.Enabled = false;
            BtnAttack.BackColor = Color.White;
            enemyLocationPicker();
        }
        private void enemyLocationPicker()
        {
            for(int index=0; index<3; ++index)
            {
                int pos = rand.Next(enemyPositions.Count);
                if (enemyPositions[pos].Enabled && (string)enemyPositions[pos].Tag == null)
                {
                    enemyPositions[pos].Tag = "enemyShip";
                    Debug.WriteLine("Enemy Position: " + enemyPositions[index].Text);
                }
                else
                {
                    pos = rand.Next(enemyPositions.Count);
                }
                
            }
        }

        
    }
}
