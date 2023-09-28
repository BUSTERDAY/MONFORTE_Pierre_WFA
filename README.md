# MONFORTE_Pierre_WFA

## Gestion de Projet


##### 20/09/2023

Suite a de nombreux soucis avec JeetBrains Rider, j'ai passer l'après midi à l'installation de Visual Studio Enterprise 2022 pour pouvoir commencer a travailler le lendemain.


##### 21/09/2023

J'ai trouver un tuto pour avoir une base de travail stable. J'ai donc passer la journée a comprendre le tuto, le fonctionnement du code et a le reproduire.


##### 22/09/2023

J'ai fini de comprendre le tuto.


##### 25/09/2023

J'ai du recréer un nouveau dépot Git du à la perte de mon projet à au mauvais paramétrage du .gitignore.

##### 26/09/2023

Je suis revenu à ce que j'avais vendredi dernier.

##### 27/09/2023

J'ai resolu le problème de collision.


## Organisation du code

Dans la public class Form1, on retrouve tout notre code nécessaire au jeu.

En premier nous avons les variable de base du jeu.

Ensuite le public Form1 permet l'initialisation du jeu.

MainGameTimerEvent contient les principales fonctions du jeu.
A l'intérieur on retrouve 3 foreach pour faire fonctionner les collisions, les coins et les ennemis.
On retrouve aussi les fonctions pour les déplacements de la plateforme vertical et des ennemis.
Enfin on retrouve la défaite si on sort de la fenêtre et les conditions de victoire.

On a KeyIsDown qui permet de savoir si une touche est appuyé ou non.
On a aussi KeyIsUp qui permet de savoir si une touche est relaché ou non.

Enfin, on a la fonction RestartGame.


## Comment Lancer le jeu

Il faut ouvrir sur visual code le fichier MONFORTE_Pierre_WFA.sln et appuyer sur la flêche verte pleine.

Les flèches gauche et droite permettent à ce diriger.
La touche espace permet de sauter. Il faut la maintenir pour faire le saut complet.

## Difficultés rencontrées

J'ai perdu mon projet à cause d'un mauvais paramétrage du .gitignore.