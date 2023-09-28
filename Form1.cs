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
        // Variables pour le jeu
        bool goLeft, goRight, jumping, isGameOver;
        bool canjump = true;
        int jumpSpeed;
        int force;
        int score = 0;
        int verticalSpeed = 3;
        int enemyOneSpeed = 2;
        int enemyTwoSpeed = 3;
        readonly int playerSpeed = 7;

        public Form1()
        {
            InitializeComponent();
        }

        // Fonction principale du jeu (méthode appelée à chaque tick du timer)
        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            // Mise à jour du score affiché
            txtScore.Text = "Score : " + score;

            // Déplacement vertical du joueur en sautant
            player.Top += jumpSpeed;

            // Gestion des mouvements du joueur
            if (goLeft == true) { player.Left -= playerSpeed; }
            if (goRight == true) { player.Left += playerSpeed; }
            if (jumping == true && force < 0) { jumping = false; }
            if (jumping == true)
            {
                jumpSpeed = -10;
                force -= 1;
            }
            else
            {
                jumpSpeed = 14;
            }

            // Gestion des collisions avec les plates-formes
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        // Si le joueur touche une plate-forme
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;

                            // Le joueur rebondit sur la plate-forme
                            if (player.Bottom > x.Top && player.Top < x.Top)
                            {
                                player.Top = x.Top - player.Height;
                                jumping = false;
                                canjump = true;
                            }

                            // Gestion de la direction du rebond
                            if (jumping == false) { force = 8; }
                            else { force = -8; }

                            // Empêche le joueur de passer à travers une plate-forme horizontale
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

            // Gestion des pièces (coins)
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin")
                    {
                        // Si le joueur collecte une pièce visible
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                }
            }

            // Gestion des collisions avec les ennemis
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "enemy")
                    {
                        // Si le joueur touche un ennemi
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score : " + score + Environment.NewLine + "Tu as été tué par un ennemi, appuie sur Entrée pour recommencer.";
                        }
                    }
                }
            }

            // Mouvement vertical de la plate-forme
            verticalPlatform.Top -= verticalSpeed;
            if (verticalPlatform.Top < 86 || verticalPlatform.Top > 445)
            {
                verticalSpeed = -verticalSpeed;
            }

            // Mouvement des ennemis horizontaux
            enemyOne.Left -= enemyOneSpeed;
            if (enemyOne.Left < pictureBox5.Left || enemyOne.Left + enemyOne.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left -= enemyTwoSpeed;
            if (enemyTwo.Left < pictureBox7.Left || enemyTwo.Left + enemyTwo.Width > pictureBox7.Left + pictureBox7.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            // Défaite si le joueur sort de la fenêtre
            if (player.Top > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score : " + score + Environment.NewLine + "Tu es tombé dans le vide, appuie sur Entrée pour recommencer.";
            }

            // Condition de victoire
            if (player.Bounds.IntersectsWith(door.Bounds) && score == 15)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Score : " + score + Environment.NewLine + "Vous avez gagné, appuie sur Entrée pour recommencer.";
            }
            else if (player.Bounds.IntersectsWith(door.Bounds) && score != 15)
            {
                txtScore.Text = "Score : " + score + Environment.NewLine + "Collecte toutes les pièces";
            }
        }

        // Gestion des touches enfoncées
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { goLeft = true; }
            if (e.KeyCode == Keys.Right) { goRight = true; }
            if (e.KeyCode == Keys.Space && canjump)
            {
                jumping = true;
                canjump = false;
                jumpSpeed = -8;
            }
        }

        // Gestion des touches relâchées
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) { goLeft = false; }
            if (e.KeyCode == Keys.Right) { goRight = false; }
            if (jumping == true) { jumping = false; }

            // Redémarrage du jeu en appuyant sur Entrée lorsque c'est game over
            if (e.KeyCode == Keys.Enter && isGameOver == true) { RestartGame(); }
        }

        // Fonction de redémarrage du jeu
        private void RestartGame()
        {
            // Réinitialisation des paramètres du jeu
            jumping = false;
            goLeft = false;
            goRight = false;
            isGameOver = false;
            score = 0;
            txtScore.Text = "Score : " + score;

            // Réaffichage des pièces
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }

            // Réinitialisation des positions
            player.Left = 83;
            player.Top = 490;
            enemyOne.Left = 112;
            enemyTwo.Left = 287;
            verticalPlatform.Top = 385;

            // Redémarrage du timer
            gameTimer.Start();
        }
    }
}
