using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JeuMotMeles
{
    public  class Plateau
    {
        // Declaration de toutes les variables de classe
        private string[,] matrix;
        private int level;
        private string[] words;

        public Plateau () // Constructeur de la classe Plateau
        {
            // Toutes les variables sont initialisees à une valeur par défault
            this.level = 0;            
            this.matrix = null;
            this.words = null;
        }

        /// <summary>
        /// Function qui parcours la matrice du Plateau pour pouvoir afficher la matrice
        /// </summary>
        /// <returns> Toutes les lignes de la matrice du Plateau </returns>
        private string DisplayMatrix ()
        {
            string matrixLines = "\t";
            if (this.matrix != null) // Cas d'une matrice non null
            {
                for (int i = 0; i < this.matrix.GetLength(0); i++) // Parcours des lignes
                {
                    for (int j = 0; j < this.matrix.GetLength(1); j++) // Parcours des colonnes
                    {
                        matrixLines += this.matrix[i, j] + " "; // Mise en forme de la ligne
                    }
                    matrixLines += "\n\t"; // Retour à la ligne
                }
            }
            else // Cas d'une matrice null
            {
                matrixLines = "<NullMatrix : Class Plateau - DisplayMatrix";
            }

            return matrixLines;
        }

        /// <summary>
        /// Fonction qui parcours tous les mots à trouver pour pouvoir les affichers
        /// </summary>
        /// <returns> Tous les mots à trouver returns>
        private string DisplayWords ()
        {
            string tabWords = "";
            if (this.words != null) // Tableau des mots non null
            {
                for (int i = 0; i < this.words.Length; i++) // Parcours de la liste de mots
                {
                    tabWords += words[i] + " ";
                }
            }
            else // Tableau des mots null
            {
                tabWords = "<NullTab : Class Plateau - DisplayWords";
            }

            return tabWords;
        }

        /// <summary>
        /// Fonction qui retourne toutes les caractéristiques numériques de la matrice du Plateau
        /// </summary>
        /// <returns> niveau - mots à trouver - matrice </returns>
        public string ToString ()
        {
            return $"Niveau : {this.level} - Mots à trouver : {DisplayWords()}\nMatrice : \n{DisplayMatrix()}"; // Formatage des informations
        }

        public void ToFile (string nomFile) // TODO : Replace all the content of the nomFile.csv in the current directory by the actual content
        {
            //StreamWriter masterWriter = new StreamWriter(nomFile);

            // Le nombre d'informations minimum est de 4 sur la première ligne
            int maxCol = 4;
            int colMatrix = this.matrix.GetLength(1); // Nombre de colonnes de la matrice
            int lineMatrix = this.matrix.GetLength(0); // Nombre de lignes de la matrice
            int numWords = this.words.Length; // Nombre de mots a trouver

            int maxLine = 2 + lineMatrix; // Calcul du nombre de lignes
            string[] lines = new string[maxLine]; // Initialisation du tableau contenant toutes les lignes du futur fichier

            string lineContent; // Variable qui contiendra tous temporairement tous les elements d'une ligne

            // Calcul du maximum de colonnes pour pouvoir generer correctement les separateurs (;)
            if (maxCol < colMatrix)
            {
                maxCol = colMatrix;
            }
            if (maxCol < numWords)
            {
                maxCol = numWords;
            }

            for (int i = 0; i < maxLine; i++)
            {
                lineContent = "";
                for (int j = 0; j < maxCol; j++)
                {
                    if (i == 0)
                    {
                        // Ligne 1
                        if (j < 4)
                        {
                            switch (j)
                            {
                                case 0: // Niveau
                                    lineContent += $"{this.level}";
                                    break;
                                case 1: // Nombre de lignes de la matrice
                                    lineContent += $"{lineMatrix}";
                                    break;
                                case 2: // Nombre de colonnes de la matrice
                                    lineContent += $"{colMatrix}";
                                    break;
                                case 3: // Nombre de mots a trouver
                                    lineContent += $"{numWords}";
                                    break;
                            }
                        }
                    }
                    else if (i == 1)
                    {
                        // Ligne 2
                        if (j < numWords)
                        {
                            lineContent += this.words[j];
                        }
                    }
                    else
                    {
                        // Matrice
                        if (j < colMatrix)
                        {
                            lineContent += this.matrix[i - 2, j];
                        }
                    }

                    if (j != maxCol - 1) // Permet de ne pas avoir un ; en trop à chaque fin de ligne
                    {
                        lineContent += ";"; // Ajout du separateur (;) entre chaque element
                    }                    
                }
                //Console.WriteLine(lineContent);
                lines[i] = lineContent; // Remplissage du tableau contenant les lignes
            }

            //masterWriter.Close();
        }

        /// <summary>
        /// Fonction permettant de lire et interpréter un fichier de mots meles, et d'initialiser les variables locales
        /// </summary>
        /// <param name="nomfile"> Emplacement du fichier à lire </param>
        public void ToRead (string nomfile)
        {
            StreamReader masterReader = new StreamReader(nomfile); // Initialisation du StreamReader
            List<string> lines = new List<string>(); // Initilisation de la liste qui contiendra tous les elements du fichier

            // Lecture de tous les élements du fichier (ligne par ligne)
            string value = masterReader.ReadLine();
            while (value != null)
            {
                lines.Add(value); // Ajout de la ligne lue dans la liste precedente
                value = masterReader.ReadLine();
            }

            // Chaque element est seprare par un ;
            string[] tabLineInfo = lines[0].Split(";"); // Generation de la liste d'informations
            string[] tabLineWords = lines[1].Split(";"); // Generation de la liste de mots

            // Extraction des informations fixes 
            int level = Convert.ToInt32(tabLineInfo[0]);
            int nbLines = Convert.ToInt32(tabLineInfo[1]);
            int nbCol = Convert.ToInt32(tabLineInfo[2]);
            int nbWords = Convert.ToInt32(tabLineInfo[3]);

            string[] words = new string[nbWords]; // Initialisation du tableau des mots a trouver
            for (int i = 0; i < nbWords; i++) // Parcours de la liste des mots generee precedemment
            {
                words[i] = tabLineWords[i];
            }

            string[] tabValues = new string[nbCol]; // Initialisation d'un tableau qui va contenir tous les elements d'une ligne
            this.matrix = new string[nbLines, nbCol]; // Intialisation de la matrice du Plateau

            for (int j = 0; j < nbLines; j++)
            {
                tabValues = lines[j + 2].Split(";"); // Separation de tous les elements de chaque ligne
                for (int k = 0; k < nbCol; k++) // Parcours de tous les elements du precedent tableau
                {
                    this.matrix[j, k] = tabValues[k]; // Generation de la matrice du Plateau
                }
            }

            // Affectation des variables de classe
            this.level = level;
            this.words = tabLineWords;

            masterReader.Close();
        }

        /// <summary>
        /// Fonction qui teste si le mot passe en parametre, au depart des coordonnees donnees et dans la direction donnee peut rentrer
        /// </summary>
        /// <param name="mot"> Mot a tester </param>
        /// <param name="ligne"> Ligne de depart </param>
        /// <param name="colonne"> Colonne de départ </param>
        /// <param name="direction"> Direction dans laquelle on doit tester le mot </param>
        /// <returns> Le mot rentre est valide ou non a un endroit de la matrice et dans une direction donnee </returns>
        public bool Test_Plateau (string mot, int ligne, int colonne, string direction)
        {
            bool state = false;
            if (mot != "" && ligne >= 0 && colonne >= 0 && ligne < this.matrix.GetLength(0) && colonne < this.matrix.GetLength(1))
            {
                int lenWord = mot.Length - 1;

                int startLine = ligne;
                int startCol = colonne;
                int endLine = -1;
                int endCol = -1;
                switch (direction) // Conditions en fonction de la direction donnee
                {
                    case "N": // Vers le haut
                        endCol = startCol;
                        endLine = startLine - lenWord;
                        break;

                    case "S": // Vers le bas
                        endCol = startCol;
                        endLine = startLine + lenWord;
                        break;

                    case "E": // Vers la droite
                        endCol = startCol + lenWord;
                        endLine = startLine;
                        break;

                    case "O": // Vers la gauche
                        endCol = startCol - lenWord;
                        endLine = startLine;
                        break;

                    case "NE": // Vers en haut a droite
                        endCol = startCol + lenWord;
                        endLine = startLine - lenWord;
                        break;

                    case "SE": // Vers en bas a droite
                        endCol = startCol + lenWord;
                        endLine = startLine + lenWord;
                        break;

                    case "NO": // Vers en haut a gauche
                        endCol = startCol - lenWord;
                        endLine = startLine - lenWord;
                        break;

                    case "SO": // Vers en bas a gauche
                        endCol = startCol - lenWord;
                        endLine = startLine + lenWord;
                        break;

                    default: // En cas de mauvaise direction
                        Console.WriteLine("[!] Invalid direction");
                        break;
                }

                if (endLine >= 0 && endCol >= 0 && endLine < this.matrix.GetLength(0) && endCol < this.matrix.GetLength(1)) // Verification du depassement des limites de la matrice
                {
                    state = true;
                }
                //Console.WriteLine($"End coord : {endLine}, {endCol}");
            }
            return state;
        }

        /* MODE GENERATION AUTOMATIQUE
        public void GenerePlateau ()
        {

        }
        */
    }
}
