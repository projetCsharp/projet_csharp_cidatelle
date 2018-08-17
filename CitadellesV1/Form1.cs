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

        int nbtours = 7;

        bool CommencerPartie_ferme = false;
        //Creation de la form pour les choix des joueurs à chaque tour
        Form ChoixTour = new Form();
        bool FormTourCree = false;

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
        int JoueurRoi = 2;
        // connaitre le numéro du joueur qui est voleur
        int JoueurVoleur = 0;

        //RECUPERATION DE LA CARTE POUR LA DESTRUCTION D'UN QUARTIER PAR LE CONDOT
        PictureBox CopieCarteDestruction = new PictureBox();

        //VARIRABLE GLOBALE POUR SAVOIR QUEL PERSONNAGE A ETE TUE 
        int Personnage_Assassine = 0;
        //VARIRABLE GLOBALE POUR SAVOIR QUEL PERSONNAGE A ETE VOLE
        int Personnage_Vole = 0;

//FORM CHOIX PERSONNAGE
        Form Choix_perso = new Form();
        //BOOL POUR SAVOIR SI HOIX DES PERSONNAGES EST FERME
        bool Choixpersonnage_ferme = false;

        //Pour savoir si le personnage est mort  
        bool Personnage_Mort = false;

        Form Commencer_tour = new Form();

        bool tourfini = false;

        bool commencer_tour_ferme = false;

        int CliqueConstrruction = 0;

        List<PictureBox> ListeDesQuartiers = new List<PictureBox>();

        List<PictureBox> quartiersJ1 = new List<PictureBox>();
        List<PictureBox> quartiersJ2 = new List<PictureBox>();


        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = SystemColors.Control;
        }

        public partial class FormInstance : Form
        {
            
           
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
                Choix_Personnage();
                Choix_perso.FormClosed += Savoir_fermeture;

                Button debut_tour = new Button();
                debut_tour.Text = "Commencer le tour";
                debut_tour.Click += Tour;
                Commencer_tour.Controls.Add(debut_tour);
                
                Commencer_tour.TopMost = true;

                if (Choixpersonnage_ferme) {

                    MessageBox.Show("on est dans le if et la fermeture de la box vaut " + Choixpersonnage_ferme);

                }

            }           
        }
        // compter le nombre de personnage dans tabpage 
        private int Compte()
        {
            int plop = tabPage1.Controls.Count;
            int plop2 = tabPage3.Controls.Count;
            int resultat = plop + plop2;
            return resultat;
        }

        private void Savoir_fermeture(Object sender, FormClosedEventArgs e)
        {
            int nbpersoOk = Compte();
            //MessageBox.Show("ce que vaut nbpersoOK : " + nbpersoOk);
            // parce que control pour cacher les cartes quand ce n'est pas à son tour
            //while (PersonnageQuiJoue <= 8)
            //{
            //    Appel_Personnage();
            //    MasqueCarteJoueurOppose();
            //    if (!Personnage_Mort)
            //    {
            //        ChoixPendantLeTour();
            //    }
            //}

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
                    Image lacarte = Image.FromFile("Images_cartes/personnage" + i + ".PNG");
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
            Choix_perso.TopMost = true;
            //voir comment faire avec le "roi" pour le 1er qui pioche vg? faire un random pour le 1er tour?
            if(JoueurRoi == 1)
            {
                MessageBox.Show(nom1 + " Choisi ton personnage");
            }
            else if (JoueurRoi == 2)
            {
                MessageBox.Show(nom2 + " Choisi ton personnage");
            }
            
        }

        private void Personnage_Choisi(object sender, EventArgs e)
        {
            PictureBox lepersonnage_clicke = (PictureBox)sender;
            if(JoueurRoi == 1)
            { 
                if (nbtours == 6 || nbtours == 4 || nbtours == 2)
                {
                    if (nbtours == 6)
                    {
                        ajoutperso1J2.Image = lepersonnage_clicke.Image;
                        ajoutperso1J2.Tag = lepersonnage_clicke.Tag;
                        MessageBox.Show(nom2+" Choisi de supprimer un personnage", "action");
                    }
                    else if (nbtours == 4)
                    {
                        ajoutperso2J1.Image = lepersonnage_clicke.Image;
                        ajoutperso2J1.Tag = lepersonnage_clicke.Tag;
                        MessageBox.Show(nom1+" Choisi de supprimer un personnage", "action");
                    }
                    else if (nbtours == 2)
                    {
                        ajoutperso2J2.Image = lepersonnage_clicke.Image;
                        ajoutperso2J2.Tag = lepersonnage_clicke.Tag;
                        MessageBox.Show(nom2+" Choisi de supprimer un personnage", "action");
                    }
                    Image ledos = Image.FromFile("Images_cartes/Couverture_jeu.png");
                    lepersonnage_clicke.Image = ledos;
                    lepersonnage_clicke.Enabled = false;
                    
                    //L'ajouter au bon tabpage

                }
                else if (nbtours == 7)
                {
                    ajoutperso1J1.Image = lepersonnage_clicke.Image;
                    ajoutperso1J1.Tag = lepersonnage_clicke.Tag;
                    Image ledos = Image.FromFile("Images_cartes/Couverture_jeu.png");
                    lepersonnage_clicke.Image = ledos;
                    lepersonnage_clicke.Enabled = false;
                    MessageBox.Show(nom2+ " Choisi un personnage", "action");
                }

                else if (nbtours == 5 || nbtours == 3)
                {

                    Image ledos = Image.FromFile("Images_cartes/Couverture_jeu.png");
                    lepersonnage_clicke.Image = ledos;
                    lepersonnage_clicke.Enabled = false;
                    if(nbtours == 5)
                    {
                        MessageBox.Show(nom1+" Choisi un personnage", "action");
                    }
                    else
                    {
                        MessageBox.Show(nom2+" Choisi un personnage", "action");
                    }
                    

                }

                else
                {
                    Choix_perso.Close();
                    MessageBox.Show("La partie peut commencer ! ");
                    Commencer_tour.Show();
                }

            }
            //quand le joueur roi = 2
            else
            {
                if (nbtours == 6 || nbtours == 4 || nbtours == 2)
                {
                    if (nbtours == 6)
                    {
                        ajoutperso1J1.Image = lepersonnage_clicke.Image;
                        ajoutperso1J1.Tag = lepersonnage_clicke.Tag;
                        MessageBox.Show(nom1+" Choisi de supprimer un personnage", "action");
                    }
                    else if (nbtours == 4)
                    {
                        ajoutperso2J2.Image = lepersonnage_clicke.Image;
                        ajoutperso2J2.Tag = lepersonnage_clicke.Tag;
                        MessageBox.Show(nom2+" Choisi de supprimer un personnage", "action");
                    }
                    else if (nbtours == 2)
                    {
                        ajoutperso2J1.Image = lepersonnage_clicke.Image;
                        ajoutperso2J1.Tag = lepersonnage_clicke.Tag;
                        MessageBox.Show(nom1+" Choisi de supprimer un personnage", "action");
                    }
                    Image ledos = Image.FromFile("Images_cartes/Couverture_jeu.png");
                    lepersonnage_clicke.Image = ledos;
                    lepersonnage_clicke.Enabled = false;
                    //L'ajouter au bon tabpage

                }
                else if (nbtours == 7)
                {
                    ajoutperso1J2.Image = lepersonnage_clicke.Image;
                    ajoutperso1J2.Tag = lepersonnage_clicke.Tag;
                    Image ledos = Image.FromFile("Images_cartes/Couverture_jeu.png");
                    lepersonnage_clicke.Image = ledos;
                    lepersonnage_clicke.Enabled = false;
                    MessageBox.Show(nom1 + " choisi un personnage", "action");
                }

                else if (nbtours == 5 || nbtours == 3)
                {

                    Image ledos = Image.FromFile("Images_cartes/Couverture_jeu.png");
                    lepersonnage_clicke.Image = ledos;
                    lepersonnage_clicke.Enabled = false;
                    if(nbtours == 5)
                    {
                        MessageBox.Show(nom2+" Choisi un personnage", "action");
                    }
                    else
                    {
                        MessageBox.Show(nom1+" Choisi un personnage", "action");
                    }
                    

                }

                else
                {
                    Choix_perso.Close();
                    MessageBox.Show("La partie peut commencer ! ");
                    Commencer_tour.Show();
                }

            }
            nbtours--;

        }

        //public void Debuter_un_tour(object sender, EventArgs e)
        //{
        //    Commencer_tour.Close();
        //    Choixpersonnage_ferme = true;

        //}


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
        //test centrer l'appel à chaque tour
        private void Tour(object sender, EventArgs e)
        {
            if (!commencer_tour_ferme)
            {
                Commencer_tour.Close();
                commencer_tour_ferme = true;
            }
           
            
            if (tourfini)
            {
                Pioche.Enabled = true;
                Pieces.Enabled = true;
                Pouvoir.Enabled = true;
                Construction.Enabled = true;
                //ReplacerCartePioche();
                Form FormChoixTour = RetourneFormEnfantChoixTour();
                FormChoixTour.Hide();
            }
            tourfini = false;

            Appel_Personnage();
                
                MasqueCarteJoueurOppose();
                //if (!Personnage_Mort)
                //{
                    ChoixPendantLeTour();

            //}
        
           

        }


        //METHODE D'APPEL POUR CHAQUE PERSONNAGE 
        public void Appel_Personnage()
        {
            if (PersonnageQuiJoue == 8)
            {
                //ON ARRIVE A LA FIN DU TOUR, IL FAUT DONC COMPTER LES CARTES QUARTIERS SI ELLES NE SONT PAS AU NOMBRE DE HUIT, PROPOSE UN NOUVEAU CHOIX DE PERSONNAGE
            }

            bool PersonnageEstChoisi = false;
            


            while (!PersonnageEstChoisi || Personnage_Mort)
            {
                //MessageBox.Show("le numero de personnage qui joue " + PersonnageQuiJoue);
                Personnage_Mort = false;
                PersonnageQuiJoue++;

                foreach (Control CartePersonnageChoisie in tabPage1.Controls)
                {


                    if (PersonnageQuiJoue == Convert.ToInt16(CartePersonnageChoisie.Tag))
                    {
                        if(PersonnageQuiJoue == 2)
                        {
                            JoueurVoleur = 1;
                        }
                        String Perso = Nom_personnage(Convert.ToInt16(CartePersonnageChoisie.Tag));
                        PersonnageEstChoisi = true;
                        Numero_Joueur_Tour = 1; //on sait du coup que c'est le joueur 1 qui a la carte est donc peut ensuite disabled le numero du joueur en face
                        MessageBox.Show("C'est au tour " + Perso + ". " + nom1 + " c'est à toi de jouer ! ", "A qui le tour ?");

                        if (PersonnageEstChoisi && PersonnageQuiJoue == Personnage_Assassine)
                        {
                            Personnage_Mort = true;
                            MessageBox.Show("Tu ne peux pas jouer car tu as été tué", "La vie est parfois cruelle...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }else if(PersonnageEstChoisi && Personnage_Vole == PersonnageQuiJoue) // dans le esle comme ça si le personnage n'est pas assassine, on teste si c'est celui qui est volé, et si il a été assasiné, il peut pas être volé donc ne fera pas le test
                        {
                            MessageBox.Show("Tu as été volé... Tu perds tout ton trésor...", "Quel dommage !", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            int pieceappartenantaujoueurvole = 0;
                            if (JoueurVoleur == 1)
                            {
                                pieceappartenantaujoueurvole = Convert.ToInt32(PiecesJ2.Text);
                                PiecesJ2.Text = "0";
                            }
                            else
                            {
                                pieceappartenantaujoueurvole = Convert.ToInt32(PiecesJ1.Text);
                                PiecesJ1.Text = "0";
                            }

                            int pieceduvoleur = Convert.ToInt32(PiecesJ2.Text);

                            pieceduvoleur += pieceappartenantaujoueurvole;
                            PiecesJ2.Text = Convert.ToString(pieceduvoleur);



                            //int piecePourLeVoleur = Convert.ToInt32(PiecesJ2.Text);

                            //MessageBox.Show("voici ce que vaut piece pour voleur " + piecePourLeVoleur);
                            //PiecesJ1.Text = "0";

                            //    int piecejoueurvoleur = Convert.ToInt32(PiecesJ2.Text);
                            //    MessageBox.Show("voici ce que vaut pice pour piecejoueurvoleur " + piecejoueurvoleur);
                            //    piecejoueurvoleur += piecePourLeVoleur;
                            //    PiecesJ2.Text = Convert.ToString(piecejoueurvoleur);

                        }

                    }
                }

                foreach (Control CartePersonnageChoisie in tabPage3.Controls)
                {
                    if (PersonnageQuiJoue == Convert.ToInt16(CartePersonnageChoisie.Tag))
                    {
                        if (PersonnageQuiJoue == 2)
                        {
                            JoueurVoleur = 2;
                        }
                        String Perso = Nom_personnage(Convert.ToInt16(CartePersonnageChoisie.Tag));
                        PersonnageEstChoisi = true;
                        Numero_Joueur_Tour = 2; //on sait du coup que c'est le joueur 2 qui a la carte est donc peut ensuite disabled le numero du joueur en face
                        MessageBox.Show("C'est au tour " + Perso + ". " + nom2 + " c'est à toi de jouer ! ", "A qui le tour ?");

                        if (PersonnageEstChoisi && PersonnageQuiJoue == Personnage_Assassine)
                        {
                            Personnage_Mort = true;
                            MessageBox.Show("Tu ne peux pas jouer car tu as été tué", "La vie est parfois cruelle...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (PersonnageEstChoisi && Personnage_Vole == PersonnageQuiJoue) // dans le esle comme ça si le personnage n'est pas assassine, on teste si c'est celui qui est volé, et si il a été assasiné, il peut pas être volé donc ne fera pas le test
                        {
                            MessageBox.Show("Tu as été volé... Tu perds tout ton trésor...", "Quel dommage !", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            int pieceappartenantaujoueurvole = 0;
                            if (JoueurVoleur == 1)
                            {
                                pieceappartenantaujoueurvole = Convert.ToInt32(PiecesJ2.Text);
                                PiecesJ2.Text = "0";
                            }
                            else
                            {
                                pieceappartenantaujoueurvole = Convert.ToInt32(PiecesJ1.Text);
                                PiecesJ1.Text = "0";
                            }

                            int pieceduvoleur = Convert.ToInt32(PiecesJ1.Text);

                            pieceduvoleur += pieceappartenantaujoueurvole;
                            PiecesJ1.Text = Convert.ToString(pieceduvoleur);


                            ////J'AI PRIS EN COMPTE QUE LE VOLEUR POUVAIT ETRE BETE ET SE VOLE TOUT SEUL !!
                            //int piecePourLeVoleur = Convert.ToInt32(PiecesJ1.Text);
                            //MessageBox.Show("voici ce que vaut piece pour voleur " + piecePourLeVoleur);
                            //PiecesJ1.Text = "0";

                            //    int piecejoueurvoleur = Convert.ToInt32(PiecesJ2.Text);
                            //    MessageBox.Show("voici ce que vaut pice pour piecejoueurvoleur " + piecejoueurvoleur);
                            //    piecejoueurvoleur += piecePourLeVoleur;
                            //    PiecesJ2.Text = Convert.ToString(piecejoueurvoleur);

                        }

                    }
                }
            }
            //MasqueCarteJoueurOppose();
            //ChoixPendantLeTour();

        }

        //Mis à part comme ça peut appeler à chaque tour
        public void ChoixPendantLeTour()
        {
            //FormChoixTour();

            //TESTER QUE LA FORM EXISTE 
            // SI N EXISTE PAS, CREE 
            //SINON NE FAIS RIEN 
            tourfini = true;
            if (! FormTourCree)
            { 
                ChoixTour.Tag = "ChoixTour";
                ChoixTour.MdiParent = this;
                FormTourCree = true;

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
                Fin.Click += Tour;

                //RECUPERE POUR LES BOUTONS PIOCHE ET PIECE LE RANG DANS LES CONTROLS 
                rang_bouton_piece = ChoixTour.Controls.IndexOf(Pieces);
                rang_bouton_pioche = ChoixTour.Controls.IndexOf(Pioche);

                Pieces.Click += UneAction;
                Pioche.Click += UneAction;
                Pouvoir.Click += UneAction;
                Construction.Click += UneAction;
            }
            


            //Button Pouvoir = new Button();
            //Button Pioche = new Button(); // EN VARAIBLE GLOBAL 
            //Button Pieces = new Button();
            //Button Construction = new Button();
            

            //ChoixTour.StartPosition = FormStartPosition.CenterScreen;
            ChoixTour.StartPosition = FormStartPosition.Manual;
            ChoixTour.Left = 1000;
            ChoixTour.Top = 50;
            ChoixTour.TopMost = true;
            ChoixTour.Show();
           // ChoixTour.BringToFront();
            //voir pour rajouter un bouton fin de partie + test si personne ferme la fenêtre, mettre un message "oups tu passes ton tour" si oui,passe au joueur suivant, si non, lui affiche à nouveau la form 
        }

        private Form RetourneFormEnfantChoixTour()
        {
            Form FormRetour = new Form();
            foreach (Form FormEnfant in this.MdiChildren)
            {
                if ((String)FormEnfant.Tag == "ChoixTour")
                {
                   FormRetour = FormEnfant;
                }
            }
            return FormRetour;
        }

        private Form RetourneFormEnfantChoixAssassin()
        {
            Form FormRetour = new Form();
            foreach (Form FormEnfant in this.MdiChildren)
            {
                if ((String)FormEnfant.Tag == "ChoixAssassin")
                {
                    FormRetour = FormEnfant;
                }
            }
            return FormRetour;
        }

        private Form RetourneFormEnfantChoixVoleur()
        {
            Form FormRetour = new Form();
            foreach (Form FormEnfant in this.MdiChildren)
            {
                if ((String)FormEnfant.Tag == "ChoixVoleur")
                {
                    FormRetour = FormEnfant;
                }
            }
            return FormRetour;
        }

        
        private Form RetourneFormEnfantChoixTypeMagicien()
        {
            Form FormRetour = new Form();
            foreach (Form FormEnfant in this.MdiChildren)
            {
                if ((String)FormEnfant.Tag == "ChoixTypeMagicien")
                {
                    FormRetour = FormEnfant;
                }
            }
            return FormRetour;
        }

        
        private Form RetourneFormEnfantChoixMagicien()
        {
            Form FormRetour = new Form();
            foreach (Form FormEnfant in this.MdiChildren)
            {
                if ((String)FormEnfant.Tag == "ChoixMagicien")
                {
                    FormRetour = FormEnfant;
                }
            }
            return FormRetour;
        }


        //private void FinTourJoueur(object sender, EventArgs e)
        //{
        //    Pioche.Enabled = true;
        //    Pieces.Enabled = true;
        //    Pouvoir.Enabled = true;
        //    Construction.Enabled = true;
        //    //ReplacerCartePioche();
        //    Form FormChoixTour = RetourneFormEnfantChoixTour();
        //    FormChoixTour.Hide();

        //    //this.ChoixTour.Close();
        //    //Appel_Personnage();
        //    //ChoixPendantLeTour();
        //    //Tour();
            


        //}

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
                
                int nbQuartier = 0;

                switch (PersonnageQuiJoue)
                {
                    case 1: //ASSASSIN

                        FormPouvoirAssassin();

                        break;

                    case 2: // VOLEUR
                        FormPouvoirVoleur();
                        break;

                    case 3: // MAGICIEN
                        FormTypeEchangeMagicien();
                        //FormPouvoirMagicien();
                        break;

                    case 4: //ROI
                        if (Numero_Joueur_Tour == 1)
                        {
                            RoiJoueur1.Visible = true;
                            foreach (PictureBox carteQuartier in citeJ1.Controls)
                            {
                                if (carteQuartier.BackColor == Color.Yellow)
                                {
                                    nbQuartier++;
                                }
                            }


                            if (nbQuartier != 0)
                            {
                                MessageBox.Show("Tu as " + nbQuartier + " quartier(s) noble(s), cela te rapporte donc " + nbQuartier + " pièce(s)", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            RoiJoueur2.Visible = true;
                            foreach (PictureBox carteQuartier in citeJ2.Controls)
                            {
                                if (carteQuartier.BackColor == Color.Yellow)
                                {
                                    nbQuartier++;
                                }
                            }


                            if (nbQuartier != 0)
                            {
                                MessageBox.Show("Tu as " + nbQuartier + " quartier(s) noble(s), cela te rapporte donc " + nbQuartier + " pièce(s)", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                if (carteQuartier.BackColor == Color.Blue)
                                {
                                    nbQuartier++;
                                }
                            }


                            if (nbQuartier != 0)
                            {
                                MessageBox.Show("Tu as " + nbQuartier + " quartier(s) religieux, cela te rapporte donc " + nbQuartier + " pièce(s)s", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                if (carteQuartier.BackColor == Color.Blue)
                                {
                                    nbQuartier++;
                                }
                            }


                            if (nbQuartier != 0)
                            {
                                MessageBox.Show("Tu as " + nbQuartier + " quartier(s) religieux, cela te rapporte donc " + nbQuartier + " pièce(s)", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                if (carteQuartier.BackColor == Color.Green)
                                {
                                    nbQuartier++;
                                }
                            }


                            if (nbQuartier != 0)
                            {
                                MessageBox.Show("Tu as " + nbQuartier + " quartier(s) commerçant(s), cela te rapporte donc " + nbQuartier + " pièce(s)", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                if (carteQuartier.BackColor == Color.Green)
                                {
                                    nbQuartier++;
                                }
                            }


                            if (nbQuartier != 0)
                            {
                                MessageBox.Show("Tu as " + nbQuartier + " quartier(s) commerçant(s), cela te rapporte donc " + nbQuartier + " pièce(s)", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                int nbPieceJoueur = Convert.ToInt32(PiecesJ2.Text);
                                nbPieceJoueur += nbQuartier;
                                PiecesJ2.Text = Convert.ToString(nbPieceJoueur);
                            }


                        }


                        break;

                    case 7:// ARCHITECTE

                        if (Numero_Joueur_Tour == 1)
                        {

                            int NumCarte1 = Generation_nb_Aleatoire();
                            int NumCarte2 = Generation_nb_Aleatoire();


                            //PROPOSE DEUX CARTES DANS LA PIOCHE 
                            Carte1 = (PictureBox)pioche.Controls[NumCarte1];
                            tabPage2.Controls.Add(Carte1);

                            int nbcarte = Compte_carte(tabPage2);
                            if (nbcarte < 4)
                            {
                                Carte1.Top = nbcarte * 150;
                                Carte1.Left = 0;
                            }
                            else if (nbcarte >= 4)
                            {
                                Carte1.Top = (nbcarte - 4) * 150;
                                Carte1.Left = 90;
                            }


                            Carte2 = (PictureBox)pioche.Controls[NumCarte1];
                            tabPage2.Controls.Add(Carte2);

                            nbcarte = Compte_carte(tabPage2);
                            if (nbcarte < 4)
                            {
                                Carte2.Top = nbcarte * 150;
                                Carte2.Left = 0;
                            }
                            else if (nbcarte >= 4)
                            {
                                Carte2.Top = (nbcarte - 4) * 150;
                                Carte2.Left = 90;
                            }

                        }
                        else if (Numero_Joueur_Tour == 2)
                        {
                            int NumCarte1 = Generation_nb_Aleatoire();
                            int NumCarte2 = Generation_nb_Aleatoire();


                            //PROPOSE DEUX CARTES DANS LA PIOCHE 
                            Carte1 = (PictureBox)pioche.Controls[NumCarte1];
                            tabPage4.Controls.Add(Carte1);

                            int nbcarte = Compte_carte(tabPage4);
                            if (nbcarte < 4)
                            {
                                Carte1.Top = nbcarte * 150;
                                Carte1.Left = 0;
                            }
                            else if (nbcarte >= 4)
                            {
                                Carte1.Top = (nbcarte - 4) * 150;
                                Carte1.Left = 90;
                            }


                            Carte2 = (PictureBox)pioche.Controls[NumCarte1];
                            tabPage4.Controls.Add(Carte2);

                            nbcarte = Compte_carte(tabPage4);
                            if (nbcarte < 4)
                            {
                                Carte2.Top = nbcarte * 150;
                                Carte2.Left = 0;
                            }
                            else if (nbcarte >= 4)
                            {
                                Carte2.Top = (nbcarte - 4) * 150;
                                Carte2.Left = 90;
                            }
                        }

                  
                        break;

                    case 8: // CONDITTIERE
                        Form FormChoix = RetourneFormEnfantChoixTour();
                        FormChoix.WindowState = FormWindowState.Minimized;

                        if (Numero_Joueur_Tour == 1)
                                        {
                                            foreach (PictureBox carteQuartier in citeJ1.Controls)
                                            {
                                                if (carteQuartier.BackColor == Color.Red)
                                                {
                                                    nbQuartier++;
                                                }
                                            }


                                            if (nbQuartier != 0)
                                            {
                                                MessageBox.Show("Tu as " + nbQuartier + " quartier(s) militaire(s), cela te rapporte donc " + nbQuartier + " pièce(s)", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                                if (carteQuartier.BackColor == Color.Red)
                                                {
                                                    nbQuartier++;
                                                }
                                            }


                                            if (nbQuartier != 0)
                                            {
                                                MessageBox.Show("Tu as " + nbQuartier + " quartier(s) militaire(s), cela te rapporte donc " + nbQuartier + " pièce(s)", "Touche le pactole !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                int nbPieceJoueur = Convert.ToInt32(PiecesJ2.Text);
                                                nbPieceJoueur += nbQuartier;
                                                PiecesJ2.Text = Convert.ToString(nbPieceJoueur);
                                            }
                                        }

                        //Form choixQuartierDestruction = new Form();
                        //METTRE UNE IMAGE GENRE EPEE OU UN ELEMENT QUI REPRESENTE LA DESTRUCTION ET JOEUUR POURRA DRAG DROP LE QUARTIER VOULU POUR LE DETRUIRE 
                       
                        panelDemolition.Visible = true;
                        panelDemolition.AllowDrop = true;
                        Texte_demolition.Visible = true;
                        
                        MessageBox.Show("Choisi le quartier que tu souhaites détruire, et glisses le sur la boule de démolition", "Fais ton choix !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        

                        break;
                }

                boutonClique.Enabled = false;
            }
            else if (tag_bouton == "construction")
            {
                CliqueConstrruction++;

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
                    afficheCarteQuartier.Width = 800;
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
                       
                        tousLesControls(tabPage2, ListeDesQuartiers);

                        foreach (PictureBox carte in ListeDesQuartiers)
                        {
                            PictureBox copiecarte = new PictureBox();
                            copiecarte.Image = carte.Image;
                            copiecarte.Text = carte.Text;
                            copiecarte.Tag = carte.Tag;
                            copiecarte.BackColor = carte.BackColor;
                            copiecarte.Size = carte.Size;
                            copiecarte.SizeMode = carte.SizeMode;

                            quartiersJ1.Add(copiecarte);
                        }

                        //quartiersJ1 = ListeDesQuartiers;
                        foreach (PictureBox carteQuartierJoueur in ListeDesQuartiers)
                        {
                            PictureBox Quartier = new PictureBox();
                            Quartier.Image = carteQuartierJoueur.Image;
                            afficheCarteQuartier.Controls.Add(Quartier);
                            Quartier.Top = 30;
                            Quartier.Left = 30 + (100 * i);
                            i++;
                            Quartier.Click += ChoixConstructionQuartier;
                        }

                    }
                        else if (Numero_Joueur_Tour == 2)
                        {
                        int test = tabPage4.Controls.Count;
                        MessageBox.Show("le nombre de controles dans les mains du joeur 2 :" + test);
                          // CREER UNE FONCTION CAR BUG AVEC  FOREACH LOOP // LES CONTROLS N ETAIENT PAS TROUVE 
                        tousLesControls(tabPage4, ListeDesQuartiers);

                        

                        foreach (PictureBox carte in ListeDesQuartiers)
                        {
                            PictureBox copiecarte = new PictureBox();
                            copiecarte.Image = carte.Image;
                            copiecarte.Text = carte.Text;
                            copiecarte.Tag = carte.Tag;
                            copiecarte.BackColor = carte.BackColor;
                            copiecarte.Size = carte.Size;
                            copiecarte.SizeMode = carte.SizeMode;

                            quartiersJ2.Add(copiecarte);
                        }

                        //quartiersJ1 = ListeDesQuartiers;


                        foreach (PictureBox carteQuartierJoueur in ListeDesQuartiers)
                        {
                                PictureBox Quartier = new PictureBox();
                                Quartier = carteQuartierJoueur;
                                afficheCarteQuartier.Controls.Add(Quartier);
                                Quartier.Top = 30;
                                Quartier.Left = 30 + (100 * i);
                                i++;
                                Quartier.Click += ChoixConstructionQuartier;
                                
                            }
                        }
                        afficheCarteQuartier.Show();
                    }
                    else
                    {
                        MessageBox.Show("Malheureusement, tu n'as pas de cartes Quartiers");
                    }


                    // /!\ VOIR SI PAS DISABLED DIRECTEMENT LE BOUTON EN FONCTION DU NOMBRE DE PIECES DISPONIBLES ET EN FONCTION DES PIECES SUR LES TAG DES CARTES QUARTIES 
                    if(PersonnageQuiJoue != 7)
                {
                    boutonClique.Enabled = false;
                }
                else
                {
                    if(CliqueConstrruction == 3)
                    {
                        boutonClique.Enabled = false;
                    }
                }
                    
                }

        }

        private int Compte_carte(Control onglet)
        {
            int nbcartes = 0;

            foreach(Control carte in onglet.Controls)
            {
                nbcartes++;
            }

            return nbcartes;
        }

        private void FormTypeEchangeMagicien()
        {
            Form ChoixTypeEchangeMagicien = new Form();
            ChoixTypeEchangeMagicien.Tag = "ChoixTypeMagicien";
            ChoixTypeEchangeMagicien.MdiParent = this;


            Label titre_form_echange_magicien = new Label();
            titre_form_echange_magicien.Text = "Quelle échange veux-tu effectuer ? ";
            titre_form_echange_magicien.Width = 300;
            ChoixTypeEchangeMagicien.Controls.Add(titre_form_echange_magicien);

            Button EchangePersonnage = new Button();
            ChoixTypeEchangeMagicien.Controls.Add(EchangePersonnage);
            EchangePersonnage.Text = "Echange avec un personnage";
            EchangePersonnage.Tag = "Personnage";
            EchangePersonnage.Width = 200;
            EchangePersonnage.Height = 80;
            EchangePersonnage.Left = 50;
            EchangePersonnage.Top = 50;
            Button EchangePioche = new Button();
            ChoixTypeEchangeMagicien.Controls.Add(EchangePioche);
            EchangePioche.Tag = "Pioche";
            EchangePioche.Text = "Echange avec des cartes de la pioche";
            EchangePioche.Width = 200;
            EchangePioche.Height = 80;
            EchangePioche.Left = 50;
            EchangePioche.Top = 150;

            EchangePersonnage.Click += FormPouvoirMagicien;
            EchangePioche.Click += FormPouvoirMagicien;

            ChoixTypeEchangeMagicien.Width = 300;
            ChoixTypeEchangeMagicien.Height = 300;
            ChoixTypeEchangeMagicien.StartPosition = FormStartPosition.Manual;
            ChoixTypeEchangeMagicien.Left = 1000;
            ChoixTypeEchangeMagicien.Top = 50;
            //MINIMIZE LA FOMR CHOIX PENDANT LA PARTIE 
            Form FormTour = RetourneFormEnfantChoixTour();
            FormTour.WindowState = FormWindowState.Minimized;
            ChoixTypeEchangeMagicien.Show();


        }


        private void FormPouvoirMagicien(object sender, EventArgs e)
        {
            Form formTypeMagicien = RetourneFormEnfantChoixTypeMagicien();
            formTypeMagicien.Close();
            Button ChoixTypeEchange = (Button)sender;
            if((String)ChoixTypeEchange.Tag == "Personnage")
            {

            // A CHANGER NOM DE LA FORM, TEXT LABEL, METHODE CLICK // IMPOSSIBLE DE FAIRE UNE METHODE GLOBALE POUR L'ensemble des personnages car il n'est pas possible en c# de mettre un string comme nom de variable 


            Form ChoixPersonnageMagicien = new Form();
            ChoixPersonnageMagicien.Tag = "ChoixMagicien";
            ChoixPersonnageMagicien.MdiParent = this;

            Label titre_form_assassin = new Label();
            titre_form_assassin.Text = "Quel personnage veux-tu voler ? ";
            titre_form_assassin.Width = 300;
            ChoixPersonnageMagicien.Controls.Add(titre_form_assassin);

            Button voleur = new Button();
            voleur.Text = "Voleur";
            voleur.Tag = 2;
            ChoixPersonnageMagicien.Controls.Add(voleur);
            Button magicien = new Button();
            magicien.Text = "Magicien";
            magicien.Tag = 3;
            ChoixPersonnageMagicien.Controls.Add(magicien);
            Button roi = new Button();
            roi.Text = "Roi";
            roi.Tag = 4;
            ChoixPersonnageMagicien.Controls.Add(roi);
            Button eveque = new Button();
            eveque.Text = "Evèque";
            eveque.Tag = 5;
            ChoixPersonnageMagicien.Controls.Add(eveque);
            Button marchand = new Button();
            marchand.Text = "Marchand";
            marchand.Tag = 6;
            ChoixPersonnageMagicien.Controls.Add(marchand);
            Button architecte = new Button();
            architecte.Text = "Architecte";
            architecte.Tag = 7;
            ChoixPersonnageMagicien.Controls.Add(architecte);
            Button condottiere = new Button();
            condottiere.Text = "Condottière";
            condottiere.Tag = 8;
            ChoixPersonnageMagicien.Controls.Add(condottiere);

            voleur.Click += ChoixPersonnageEchangeMagicien; //A CHANGER !!!
            magicien.Click += ChoixPersonnageEchangeMagicien;
            roi.Click += ChoixPersonnageEchangeMagicien;
            eveque.Click += ChoixPersonnageEchangeMagicien;
            marchand.Click += ChoixPersonnageEchangeMagicien;
            architecte.Click += ChoixPersonnageEchangeMagicien;
            condottiere.Click += ChoixPersonnageEchangeMagicien;

            voleur.Width = 200;
            voleur.Height = 50;
            voleur.Left = 50;
            voleur.Top = 50;

            magicien.Width = 200;
            magicien.Height = 50;
            magicien.Left = 50;
            magicien.Top = 50 + (50 * 1);

            roi.Width = 200;
            roi.Height = 50;
            roi.Left = 50;
            roi.Top = 50 + (50 * 2);

            eveque.Width = 200;
            eveque.Height = 50;
            eveque.Left = 50;
            eveque.Top = 50 + (50 * 3);

            marchand.Width = 200;
            marchand.Height = 50;
            marchand.Left = 50;
            marchand.Top = 50 + (50 * 4);

            architecte.Width = 200;
            architecte.Height = 50;
            architecte.Left = 50;
            architecte.Top = 50 + (50 * 5);

            condottiere.Width = 200;
            condottiere.Height = 50;
            condottiere.Left = 50;
            condottiere.Top = 50 + (50 * 6);


            ChoixPersonnageMagicien.Width = 300;
            ChoixPersonnageMagicien.Height = 600;

            ChoixPersonnageMagicien.StartPosition = FormStartPosition.Manual;
            ChoixPersonnageMagicien.Left = 1000;
            ChoixPersonnageMagicien.Top = 50;
            //MINIMIZE LA FOMR CHOIX PENDANT LA PARTIE 
            Form FormTour = RetourneFormEnfantChoixTour();
            FormTour.WindowState = FormWindowState.Minimized;
            ChoixPersonnageMagicien.Show();
            }else if ( (String)ChoixTypeEchange.Tag == "Pioche")
            {
                if (Numero_Joueur_Tour == 1)
                {
                    int nb_cartes_joueur = tabPage2.Controls.Count;
                    tabPage2.Controls.Clear();

                    for (int i = 1; i < nb_cartes_joueur; i++)
                    {
                        int NumCarte = Generation_nb_Aleatoire();

                        PictureBox Carte = (PictureBox)pioche.Controls[NumCarte];
                        tabPage2.Controls.Add(Carte);
                        Carte.Top = 40 * i;

                    }

                }
                else if (Numero_Joueur_Tour == 2)
                {
                    int nb_cartes_joueur = tabPage4.Controls.Count;
                    tabPage4.Controls.Clear();

                    for (int i = 1; i < nb_cartes_joueur; i++)
                    {
                        int NumCarte = Generation_nb_Aleatoire();

                        PictureBox Carte = (PictureBox)pioche.Controls[NumCarte];
                        tabPage4.Controls.Add(Carte);
                        Carte.Top = 40 * i;

                    }

                }
                MessageBox.Show("Tes cartes ont été échangées avec la pioche", "Et hop un tour de magie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form FormTour = RetourneFormEnfantChoixTour();
                FormTour.WindowState = FormWindowState.Normal;

            }

        }



        private void FormPouvoirVoleur()
        {

            // A CHANGER NOM DE LA FORM, TEXT LABEL, METHODE CLICK // IMPOSSIBLE DE FAIRE UNE METHODE GLOBALE POUR L'ensemble des personnages car il n'est pas possible en c# de mettre un string comme nom de variable 


            Form ChoixPersonnageVoleur = new Form();
            ChoixPersonnageVoleur.Tag = "ChoixVoleur";
            ChoixPersonnageVoleur.MdiParent = this;

            Label titre_form_assassin = new Label();
            titre_form_assassin.Text = "Quel personnage veux-tu voler ? ";
            titre_form_assassin.Width = 300;
            ChoixPersonnageVoleur.Controls.Add(titre_form_assassin);

            Button voleur = new Button();
            voleur.Text = "Voleur";
            voleur.Tag = 2;
            ChoixPersonnageVoleur.Controls.Add(voleur);
            Button magicien = new Button();
            magicien.Text = "Magicien";
            magicien.Tag = 3;
            ChoixPersonnageVoleur.Controls.Add(magicien);
            Button roi = new Button();
            roi.Text = "Roi";
            roi.Tag = 4;
            ChoixPersonnageVoleur.Controls.Add(roi);
            Button eveque = new Button();
            eveque.Text = "Evèque";
            eveque.Tag = 5;
            ChoixPersonnageVoleur.Controls.Add(eveque);
            Button marchand = new Button();
            marchand.Text = "Marchand";
            marchand.Tag = 6;
            ChoixPersonnageVoleur.Controls.Add(marchand);
            Button architecte = new Button();
            architecte.Text = "Architecte";
            architecte.Tag = 7;
            ChoixPersonnageVoleur.Controls.Add(architecte);
            Button condottiere = new Button();
            condottiere.Text = "Condottière";
            condottiere.Tag = 8;
            ChoixPersonnageVoleur.Controls.Add(condottiere);

            voleur.Click += ChoixPersonnageVol; 
            magicien.Click += ChoixPersonnageVol;
            roi.Click += ChoixPersonnageVol;
            eveque.Click += ChoixPersonnageVol;
            marchand.Click += ChoixPersonnageVol;
            architecte.Click += ChoixPersonnageVol;
            condottiere.Click += ChoixPersonnageVol;

            voleur.Width = 200;
            voleur.Height = 50;
            voleur.Left = 50;
            voleur.Top = 50;

            magicien.Width = 200;
            magicien.Height = 50;
            magicien.Left = 50;
            magicien.Top = 50 + (50 * 1);

            roi.Width = 200;
            roi.Height = 50;
            roi.Left = 50;
            roi.Top = 50 + (50 * 2);

            eveque.Width = 200;
            eveque.Height = 50;
            eveque.Left = 50;
            eveque.Top = 50 + (50 * 3);

            marchand.Width = 200;
            marchand.Height = 50;
            marchand.Left = 50;
            marchand.Top = 50 + (50 * 4);

            architecte.Width = 200;
            architecte.Height = 50;
            architecte.Left = 50;
            architecte.Top = 50 + (50 * 5);

            condottiere.Width = 200;
            condottiere.Height = 50;
            condottiere.Left = 50;
            condottiere.Top = 50 + (50 * 6);


            ChoixPersonnageVoleur.Width = 300;
            ChoixPersonnageVoleur.Height = 600;

            ChoixPersonnageVoleur.StartPosition = FormStartPosition.Manual;
            ChoixPersonnageVoleur.Left = 1000;
            ChoixPersonnageVoleur.Top = 50;
            //MINIMIZE LA FOMR CHOIX PENDANT LA PARTIE 
            Form FormTour = RetourneFormEnfantChoixTour();
            FormTour.WindowState = FormWindowState.Minimized;
            ChoixPersonnageVoleur.Show();
        }




        private void FormPouvoirAssassin()
        {

            // A CHANGER NOM DE LA FORM, TEXT LABEL, METHODE CLICK // IMPOSSIBLE DE FAIRE UNE METHODE GLOBALE POUR L'ensemble des personnages car il n'est pas possible en c# de mettre un string comme nom de variable 


            Form ChoixPersonnageAssassin = new Form();
            ChoixPersonnageAssassin.Tag = "ChoixAssassin";
            ChoixPersonnageAssassin.MdiParent = this;

            Label titre_form_assassin = new Label();
            titre_form_assassin.Text = "Quel personnage veux-tu assassiner ? ";
            titre_form_assassin.Width = 300;
            ChoixPersonnageAssassin.Controls.Add(titre_form_assassin);

            Button voleur = new Button();
            voleur.Text = "Voleur";
            voleur.Tag = 2;
            ChoixPersonnageAssassin.Controls.Add(voleur);
            Button magicien = new Button();
            magicien.Text = "Magicien";
            magicien.Tag = 3;
            ChoixPersonnageAssassin.Controls.Add(magicien);
            Button roi = new Button();
            roi.Text = "Roi";
            roi.Tag = 4;
            ChoixPersonnageAssassin.Controls.Add(roi);
            Button eveque = new Button();
            eveque.Text = "Evèque";
            eveque.Tag = 5;
            ChoixPersonnageAssassin.Controls.Add(eveque);
            Button marchand = new Button();
            marchand.Text = "Marchand";
            marchand.Tag = 6;
            ChoixPersonnageAssassin.Controls.Add(marchand);
            Button architecte = new Button();
            architecte.Text = "Architecte";
            architecte.Tag = 7;
            ChoixPersonnageAssassin.Controls.Add(architecte);
            Button condottiere = new Button();
            condottiere.Text = "Condottière";
            condottiere.Tag = 8;
            ChoixPersonnageAssassin.Controls.Add(condottiere);

            voleur.Click += ChoixPersonnageAssassine;
            magicien.Click += ChoixPersonnageAssassine;
            roi.Click += ChoixPersonnageAssassine;
            eveque.Click += ChoixPersonnageAssassine;
            marchand.Click += ChoixPersonnageAssassine;
            architecte.Click += ChoixPersonnageAssassine;
            condottiere.Click += ChoixPersonnageAssassine;

            voleur.Width = 200;
            voleur.Height = 50;
            voleur.Left = 50;
            voleur.Top = 50;

            magicien.Width = 200;
            magicien.Height = 50;
            magicien.Left = 50;
            magicien.Top = 50 + (50 * 1);

            roi.Width = 200;
            roi.Height = 50;
            roi.Left = 50;
            roi.Top = 50 + (50 * 2);

            eveque.Width = 200;
            eveque.Height = 50;
            eveque.Left = 50;
            eveque.Top = 50 + (50 * 3);

            marchand.Width = 200;
            marchand.Height = 50;
            marchand.Left = 50;
            marchand.Top = 50 + (50 * 4);

            architecte.Width = 200;
            architecte.Height = 50;
            architecte.Left = 50;
            architecte.Top = 50 + (50 * 5);

            condottiere.Width = 200;
            condottiere.Height = 50;
            condottiere.Left = 50;
            condottiere.Top = 50 + (50 * 6);


            ChoixPersonnageAssassin.Width = 300;
            ChoixPersonnageAssassin.Height = 600;

            ChoixPersonnageAssassin.StartPosition = FormStartPosition.Manual;
            ChoixPersonnageAssassin.Left = 1000;
            ChoixPersonnageAssassin.Top = 50;
            //MINIMIZE LA FOMR CHOIX PENDANT LA PARTIE 
            Form FormTour = RetourneFormEnfantChoixTour();
            FormTour.WindowState = FormWindowState.Minimized;
            ChoixPersonnageAssassin.Show();
        }


        private void ChoixPersonnageAssassine(object sender, EventArgs e)
        {
            Button PersonnageChosi = (Button)sender;
            Personnage_Assassine = Convert.ToInt32(PersonnageChosi.Tag);

            String nomperso = Nom_personnage(Personnage_Assassine);
            MessageBox.Show("Tu as mis fin aux jours"+nomperso+".", "Tu as fait un choix et ta sentence est irrévocable !", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Form formChoix = RetourneFormEnfantChoixAssassin();
            formChoix.Close();
            Form FormTour = RetourneFormEnfantChoixTour();
            FormTour.WindowState = FormWindowState.Normal;


        }

        private void ChoixPersonnageVol(object sender, EventArgs e)
        {
            Button PersonnageChosi = (Button)sender;
            Personnage_Vole = Convert.ToInt32(PersonnageChosi.Tag);

            String nomperso = Nom_personnage(Personnage_Vole);
            MessageBox.Show("Tu as pris le buttin" + nomperso + ".", "Tu as fait un choix et ta sentence est irrévocable !", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Form formChoix = RetourneFormEnfantChoixVoleur();
            formChoix.Close();
            Form FormTour = RetourneFormEnfantChoixTour();
            FormTour.WindowState = FormWindowState.Normal;
     
        }

        //lorsque le magicien a choisi d'échanger ses cartes avec un personnnage, et qu'il a choisi le personnage 
        private void ChoixPersonnageEchangeMagicien(object sender, EventArgs e)
        {
            Button PersonnageChosi = (Button)sender;
            int Personnage_Echange = Convert.ToInt32(PersonnageChosi.Tag);

            String nomperso = Nom_personnage(Personnage_Echange);
            MessageBox.Show("Tu as pris les cartes" + nomperso + ".", "Tu as fait un choix et ta sentence est irrévocable !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bool CartePersoChoisi = false;
            if(Numero_Joueur_Tour == 1)
            {

                
            foreach (PictureBox cartePersoJoueurOppose in tabPage3.Controls)
                {
                    int tagCarte = Convert.ToInt32(cartePersoJoueurOppose.Tag);
                    if(Personnage_Echange == tagCarte)
                    {
                        CartePersoChoisi = true;
                        //booleen pour tester que c'est joueur opppose qui a carte perso chois 
                    }


                }

                if (CartePersoChoisi)
                {
                    List<PictureBox> carteTabPage2 = new List<PictureBox>();
                    tousLesControls(tabPage2, carteTabPage2);

                    foreach (PictureBox carteMagicien in carteTabPage2)
                    {
                        panelAttente.Controls.Add(carteMagicien); //enregistre les cartes dans un panel en attente l'échange 
                    }


                    tabPage2.Controls.Clear();
                    int y = 0;

                    int nbPanel = tabPage4.Controls.Count;
                    MessageBox.Show("Il y a ce nb de control dans panel 4: " + nbPanel);

                    List<PictureBox> carteTabPage4 = new List<PictureBox>();  // CREER UNE FONCTION CAR BUG AVEC  FOREACH LOOP // LES CONTROLS N ETAIENT PAS TROUVE 
                    tousLesControls(tabPage4, carteTabPage4);

                    foreach (PictureBox carteAutreJoueur in carteTabPage4)
                    {
                        MessageBox.Show("Je fais un tour");
                        tabPage2.Controls.Add(carteAutreJoueur);  //met les cartes du joueur opposé dans panel du joueur magicien 
                        carteAutreJoueur.Top = 50 * y;
                        carteAutreJoueur.Left = 0;
                        y++;
                    }
                   
                    tabPage4.Controls.Clear();

                    int i = 0;

                    List<PictureBox> carteEnAttente = new List<PictureBox>();
                    tousLesControls(panelAttente, carteEnAttente);
                    foreach (PictureBox carteMagicienEnAttente in carteEnAttente)
                    {
                        tabPage4.Controls.Add(carteMagicienEnAttente);
                        carteMagicienEnAttente.Top = 50 * i;
                        carteMagicienEnAttente.Left = 0;
                        i++;
                    }

                    

                    int nbPanel1 =tabPage2.Controls.Count;
                    MessageBox.Show("Il y a ce nb de control dans tabpage2 : " + nbPanel1);
                }
               

            }else if(Numero_Joueur_Tour == 2)
            {

                foreach (PictureBox cartePersoJoueurOppose in tabPage1.Controls.OfType<PictureBox>())
                {
                    int tagCarte = Convert.ToInt32(cartePersoJoueurOppose.Tag);
                    if (Personnage_Echange == tagCarte)
                    {
                        CartePersoChoisi = true;
                        //booleen pour tester que c'est joueur opppose qui a carte perso chois 
                    }


                }

                if (CartePersoChoisi)
                {
                    List<PictureBox> carteTabPage4 = new List<PictureBox>();  // CREER UNE FONCTION CAR BUG AVEC  FOREACH LOOP // LES CONTROLS N ETAIENT PAS TROUVE 
                    tousLesControls(tabPage4, carteTabPage4);

                    foreach (PictureBox carteMagicien in carteTabPage4)
                    {
                        panelAttente.Controls.Add(carteMagicien); //enregistre les cartes dans un panel en attente l'échange 
                    }
                    tabPage4.Controls.Clear();
                    //MessageBox.Show("J'aim mis les cartes dans le panel d'attente");

                    int y = 0;
                    List<PictureBox> carteTabPage2 = new List<PictureBox>();
                    tousLesControls(tabPage2, carteTabPage2);

                    foreach (PictureBox carteAutreJoueur in carteTabPage2)
                    {
                        tabPage4.Controls.Add(carteAutreJoueur);  //met les cartes du joueur opposé dans panel du joueur magicien 
                        carteAutreJoueur.Top = 50 * y;
                        carteAutreJoueur.Left = 0;
                        y++;
                    }
                    tabPage2.Controls.Clear();
                    MessageBox.Show("J'aim mis les cartes dans le panel du joueur 1");

                    int nbPanel = panelAttente.Controls.Count;
                    MessageBox.Show("Il y a ce nb de control dans Panel : " + nbPanel);

                    int i = 0;
                    List<PictureBox> carteEnAttente = new List<PictureBox>();
                    tousLesControls(panelAttente, carteEnAttente);
                    foreach (PictureBox carteMagicienEnAttente in carteEnAttente)
                    {
                        tabPage2.Controls.Add(carteMagicienEnAttente);
                        carteMagicienEnAttente.Top = 50 * i;
                        carteMagicienEnAttente.Left = 0;
                        i++;

                    }
                    panelAttente.Controls.Clear();
                    //MessageBox.Show("J'aim mis les cartes dans le panel du joueur 2");
                }

            }
            //INVERSE CARTE DU VOLEUR ET DU VOLE 

            Form formChoix = RetourneFormEnfantChoixMagicien();
            formChoix.Close();
            Form FormTour = RetourneFormEnfantChoixTour();
            FormTour.WindowState = FormWindowState.Normal;

        }
        
        private void tousLesControls(Control PaquetCarte, List<PictureBox> toutesLesCartes)
        {
            
            foreach(PictureBox controle in PaquetCarte.Controls)
            {
                tousLesControls(controle, toutesLesCartes);
                toutesLesCartes.Add(controle);
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


            int index_carte = ListeDesQuartiers.IndexOf(QuartierSelectionné);
            MessageBox.Show("voici l'index : " + index_carte);

            if (nombre_de_pieces >= prixQuartierSelectionné)
            {
                int nbquartiercite = Compte_carte(PanelJoueur);
                QuartierSelectionné.MouseDown += carteCite_MouseDown;
                if (nbquartiercite <= 4)
                {
                    QuartierSelectionné.Top = nbquartiercite * 150;
                    QuartierSelectionné.Left = 0;
                }
                else
                {
                    QuartierSelectionné.Top = (nbquartiercite - 4) * 150;
                    QuartierSelectionné.Left = 90;

                }
                if(Numero_Joueur_Tour == 1)
                {
                    quartiersJ1.RemoveAt(index_carte);
                    
                }
                else
                {
                    quartiersJ2.RemoveAt(index_carte);
                }
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

                MessageBox.Show("Tu as construit un quartier dans ta cité", "Félicitations !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ListeDesQuartiers.Clear();

            }
            else
            {
                MessageBox.Show("Malheureusement, tu n'as pas suffisament de pièces pour construire ce quartier", "Gagne plus de pièces ! ");
            }

            ActiveForm.Close();

            Form essai = new Form();

            int nb = 0;

            if(Numero_Joueur_Tour == 1)
            {
                foreach (PictureBox carte in quartiersJ1)
                {
                    essai.Controls.Add(carte);
                    carte.Left = nb;
                    nb += 90;
                }
            }
            else
            {
                foreach (PictureBox carte in quartiersJ2)
                {
                    essai.Controls.Add(carte);
                    carte.Left = nb;
                    nb += 90;
                }
            }
           

            essai.Width = 800;
            essai.Show();

        }

        private void ChoixCartePioche(object sender, EventArgs e)
        {
            PictureBox CarteChoisie = (PictureBox)sender;
            int cartes = 0;
            if (Numero_Joueur_Tour == 1)
            {
                cartes = Compte_carte(tabPage2);
                if(cartes <= 4)
                {
                    CarteChoisie.Top = cartes * 150;
                    CarteChoisie.Left = 0;
                }
                else if(cartes > 4)
                {
                    CarteChoisie.Top = (cartes-4) * 150;
                    CarteChoisie.Left = 90;
                }
                
                tabPage2.Controls.Add(CarteChoisie);
            }
            else if (Numero_Joueur_Tour == 2)
            {
                cartes = Compte_carte(tabPage4);

                if (cartes <= 4)
                {
                    CarteChoisie.Top = cartes * 150;
                    CarteChoisie.Left = 0;
                }
                else if (cartes > 4)
                {
                    CarteChoisie.Top = (cartes - 4) * 150;
                    CarteChoisie.Left = 90;
                }

                
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

        // EVENEMENT MOUSE DOWN POUR PERMETTRE LE DRAG DROP SUR LES CARTES DANS LA CITE POUR LA DESTRUCTION PAR LE CONDOTTIERE
        private void carteCite_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox Carte = (PictureBox)sender;
            // RECUPERATION DE LA CARTE 
            CopieCarteDestruction = Carte;
            Carte.DoDragDrop("Hello", DragDropEffects.All);
            
        }

        private void Demolition_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        //ACTION LORSQUE QU'UNE CARTE EST SELECTIONNEE
        private void Demolition_DragDrop(object sender, DragEventArgs e)
        {
            bool JoueurOpposeEstEveque = false;

            if (Numero_Joueur_Tour == 1)
            {
                foreach (PictureBox CarteJoueurOppose in tabPage3.Controls) // VERIFIE QUE LE JOUEUR OPPOSE N'EST PAS EVEQUE CAR LE CONDOTTIERE NE PEUX DETRUIRE LES QUARTIERS D'UN EVEQUE
                {
                    if (CarteJoueurOppose.Name == "Eveque")
                    {
                        JoueurOpposeEstEveque = true;
                    }
                }


                if (citeJ1.Controls.Contains(CopieCarteDestruction))
                {
                    DialogResult reponse = MessageBox.Show("Tu es sur le point de détruire un de tes propres quarties. Es-tu sûr de ton coup ?", "Tu vas faire une erreur !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (reponse == DialogResult.Yes)
                    {
                        MessageBox.Show("J'ai répondu oui, je veux détruire mon propre quartier");
                        citeJ1.Controls.Remove(CopieCarteDestruction);

                    }
                    else if (reponse == DialogResult.No)
                    {
                        MessageBox.Show("J'ai répondu non, je ne veux pas détruire mon propre quartier");
                        citeJ1.Controls.Add(CopieCarteDestruction);
                        
                    }
                }
                else
                {
                    if (JoueurOpposeEstEveque)
                    {
                        MessageBox.Show("Ce quartier appartient à l'évèque, tu ne peux pas le détruire.", "Ne touche pas au quartier de l'évèque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        citeJ2.Controls.Add(CopieCarteDestruction);
                        
                    }
                    else
                    {
                       
                        int prixQuartier = Convert.ToInt32(CopieCarteDestruction.Tag);
                        
                        prixQuartier -= 1;
                        int piecesJoueur = Convert.ToInt32(PiecesJ1.Text);

                        if(piecesJoueur >= prixQuartier)
                        {
                            citeJ2.Controls.Remove(CopieCarteDestruction);
                            MessageBox.Show("Tu as détruit un quartier, cela t'as coûté " + prixQuartier, "Bravo Terminator !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Fin_Choix_Condottiere_Destruction();
                        }
                        else
                        {
                            MessageBox.Show("Tu n'as pas suffisament de pièces pour détruire ce quartier", "Oups ! Tu ne peux pas détruire ce quartier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                        
                    }
                }


            }
            else if (Numero_Joueur_Tour == 2)
            {
                foreach (PictureBox CarteJoueurOppose in tabPage1.Controls) // VERIFIE QUE LE JOUEUR OPPOSE N'EST PAS EVEQUE CAR LE CONDOTTIERE NE PEUX DETRUIRE LES QUARTIERS D'UN EVEQUE
                {
                    if (CarteJoueurOppose.Name == "Eveque")
                    {
                        JoueurOpposeEstEveque = true;
                    }
                }


                if (citeJ2.Controls.Contains(CopieCarteDestruction))
                {
                    DialogResult reponse = MessageBox.Show("Tu es sur le point de détruire un de tes propres quarties. Es-tu sûr de ton coup ?", "Tu vas faire une erreur !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (reponse == DialogResult.Yes)
                    {
                        MessageBox.Show("J'ai répondu oui, je veux détruire mon propre quartier");
                        citeJ2.Controls.Remove(CopieCarteDestruction);
                        
                    }
                    else if (reponse == DialogResult.No)
                    {
                        MessageBox.Show("J'ai répondu non, je ne veux pas détruire mon propre quartier");
                        citeJ2.Controls.Add(CopieCarteDestruction);
                        
                    }
                }
                else
                {
                    if (JoueurOpposeEstEveque)
                    {
                        MessageBox.Show("Ce quartier appartient à l'évèque, tu ne peux pas le détruire.", "Ne touche pas au quartier de l'évèque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        citeJ1.Controls.Add(CopieCarteDestruction);
                    }
                    else
                    {
                       
                        int prixQuartier = Convert.ToInt32(CopieCarteDestruction.Tag);
                        
                        prixQuartier -= 1;
                        int piecesJoueur = Convert.ToInt32(PiecesJ2.Text);

                        if (piecesJoueur >= prixQuartier)
                        {
                            citeJ1.Controls.Remove(CopieCarteDestruction);
                            MessageBox.Show("Tu as détruit un quartier, cela t'as coûté " + prixQuartier, "Bravo Terminator !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //Fin_Choix_Condottiere_Destruction();
                        }
                        else
                        {
                            MessageBox.Show("Tu n'as pas suffisament de pièces pour détruire ce quartier", "Oups ! Tu ne peux pas détruire ce quartier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                    }
                }

            }

            Fin_Choix_Condottiere_Destruction();
        }


        private void Fin_Choix_Condottiere_Destruction()
        {
            
            panelDemolition.AllowDrop = false; // ne permet plus de faire de détruire de quartier 
            panelDemolition.Visible = false;
            Texte_demolition.Visible = false;
            Form FormChoix = RetourneFormEnfantChoixTour();
            FormChoix.WindowState = FormWindowState.Normal;
         
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
