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
        

        public Form1()
        {
            InitializeComponent();
            
        }
        String nom1, nom2;
        private void Form1_Load(object sender, EventArgs e)
        {
            var Commencer_partie = new Form();
            Commencer_partie.StartPosition = FormStartPosition.CenterScreen;
            

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
            
            nom1 = nomjoueur1.Text;
            nom2 = nomjoueur2.Text;
            Commencer_partie.ShowDialog(this);

            tabPage1.Text = "Personnages";
            tabPage3.Text = "Personnages";
            tabPage2.Text = "Quartiers";
            tabPage4.Text = "Quartiers";

        }

        private void Validation_joueur(object sender, EventArgs e)
        {
            //string essai = nom1;
            MessageBox.Show("les noms sont : "+nom1+" et "+nom2);
        }

        

        private void reglesdujeu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string titre = "les règles du jeu";
            string regles = "on mettra une photo des règles du jeu ou on les écrira";
            MessageBox.Show(regles,titre, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
