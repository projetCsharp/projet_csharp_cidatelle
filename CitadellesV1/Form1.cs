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
        String nom1, nom2, lebouton;
        Form Commencer_partie = new Form();
        int PersonnageQuiJoue = 0;

        bool CommencerPartie_ferme = false;
        //Creation de la form pour les choix des joueurs à chaque tour
        Form ChoixTour = new Form();

        int rang_bouton_pioche;
        int rang_bouton_piece;

        //variable globale pour savoir quel joueur joue 
        int Numero_Joueur_Tour;

        //FOMR POUR LA PIOCHE
       Form FormChoixPioche = new Form();

        //BOUTON PIOCHE ET PIECE POUR POUVOIR LES DISABLED LORSQUE JOUEUR CHOISIT L'UNE DES DEUX POSSIBILITES
        Button Pioche = new Button();
        Button Pieces = new Button();
        Button Pouvoir = new Button();
        Button Construction = new Button();

        //CREATION DES CARTES DE LA PIOCHE EN VARIABLE GLOBALE POUR POUVOIR LES REPLACER DANS LA PIOCHE
        PictureBox Carte1 = new PictureBox();
        PictureBox Carte2 = new PictureBox();

       

        PictureBox CopieCarte = new PictureBox();
        PictureBox CopieCarte2 = new PictureBox();

        // CONNAITRE LE NUMERO DU JOUEUR QUI EST ROI 
        int JoueurRoi = 0;

        public Form1()
        {
            InitializeComponent();
            //this.IsMdiContainer = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //METHODE POUR L'OUVERTURE DE LA FORME POUR INSCRIRE LES NOMS DES JOUEURS 
            Commencer_partie_essai();


            tabPage1.Text = "Personnages";
            tabPage3.Text = "Personnages";
            tabPage2.Text = "Quartiers";
            tabPage4.Text = "Quartiers";
            PiecesJ1.Text = "2";
            PiecesJ2.Text = "2";

            //ANALYSE DES CARTES CHOISIE PAR LES JOUEURS POUR L'APPEL DES DIFFERENTS PERSONNAGES

            if (CommencerPartie_ferme)
            { // test si la form pour choisir les prénoms est fermé, si c'est le cas peut excécuter le code suivant 

                Appel_Personnage();

                MasqueCarteJoueurOppose();

                ChoixPendantLeTour();
            }
        }

        public void MasqueCarteJoueurOppose()
        {
            if (Numero_Joueur_Tour == 1)
            {
                MasqueJoueur2.Visible = true;
                MasqueJoueur1.Visible = false;
            }
            else if (Numero_Joueur_Tour == 2)
            {
                MasqueJoueur1.Visible = true;
                MasqueJoueur2.Visible = false;
            }
        }

        //METHODE D'APPEL POUR CHAQUE PERSONNAGE 
        public void Appel_Personnage()
        {
            if (PersonnageQuiJoue == 8)
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
                        MessageBox.Show("C'est au tour " + Perso + ". " + nom1 + " c'est à toi de jouer ! ", "A qui le tour ?");
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
            MasqueCarteJoueurOppose();
            //ChoixPendantLeTour();

        }

        //Mis à part comme ça peut appeler à chaque tour
        public void ChoixPendantLeTour()
        {
            Form ChoixTour = new Form();
           
            //Button Pouvoir = new Button();
            //Button Pioche = new Button(); // EN VARAIBLE GLOBAL 
            //Button Pieces = new Button();
            //Button Construction = new Button();
            Button Fin = new Button();
            ChoixTour.Controls.Add(Pouvoir);
            ChoixTour.Controls.Add(Pioche);
            ChoixTour.Controls.Add(Pieces);
            ChoixTour.Controls.Add(Construction);
            ChoixTour.Controls.Add(Fin);
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

            Fin.Width = 100;
            Fin.Height = 50;
            Fin.Text = "Je m'arrête là";
            Fin.Top = 150;
            Fin.Left = 100;
            Fin.Click += FinTourJoueur;

            //RECUPERE POUR LES BOUTONS PIOCHE ET PIECE LE RANG DANS LES CONTROLS 
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


        private void FinTourJoueur(object sender, EventArgs e)
        {
            Pioche.Enabled = true;
            Pieces.Enabled = true;
            Pouvoir.Enabled = true;
            Construction.Enabled = true;
            //ReplacerCartePioche();
            ChoixTour.Close();
            Appel_Personnage();
        }

        //CHOIX ACTION DURANT LE TOUR
        private void UneAction(object sender, EventArgs e)
        {
            Button boutonClique = (Button)sender;
            string tag_bouton = Convert.ToString(boutonClique.Tag);
            if (tag_bouton == "piece")
            {
                if (Numero_Joueur_Tour == 1)
                {
                    int ajoutpiece = Convert.ToInt16(PiecesJ1.Text);
                    ajoutpiece += 2;
                    PiecesJ1.Text = Convert.ToString(ajoutpiece);
                }
                else if (Numero_Joueur_Tour == 2)
                {

                    int ajoutpiece = Convert.ToInt16(PiecesJ2.Text);
                    ajoutpiece += 2;
                    PiecesJ2.Text = Convert.ToString(ajoutpiece);
                }

                boutonClique.Enabled = false;
                Pioche.Enabled = false;
                //ChoixTour.Controls[rang_bouton_pioche].Enabled = false;
            }
            else if (tag_bouton == "pioche")
            {
                // MessageBox.Show("Pioche !");
                Form PopUpCartesPioche = new Form();
                Label title_form_pioche = new Label();
                PopUpCartesPioche.Controls.Add(title_form_pioche);
                title_form_pioche.Text = "Quelle carte veux-tu choisir ? ";
                title_form_pioche.Width = 200;

                //RANDOM QUI SELECTIONNE DEUX CARTES DANS LA PIOCHE
                int NumCarte1 = Generation_nb_Aleatoire();
                int NumCarte2 = Generation_nb_Aleatoire();

                //int nombre_carte_pioche = pioche.Controls.Count;
                //MessageBox.Show("Voici le nombre de carte dans la pioche : " + nombre_carte_pioche + " et les nums des cartes au hasard : " + NumCarte1 + " et " + NumCarte2);

                //foreach (PictureBox cartedelapioche in pioche.Controls)
                //{
                //    int indice = pioche.Controls.IndexOf(cartedelapioche);
                //    MessageBox.Show("Voici l'indice de lac arte de la picohe " + indice);
                //}

                //CARTE PRISE DANS LA PIOCHE, disparaise après 
                //et num généré est le même pour les deux cartes


                //PROPOSE DEUX CARTES DANS LA PIOCHE 
                Carte1 = (PictureBox)pioche.Controls[NumCarte1];
                PopUpCartesPioche.Controls.Add(Carte1);
                Carte1.Top = 40;

                Carte2 = (PictureBox)pioche.Controls[NumCarte1];
                PopUpCartesPioche.Controls.Add(Carte2);
                Carte2.Top = 40;
                Carte2.Left = 150;

                FormChoixPioche = PopUpCartesPioche;

                //JOUEUR CHOISIT SA CARTE ET CETTE DERNIERE APPARAIT DANS TABPAGE QUARTIER 
                Carte1.Click += ChoixCartePioche;
                Carte2.Click += ChoixCartePioche;
                
                //CopieCarte.Tag = Carte1.Tag;
                //CopieCarte.Name = Carte1.Name;
                //CopieCarte.BackColor = Carte1.BackColor;
                //CopieCarte.Image = Carte1.Image;
                ////CopieCarte.Click += ChoixCartePioche;

                //CopieCarte2.Tag = Carte2.Tag;
                //CopieCarte2.Name = Carte2.Name;
                //CopieCarte2.BackColor = Carte2.BackColor;
                //CopieCarte2.Image = Carte2.Image;
                ////CopieCarte2.Click += ChoixCartePioche;



                PopUpCartesPioche.StartPosition = FormStartPosition.CenterScreen;
                PopUpCartesPioche.TopMost = true;
                PopUpCartesPioche.Show();

                boutonClique.Enabled = false;
                Pieces.Enabled = false;
                // ChoixTour.Controls[rang_bouton_piece].Enabled = false;
        
               
            }
            else if (tag_bouton == "pouvoir")  //EXECUTION DU POUVOIR EN FONCTION DU ROLE
            {
                MessageBox.Show("Utilise ses pouvoirs !");

                int nbQuartier = 0;

                switch (PersonnageQuiJoue)
                {
                    case 1: //ASSASSIN
                        break;

                    case 2: // VOLEUR
                        break;

                    case 3: // MAGICIEN
                        break;

                    case 4: //ROI
                                    if (Numero_Joueur_Tour == 1)
                                    {
                                        foreach (PictureBox carteQuartier in citeJ1.Controls)
                                        {
                                            if (carteQuartier.BackColor == Roi.BackColor)
                                            {
                                                nbQuartier++;
                                            }
                                        }


                                        if (nbQuartier != 0)
                                        {
                                            MessageBox.Show("Tu as " + nbQuartier + " quartiers nobles, cela te rapporte donc " + nbQuartier + " pièces", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            int nbPieceJoueur = Convert.ToInt32(PiecesJ1.Text);
                                            nbPieceJoueur += nbQuartier;
                                            PiecesJ1.Text = Convert.ToString(nbPieceJoueur);
                                        }
                                        //else
                                        //{
                                        //    MessageBox.Show("Tu n'as pas de")
                                        //}


                                        JoueurRoi = 1;
                                    }
                                    else if (Numero_Joueur_Tour == 2)
                                    {
                                        foreach (PictureBox carteQuartier in citeJ2.Controls)
                                        {
                                            if (carteQuartier.BackColor == Roi.BackColor)
                                            {
                                                nbQuartier++;
                                            }
                                        }


                                        if (nbQuartier != 0)
                                        {
                                            MessageBox.Show("Tu as " + nbQuartier + " quartiers nobles, cela te rapporte donc " + nbQuartier + " pièces", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            int nbPieceJoueur = Convert.ToInt32(PiecesJ2.Text);
                                            nbPieceJoueur += nbQuartier;
                                            PiecesJ2.Text = Convert.ToString(nbPieceJoueur);
                                        }

                                        JoueurRoi = 2;  
                                    }    
                        break;

                    case 5: // EVEQUE 
                                    if (Numero_Joueur_Tour == 1)
                                    {
                                        foreach (PictureBox carteQuartier in citeJ1.Controls)
                                        {
                                            if (carteQuartier.BackColor == Eveque.BackColor)
                                            {
                                                nbQuartier++;
                                            }
                                        }


                                        if (nbQuartier != 0)
                                        {
                                            MessageBox.Show("Tu as " + nbQuartier + " quartiers religieux, cela te rapporte donc " + nbQuartier + " pièces", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            int nbPieceJoueur = Convert.ToInt32(PiecesJ1.Text);
                                            nbPieceJoueur += nbQuartier;
                                            PiecesJ1.Text = Convert.ToString(nbPieceJoueur);
                                        }
                                        //else
                                        //{
                                        //    MessageBox.Show("Tu n'as pas de")
                                        //}


                                        
                                    }
                                    else if (Numero_Joueur_Tour == 2)
                                    {
                                        foreach (PictureBox carteQuartier in citeJ2.Controls)
                                        {
                                            if (carteQuartier.BackColor == Eveque.BackColor)
                                            {
                                                nbQuartier++;
                                            }
                                        }


                                        if (nbQuartier != 0)
                                        {
                                            MessageBox.Show("Tu as " + nbQuartier + " quartiers religieux, cela te rapporte donc " + nbQuartier + " pièces", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            int nbPieceJoueur = Convert.ToInt32(PiecesJ2.Text);
                                            nbPieceJoueur += nbQuartier;
                                            PiecesJ2.Text = Convert.ToString(nbPieceJoueur);
                                        }

                                        
                                    }
                        break;

                    case 6: // MARCHAND
                      
                        if (Numero_Joueur_Tour == 1)
                                    {
                                        int ajoutpiece = Convert.ToInt16(PiecesJ1.Text);
                                        ajoutpiece += 1;
                                        PiecesJ1.Text = Convert.ToString(ajoutpiece);


                                        foreach (PictureBox carteQuartier in citeJ1.Controls)
                                        {
                                            if (carteQuartier.BackColor == Marchand.BackColor)
                                            {
                                                nbQuartier++;
                                            }
                                        }


                                        if (nbQuartier != 0)
                                        {
                                            MessageBox.Show("Tu as " + nbQuartier + " quartiers commerçants, cela te rapporte donc " + nbQuartier + " pièces", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            int nbPieceJoueur = Convert.ToInt32(PiecesJ1.Text);
                                            nbPieceJoueur += nbQuartier;
                                            PiecesJ1.Text = Convert.ToString(nbPieceJoueur);
                                        }
                                        //else
                                        //{
                                        //    MessageBox.Show("Tu n'as pas de")
                                        //}



                                    }
                                    else if (Numero_Joueur_Tour == 2)
                                    {
                                        int ajoutpiece = Convert.ToInt16(PiecesJ2.Text);
                                        ajoutpiece += 1;
                                        PiecesJ2.Text = Convert.ToString(ajoutpiece);

                                        foreach (PictureBox carteQuartier in citeJ2.Controls)
                                        {
                                            if (carteQuartier.BackColor == Marchand.BackColor)
                                            {
                                                nbQuartier++;
                                            }
                                        }


                                        if (nbQuartier != 0)
                                        {
                                            MessageBox.Show("Tu as " + nbQuartier + " quartiers commerçants, cela te rapporte donc " + nbQuartier + " pièces", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            int nbPieceJoueur = Convert.ToInt32(PiecesJ2.Text);
                                            nbPieceJoueur += nbQuartier;
                                            PiecesJ2.Text = Convert.ToString(nbPieceJoueur);
                                        }


                                    }


                        break;

                    case 7:// ARCHITECTE

                        break;

                    case 8: // CONDITTIERE

                                        if (Numero_Joueur_Tour == 1)
                                        {
                                            foreach (PictureBox carteQuartier in citeJ1.Controls)
                                            {
                                                if (carteQuartier.BackColor == Condottiere.BackColor)
                                                {
                                                    nbQuartier++;
                                                }
                                            }


                                            if (nbQuartier != 0)
                                            {
                                                MessageBox.Show("Tu as " + nbQuartier + " quartiers militaires, cela te rapporte donc " + nbQuartier + " pièces", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                int nbPieceJoueur = Convert.ToInt32(PiecesJ1.Text);
                                                nbPieceJoueur += nbQuartier;
                                                PiecesJ1.Text = Convert.ToString(nbPieceJoueur);
                                            }
                                            //else
                                            //{
                                            //    MessageBox.Show("Tu n'as pas de")
                                            //}



                                        }
                                        else if (Numero_Joueur_Tour == 2)
                                        {
                                            foreach (PictureBox carteQuartier in citeJ2.Controls)
                                            {
                                                if (carteQuartier.BackColor == Condottiere.BackColor)
                                                {
                                                    nbQuartier++;
                                                }
                                            }


                                            if (nbQuartier != 0)
                                            {
                                                MessageBox.Show("Tu as " + nbQuartier + " quartiers militaires, cela te rapporte donc " + nbQuartier + " pièces", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                int nbPieceJoueur = Convert.ToInt32(PiecesJ2.Text);
                                                nbPieceJoueur += nbQuartier;
                                                PiecesJ2.Text = Convert.ToString(nbPieceJoueur);
                                            }
                                        }

                        Form choixQuartierDestruction = new Form();
                        //METTRE UNE IMAGE GENRE EPEE OU UN ELEMENT QUI REPRESENTE LA DESTRUCTION ET JOEUUR POURRA DRAG DROP LE QUARTIER VOULU POUR LE DETRUIRE 


                        break;
                }

                boutonClique.Enabled = false;
            }
            else if (tag_bouton == "construction")
            {
                MessageBox.Show("Construit !");

                int nombreCarteQuartier = 0;

                if (Numero_Joueur_Tour == 1)
                {
                    nombreCarteQuartier = tabPage2.Controls.Count;
                }
                else if (Numero_Joueur_Tour == 2)
                {
                    nombreCarteQuartier = tabPage4.Controls.Count;
                }
                Form afficheCarteQuartier = new Form();
                afficheCarteQuartier.StartPosition = FormStartPosition.CenterScreen;
                afficheCarteQuartier.TopMost = true;
                Label questionquartier = new Label();
                questionquartier.Text = "Quel quartier souhaites-tu construire ?";
                afficheCarteQuartier.Controls.Add(questionquartier);


                if (nombreCarteQuartier != 0)
                {

                    int i = 0;

                    if (Numero_Joueur_Tour == 1)
                    {
                        foreach (PictureBox carteQuartierJoueur in tabPage2.Controls)
                        {
                            afficheCarteQuartier.Controls.Add(carteQuartierJoueur);
                            carteQuartierJoueur.Top = 30;
                            carteQuartierJoueur.Left = 30 + (100 * i);
                            i++;
                            carteQuartierJoueur.Click += ChoixConstructionQuartier;
                        }

                    }
                    else if (Numero_Joueur_Tour == 2)
                    {
                        foreach (PictureBox carteQuartierJoueur in tabPage4.Controls)
                        {
                            afficheCarteQuartier.Controls.Add(carteQuartierJoueur);
                            carteQuartierJoueur.Top = 30;
                            carteQuartierJoueur.Left = 30 + (100 * i);
                            i++;
                            carteQuartierJoueur.Click += ChoixConstructionQuartier;
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Malheureusement, tu n'as pas de cartes Quartiers");
                }

                afficheCarteQuartier.Show();
                // /!\ VOIR SI PAS DISABLED DIRECTEMENT LE BOUTON EN FONCTION DU NOMBRE DE PIECES DISPONIBLES ET EN FONCTION DES PIECES SUR LES TAG DES CARTES QUARTIES 
                boutonClique.Enabled = false;
            }
        }

        private void FermetureForm(object sender)
        {
            Form element = (Form)sender;
            element.Close();
        }


        private void ChoixConstructionQuartier(object sender, EventArgs e)
        {
            int nombre_de_pieces = 0;
            Panel PanelJoueur = new Panel();
            if (Numero_Joueur_Tour == 1)
            {
                nombre_de_pieces = Convert.ToInt32(PiecesJ1.Text);
                PanelJoueur = citeJ1;
            }
            else if (Numero_Joueur_Tour == 2)
            {
                nombre_de_pieces = Convert.ToInt32(PiecesJ2.Text);
                PanelJoueur = citeJ2;
            }

            PictureBox QuartierSelectionné = (PictureBox)sender;
            int prixQuartierSelectionné = Convert.ToInt32(QuartierSelectionné.Tag);
            if (nombre_de_pieces >= prixQuartierSelectionné)
            {
                MessageBox.Show("suffisament de pièces");
                PanelJoueur.Controls.Add(QuartierSelectionné);
                nombre_de_pieces -= prixQuartierSelectionné;

                if (Numero_Joueur_Tour == 1)
                {
                    PiecesJ1.Text = Convert.ToString(nombre_de_pieces);
                }
                else if (Numero_Joueur_Tour == 2)
                {
                    PiecesJ2.Text = Convert.ToString(nombre_de_pieces);
                }

            }
            else
            {
                MessageBox.Show("Malheureusement, tu n'as pas suffisament de pièces pour construire ce quartier", "Gagne plus de pièces ! ");
            }

            ActiveForm.Close();
        }


        private void ChoixCartePioche(object sender, EventArgs e)
        {
            PictureBox CarteChoisie = (PictureBox)sender;

            if (Numero_Joueur_Tour == 1)
            {
                tabPage2.Controls.Add(CarteChoisie);
            }
            else if (Numero_Joueur_Tour == 2)
            {
                tabPage4.Controls.Add(CarteChoisie);
            }

           

           // CartePiocheChoisit = true;

            FermetureForm(FormChoixPioche);
        }

        private void ReplacerCartePioche()
        {
            //remet dans la pioche 
 
            pioche.Controls.Add(CopieCarte);
            pioche.Controls.Add(CopieCarte2);
        }

        private int Generation_nb_Aleatoire()
        {
            Random alea = new Random();

            int nb = alea.Next(0, 8);

            return nb;
        }

        private String Nom_personnage(int num_carte)
        {
            String NomPersonnage = "";
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
            MessageBox.Show(regles, titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
