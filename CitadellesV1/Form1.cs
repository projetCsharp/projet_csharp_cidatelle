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
        String nom1, nom2;
        Form Commencer_partie = new Form();
        Form Choix_perso = new Form();
        int PersonnageQuiJoue = 0, nbtours=7;
        bool CommencerPartie_ferme = false;
        bool Choixpersonnage_ferme = false;
        //Creation de la form pour les choix des joueurs à chaque tour
        Form ChoixTour = new Form();

        int rang_bouton_pioche;
        int rang_bouton_piece;

        //variable globale pour savoir quel joueur joue 
        int Numero_Joueur_Tour;

        //FOMR POUR LA PIOCHE
        Form PopUpCartesPioche = new Form();

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
                Choix_Personnage();
                Choix_perso.FormClosed += Savoir_fermeture;
                if (Choixpersonnage_ferme)
                { 

                Appel_Personnage();

                //Masque les cartes de l'autre joueur 
                if (Numero_Joueur_Tour == 1)
                {
                    MasqueJoueur2.Visible = true;
                }
                else if (Numero_Joueur_Tour == 2)
                {
                    MasqueJoueur1.Visible = true;
                }

                ChoixPendantLeTour();
            }
            }
        }
        // compter le nombre de personnage dans tabpage 
        private int  Compte()
        {
            int plop = tabPage1.Controls.Count;
            int plop2 = tabPage3.Controls.Count;
            int resultat = plop + plop2;
            return resultat;
        }

        private void Savoir_fermeture(Object sender, FormClosedEventArgs e)
        {
            int nbpersoOk = Compte();
            // parce que control pour cacher les cartes quand ce n'est pas à son tour
            if(nbpersoOk == 5)
            {
                Appel_Personnage();
                if (Numero_Joueur_Tour == 1)
                {
                    MasqueJoueur2.Visible = true;
                }
                else if (Numero_Joueur_Tour == 2)
                {
                    MasqueJoueur1.Visible = true;
                }

                ChoixPendantLeTour();
            }
        }
        //AFFICHAGE DES CARTES PERSONNAGES
        private void  Choix_Personnage()
        {
            int position = 1 ;
            Choix_perso.Width = 1500;
            Choix_perso.StartPosition = FormStartPosition.CenterScreen;
            Random suppressionperso = new Random();
            int Personnagesupprime = suppressionperso.Next(1, 8);
            //MessageBox.Show("le numero random :" + lechoisi);
            for (int i = 1; i < 9; i++)
            {
                if (i != Personnagesupprime)
                {

                    PictureBox personnage = new PictureBox();
                    personnage.Top = 50;
                    personnage.Left = 60 + (position * 120);
                    Image lacarte = Image.FromFile("C:/csharp/CitadellesV2/Images_cartes/personnage" + i + ".PNG");
                    personnage.SizeMode = PictureBoxSizeMode.Zoom;
                    personnage.Width = 92;
                    personnage.Height = 150;
                    personnage.Image = lacarte;
                    personnage.Tag = i;
                    personnage.Click += Personnage_Choisi;
                    Choix_perso.Controls.Add(personnage);
                    position++;
                    


                    //tours de choix suppression personnages
                    
                }
            }
            Choix_perso.Show();
            //voir comment faire avec le "roi" pour le 1er qui pioche vg? faire un random pour le 1er tour?
            MessageBox.Show("Joueur 1 choisi ton personnage");
            //Choixpersonnage_ferme = true;

            
        }

        private void Personnage_Choisi(object sender, EventArgs e)
        {
            PictureBox lepersonnage_clicke = (PictureBox)sender;

            if (nbtours == 6 || nbtours == 4 || nbtours == 2)
            {
                if (nbtours == 6)
                {
                    ajoutperso1J2.Image = lepersonnage_clicke.Image;
                    ajoutperso1J2.Tag = lepersonnage_clicke.Tag;
                }
                else if (nbtours == 4){
                    ajoutperso2J1.Image = lepersonnage_clicke.Image;
                    ajoutperso2J1.Tag = lepersonnage_clicke.Tag;
                }
                else if(nbtours == 2)
                {
                    ajoutperso2J2.Image = lepersonnage_clicke.Image;
                    ajoutperso2J2.Tag = lepersonnage_clicke.Tag;
                }
                Image ledos = Image.FromFile("C:/csharp/CitadellesV2/Images_cartes/Couverture_jeu.png");
                lepersonnage_clicke.Image = ledos;
                lepersonnage_clicke.Enabled = false;
                MessageBox.Show("Choisi de supprimer un personnage", "action");
                //L'ajouter au bon tabpage

            }
            else if (nbtours == 7)
            {
                ajoutperso1J1.Image = lepersonnage_clicke.Image;
                ajoutperso1J1.Tag = lepersonnage_clicke.Tag;
                Image ledos = Image.FromFile("C:/csharp/CitadellesV2/Images_cartes/Couverture_jeu.png");
                lepersonnage_clicke.Image = ledos;
                lepersonnage_clicke.Enabled = false;
                MessageBox.Show("Choisi un personnage", "action");
            }

            else if (nbtours == 5 || nbtours == 3)
            {
               
                Image ledos = Image.FromFile("C:/csharp/CitadellesV2/Images_cartes/Couverture_jeu.png");
                lepersonnage_clicke.Image = ledos;
                lepersonnage_clicke.Enabled = false;
                MessageBox.Show("Choisi un personnage", "action");

            }

            else
            {
                Choix_perso.Close();
                MessageBox.Show("La partie peut commencer ! ");
            }

            nbtours--;

        }
        //METHODE D'APPEL POUR CHAQUE PERSONNAGE 
        public void Appel_Personnage()
        {
            if(PersonnageQuiJoue == 8)
            {
                //ON ARRIVE A LA FIN DU TOUR, IL FAUT DONC COMPTER LES CARTES QUARTIERS SI ELLES NE SONT PAS AU NOMBRE DE HUIT, PROPOSE UN NOUVEAU CHOIX DE PERSONNAGE
            }

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
                        Numero_Joueur_Tour = 1; //on sait du coup que c'est le joueur 1 qui a la carte est donc peut ensuite disabled le numero du joueur en face
                        MessageBox.Show("C'est au tour " + Perso + ". "+nom1+" c'est à toi de jouer ! ", "A qui le tour ?");
                    }
                }

                foreach (Control CartePersonnageChoisie in tabPage3.Controls)
                {


                    if (PersonnageQuiJoue == Convert.ToInt16(CartePersonnageChoisie.Tag))
                    {
                        String Perso = Nom_personnage(Convert.ToInt16(CartePersonnageChoisie.Tag));
                        PersonnageEstChoisi = true;
                        Numero_Joueur_Tour = 2; //on sait du coup que c'est le joueur 2 qui a la carte est donc peut ensuite disabled le numero du joueur en face
                        MessageBox.Show("C'est au tour " + Perso + ". " + nom2 + " c'est à toi de jouer ! ", "A qui le tour ?");
                    }
                }
            }
        }

        //Mis à part comme ça peut appeler à chaque tour
        public void ChoixPendantLeTour() { 

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
                Pouvoir.Tag = "pouvoir";
                Pioche.Width = 100;
                Pioche.Height = 50;
                Pioche.Text = "Piocher des cartes";
                Pioche.Tag = "pioche";
                Pieces.Width = 100;
                Pieces.Height = 50;
                Pieces.Text = "Collecter des pièces";
                Pieces.Tag = "piece";
                Construction.Width = 100;
                Construction.Height = 50;
                Construction.Text = "Construire un quartier";
                Construction.Tag = "construction";   
                Construction.Top = 80;
                Construction.Left = 150;
                Pieces.Top = 80;
                Pioche.Left = 150;

                ///RECUPERE POUR LES BOUTONS PIOCHE ET PIECE LE RANG DANS LES CONTROLS 
                rang_bouton_piece = ChoixTour.Controls.IndexOf(Pieces);
                rang_bouton_pioche = ChoixTour.Controls.IndexOf(Pioche);

                ChoixTour.StartPosition = FormStartPosition.CenterScreen;
                ChoixTour.TopMost = true;
                ChoixTour.Show();


                Pieces.Click += UneAction;
                Pioche.Click += UneAction;
                Pouvoir.Click += UneAction;
                Construction.Click += UneAction;


            //voir pour rajouter un bouton fin de partie + test si personne ferme la fenêtre, mettre un message "oups tu passes ton tour" si oui,passe au joueur suivant, si non, lui affiche à nouveau la form 
        }

        //QUAND CHOISI DE PIOCHER DES PIECES ( a modifier )
        private void UneAction(object sender, EventArgs e)
        {
            Button boutonClique = (Button)sender ;
            string tag_bouton = Convert.ToString(boutonClique.Tag);
            if (tag_bouton == "piece")
           {
                if(Numero_Joueur_Tour == 1)
                {
                    int ajoutpiece = Convert.ToInt16(PiecesJ1.Text);
                    ajoutpiece += 2;
                    PiecesJ1.Text = Convert.ToString(ajoutpiece);
                }else if(Numero_Joueur_Tour == 2)
                {
                    int ajoutpiece = Convert.ToInt16(PiecesJ2.Text);
                    ajoutpiece += 2;
                    PiecesJ2.Text = Convert.ToString(ajoutpiece);
                }
                
                boutonClique.Enabled = false ;
                ChoixTour.Controls[rang_bouton_pioche].Enabled = false;
           }else if(tag_bouton == "pioche")
            {
                // MessageBox.Show("Pioche !");
                

                Label title_form_pioche = new Label();
                PopUpCartesPioche.Controls.Add(title_form_pioche);
                title_form_pioche.Text = "Quelle carte veux-tu choisir ? ";
                title_form_pioche.Width = 200;

                //RANDOM QUI SELECTIONNE DEUX CARTES DANS LA PIOCHE
                int NumCarte1 = Generation_nb_Aleatoire();
                int NumCarte2 = Generation_nb_Aleatoire();

                //PROPOSE DEUX CARTES DANS LA PIOCHE 
                PictureBox Carte1 = (PictureBox) pioche.Controls[NumCarte1];
                PopUpCartesPioche.Controls.Add(Carte1);
                Carte1.Top = 40;

                PictureBox Carte2 = (PictureBox)pioche.Controls[NumCarte1];
                PopUpCartesPioche.Controls.Add(Carte2);
                Carte2.Top = 40;
                Carte2.Left = 150;

                //JOUEUR CHOISIT SA CARTE ET CETTE DERNIERE APPARAIT DANS TABPAGE QUARTIER 
                Carte1.Click += ChoixCartePioche;
                Carte2.Click += ChoixCartePioche;

                PopUpCartesPioche.StartPosition = FormStartPosition.CenterScreen;
                PopUpCartesPioche.TopMost = true;
                PopUpCartesPioche.Show();

                boutonClique.Enabled = false;
                ChoixTour.Controls[rang_bouton_piece].Enabled = false;
            }
            else if(tag_bouton == "pouvoir")
            {
                MessageBox.Show("Utilise ses pouvoirs !");
                //EXECUTION DU POUVOIR EN FONCTION DU ROLE
                boutonClique.Enabled = false;
            }
            else if(tag_bouton == "construction")
            {
                MessageBox.Show("Construit !");
                //PROPOSE LES CARTES QUARTIERS 
                // CLIQUE SUR LA CARTE DE CONSTRUCTION VOULUE
                // COMPTE LES PIECES ET COMPARE AVEC NOMBRE DE PIECES 
                // /!\ VOIR SI PAS DISABLED DIRECTEMENT LE BOUTON EN FONCTION DU NOMBRE DE PIECES DISPONIBLES ET EN FONCTION DES PIECES SUR LES TAG DES CARTES QUARTIES 
                boutonClique.Enabled = false;
            }
        }

        private void ChoixCartePioche(object sender, EventArgs e)
        {
            PictureBox CarteChoisie = (PictureBox)sender;

            if(Numero_Joueur_Tour == 1)
            {
                tabPage2.Controls.Add(CarteChoisie);
            }else if (Numero_Joueur_Tour == 2)
            {
                tabPage4.Controls.Add(CarteChoisie);
            }

            PopUpCartesPioche.Close();
        }


        private int Generation_nb_Aleatoire()
        {
            Random alea = new Random();

            int nb = alea.Next(0, 3);

            return nb;
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
