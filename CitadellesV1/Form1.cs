// TEST GIT GIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CitadellesV1
{
    
    public partial class Form1 : Form
    {
        String nom1, nom2,lebouton;
        Form Commencer_partie = new Form();
        int PersonnageQuiJoue = 0;
        bool CommencerPartie_ferme = false;

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //METHODE POUR L'OUVERTURE DE LA FORME POUR INSCRIRE LES NOMS DES JOUEURS 
            Commencer_partie_essai();


            tabPage1.Text = "Personnages";
            tabPage3.Text = "Personnages";
            tabPage2.Text = "Quartiers";
            tabPage4.Text = "Quartiers";
            PiecesJ1.Text += 2;
            PiecesJ2.Text += 2;

            //ANALYSE DES CARTES CHOISIE PAR LES JOUEURS POUR L'APPEL DES DIFFERENTS PERSONNAGES
            if (CommencerPartie_ferme)
            { // test si la form pour choisir les prénoms est fermé, si c'est le cas peut excécuter le code suivant 
                bool PersonnageEstChoisi = false;


                while (!PersonnageEstChoisi)
                {

                    PersonnageQuiJoue++;

                    foreach (Control CartePersonnageChoisie in tabPage1.Controls)
                    {


                        if (PersonnageQuiJoue == Convert.ToInt16(CartePersonnageChoisie.Tag))
                        {
                            String Perso = Nom_personnage(Convert.ToInt16(CartePersonnageChoisie.Tag));


                            PersonnageEstChoisi = true;

                            MessageBox.Show("C'est au tour " + Perso + " de jouer", "A qui le tour ?");
                        }
                    }

                    //voir si déplacer la boucle pour la mettre dans un event pour l'appeler à chaque fois ? Voir quand le personnage n'est pas pioché aussi et de checker dans le tabpage3


                }
                ChoixPendantLeTour();
            }
        }

        //Mis à part comme ça peut appeler à chaque tour
        public void ChoixPendantLeTour() { 

                Form ChoixTour = new Form();
                Button Pouvoir = new Button();
                Button Pioche = new Button();
                Button Pieces = new Button();
                Button Construction = new Button();
                ChoixTour.Controls.Add(Pouvoir);
                ChoixTour.Controls.Add(Pioche);
                ChoixTour.Controls.Add(Pieces);
                ChoixTour.Controls.Add(Construction);
                Pouvoir.Width = 100;
                Pouvoir.Height = 50;
                Pouvoir.Text = "Utiliser votre pouvoir";
                Pioche.Width = 100;
                Pioche.Height = 50;
                Pioche.Text = "Piocher des cartes";
                Pieces.Width = 100;
                Pieces.Height = 50;
                Pieces.Text = "Collecter des pièces";
                Pieces.Tag = "plop";
                Construction.Width = 100;
                Construction.Height = 50;
                Construction.Text = "Construire un quartier";
                Construction.Top = 80;
                Construction.Left = 150;
                Pieces.Top = 80;
                Pioche.Left = 150;
                ChoixTour.StartPosition = FormStartPosition.CenterScreen;
                ChoixTour.TopMost = true;
                ChoixTour.Show();

                Pieces.Click += UneAction;
            }

        //QUAND CHOISI DE PIOCHER DES PIECES ( a modifier )
        private void UneAction(object sender, EventArgs e)
        {
            Button essai = (Button)sender ;
            string plop = Convert.ToString(essai.Tag);
            if (plop == "plop")
           {
                int ajoutpiece = Convert.ToInt16(PiecesJ1.Text);
                ajoutpiece += 2;
                PiecesJ1.Text = Convert.ToString(ajoutpiece);
                essai.Enabled = false ;
                //le problème là est de récupérer le bouton pioche pour le rendre disable aussi 
           }
        }

        private String Nom_personnage(int num_carte)
        {
            String NomPersonnage="";
            switch (num_carte)
            {
                case 1:
                   NomPersonnage = "de l'Assassin";
                    break;
                case 2:
                    NomPersonnage = "du Voleur";
                    break;
                case 3:
                    NomPersonnage = " du Magicien";
                    break;
                case 4:
                    NomPersonnage = " du Roi";
                    break;
                case 5:
                    NomPersonnage = "de l'Evèque";
                    break;
                case 6:
                    NomPersonnage = "du Marchand";
                    break;
                case 7:
                    NomPersonnage = "de l'Architecte";
                    break;
                case 8:
                    NomPersonnage = "du Condottiere";
                    break;
            }
            return NomPersonnage;
        }

        private void Commencer_partie_essai()
        {
            Commencer_partie.StartPosition = FormStartPosition.CenterScreen;
            // CREATION, AJOUT ET DISPOSITON DES DIFFERENTS ELEMENTS SUR LA FORM 'Commencer_partie'
            Label joueur1nom = new Label();
            Label joueur2nom = new Label();
            Button validationbouton = new Button();
            validationbouton.Click += Validation_joueur;
            TextBox nomjoueur1 = new TextBox();
            TextBox nomjoueur2 = new TextBox();
            Commencer_partie.Controls.Add(joueur1nom);
            Commencer_partie.Controls.Add(joueur2nom);
            Commencer_partie.Controls.Add(validationbouton);
            Commencer_partie.Controls.Add(nomjoueur1);
            Commencer_partie.Controls.Add(nomjoueur2);
            joueur1nom.Text = "Veuillez entrer le nom du 1er joueur : ";
            joueur2nom.Text = "Veuillez entrer le nom du 2nd joueur : ";
            validationbouton.Text = "Commencer la partie";
            joueur1nom.Left = 30;
            joueur1nom.Width = 300;
            nomjoueur1.Left = 30;
            nomjoueur1.Top = 30;
            nomjoueur1.Width = 100;
            nomjoueur1.Height = 30;
            joueur2nom.Left = 30;
            joueur2nom.Top = 50;
            joueur2nom.Width = 300;
            nomjoueur2.Left = 30;
            nomjoueur2.Top = 80;
            nomjoueur2.Width = 100;
            nomjoueur2.Height = 30;
            validationbouton.Left = 50;
            validationbouton.Top = 200;
            validationbouton.Width = 200;

            nomjoueur1.LostFocus += Enregistrement_nom_joueur1;
            nomjoueur2.LostFocus += Enregistrement_nom_joueur2;

            Commencer_partie.ShowDialog(this);
        }

            //RECUPERATION DU NOM INSCRIT DANS LA TEXTBOX ET ENREGISTREMENT DANS LA VARIABLE NOM1
        private void Enregistrement_nom_joueur1(object sender, EventArgs e)
        {
            TextBox nom = (TextBox)sender;
            nom1 = nom.Text;
        }

        //RECUPERATION DU NOM INSCRIT DANS LA TEXTBOX ET ENREGISTREMENT DANS LA VARIABLE NOM2
        private void Enregistrement_nom_joueur2(object sender, EventArgs e)
        {
            TextBox nom = (TextBox)sender;
            nom2 = nom.Text;
        }

        //VALIDATION DES NOMS PAR L'UTLISATEUR (ferme la fenêtre et inscrit les noms dans la form principale)
        private void Validation_joueur(object sender, EventArgs e)
        {
            label5.Text = nom1;
            label6.Text = nom2;

            Commencer_partie.Close();

            CommencerPartie_ferme = true;

        }


        private void reglesdujeu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string titre = "les règles du jeu";
            string regles = "on mettra une photo des règles du jeu ou on les écrira";
            MessageBox.Show(regles,titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
