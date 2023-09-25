using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MONFORTE_Pierre_WFA
{
    public partial class Form1 : Form
    {
        bool goLeft, goRight, jumping, isGameOver;

        int jumpSpeed;
        int force;
        int score = 0;
        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;


        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score : " + score;

            player.Top += jumpSpeed;

            if (goLeft == true) { player.Left -= playerSpeed; }
            if (goRight == true) { player.Left += playerSpeed; }
            if (jumping == true && force < 0) { jumping = false; }
            if (jumping == true)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {

                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;



                            if ((string)x.Name == "horizontalPlatform" && goLeft == false || (string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }
                        x.BringToFront();
                    }

                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }

                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score : " + score + Environment.NewLine + "Tu as été tuer par un ennemie, appuis sur Entrée pour recommencer.";
                        }
                    }
                }
            }

            //mouvement vertical d'une plateforme

            verticalPlatform.Top -= verticalSpeed;

            if (verticalPlatform.Top < 86 || verticalPlatform.Top > 385)
            {
                verticalSpeed = -verticalSpeed;
            }

            //mouvement des ennemies

            enemyOne.Left -= enemyOneSpeed;

            if (enemyOne.Left < pictureBox5.Left || enemyOne.Left + enemyOne.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left -= enemyTwoSpeed;

            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            //fonction de défaite lorsqu'on sort de la fenêtre

            if (player.Top > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score : " + score + Environment.NewLine + "Tu es tombé dans le vide, appuis sur Entrée pour recommencer.";
            }

            if (player.Bounds.IntersectsWith(door.Bounds) && score == 28)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score : " + score + Environment.NewLine + "Vous avez gagner, appuis sur Entrée pour recommencer.";
            }
            else
            {
                txtScore.Text = "Score : " + score + Environment.NewLine + "Collecte toutes les pièces";
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { goLeft = true; }
            if (e.KeyCode == Keys.Right) { goRight = true; }
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;

            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { goLeft = false; }
            if (e.KeyCode == Keys.Right) { goRight = false; }
            if (jumping == true) { jumping = false; }

            if (e.KeyCode == Keys.Enter && isGameOver == true) { RestartGame(); }

        }
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            txtScore.Text = "Score : " + score;

            // reset visibilité des coins lors qu'on recommence la partie

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            //reset position des plateformes, des ennemies, et du joueur

            player.Left = 83;
            player.Top = 645;

            enemyOne.Left = 126;
            enemyTwo.Left = 357;

            horizontalPlatform.Left = 12;
            verticalPlatform.Top = 503;

            gameTimer.Start();
        }
    }
}
