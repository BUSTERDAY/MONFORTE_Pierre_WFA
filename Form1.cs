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
        bool canjump = true;
        int force;
        int score = 0;
        readonly int playerSpeed = 7;
        
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;


        public Form1()
        {
            InitializeComponent();
        }

        //Fonction principal
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score : " + score;

            player.Top += jumpSpeed;

            if (goLeft == true) { player.Left -= playerSpeed; }
            if (goRight == true) { player.Left += playerSpeed; }
            if (jumping == true && force < 0) { jumping = false; }
            if (jumping == true)
            {
                jumpSpeed = -6;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            //collision du joueur
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {

                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;

                            if (player.Bottom > x.Top && player.Top < x.Top)
                            {
                                player.Top = x.Top - player.Height;
                                jumping = false;
                                canjump = true;
                            }

                            if (jumping == false) { force = 8;}
                            else { force = -8;}

                            if ((string)x.Name == "horizontalPlatform" && goLeft == false)
                            {
                                goRight = false;
                            }

                            if ((string)x.Name == "horizontalPlatform" && goRight == false)
                            {
                                goLeft = false;
                            }
                        }
                    }
                }
            }

            //Fonctionnement des coins
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                }
            }

            // Fonctionnement des ennemis
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
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

            //Mouvement vertical d'une plateforme

            verticalPlatform.Top -= verticalSpeed;

            if (verticalPlatform.Top < 86 || verticalPlatform.Top > 385)
            {
                verticalSpeed = -verticalSpeed;
            }

            //Mouvement des ennemies

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

            //Fonction de défaite lorsqu'on sort de la fenêtre

            if (player.Top > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score : " + score + Environment.NewLine + "Tu es tombé dans le vide, appuis sur Entrée pour recommencer.";
            }

            //Fonction de victoire et conditions
            if (player.Bounds.IntersectsWith(door.Bounds) && score == 18)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score : " + score + Environment.NewLine + "Vous avez gagner, appuis sur Entrée pour recommencer.";
            }
            else if (player.Bounds.IntersectsWith(door.Bounds) && score != 18)
            {
                txtScore.Text = "Score : " + score + Environment.NewLine + "Collecte toutes les pièces";
            }
        }

        //Lorsque les touches sont appuyés
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { goLeft = true; }
            if (e.KeyCode == Keys.Right) { goRight = true; }
            if (e.KeyCode == Keys.Space)
            {

            }
            if (e.KeyCode == Keys.Space && canjump)
            {
                jumping = true;
                canjump = false;
                jumpSpeed = -8;
            }
        }

        //Lorsque les touches sont relacher
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { goLeft = false; }
            if (e.KeyCode == Keys.Right) { goRight = false; }
            if (jumping == true) { jumping = false; }

            if (e.KeyCode == Keys.Enter && isGameOver == true) { RestartGame(); }

        }

        //Fonction de relance du jeu
        private void RestartGame()
        {
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;

            txtScore.Text = "Score : " + score;

            //Reset visibilité des coins lors qu'on recommence la partie

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            //Reset position des plateformes, des ennemies, et du joueur

            player.Left = 83;
            player.Top = 490;

            enemyOne.Left = 126;
            enemyTwo.Left = 307;

            verticalPlatform.Top = 385;

            gameTimer.Start();
        }
    }
}
