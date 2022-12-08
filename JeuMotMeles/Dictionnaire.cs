using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JeuMotMeles
{
    public class Dictionnaire
    {
        private string[] dicoTab;
        private int[] lengthWords;
        private string lang;

        private void GetTabWords () // To Do : faire le compteur de mots par longueur avec la méthode Split ligne par ligne dans un compteur
        {
            StreamReader masterReader;
            string path = ""; // Initialisation du chemin d'accès du fichier considere (par default)

            if (this.lang == "FR")
            {
                path = Program.GetFilePath("MotsPossiblesFR.txt"); // Recuperation du chemin d'accès du fichier
            }
            else if (this.lang == "EN")
            {
                path = Program.GetFilePath("MotsPossiblesEN.txt"); // Recuperation du chemin d'accès du fichier
            }

            masterReader = new StreamReader(path); // Initialisation du StreamReader

            this.dicoTab = new string[14];
            this.lengthWords = new int[14];

            int line = 0; // Line lue actuellement
            int ranq = 0; // Rang de l'element du tableau (utilise pour l'ajout des elements dans le tableau)
            string valLength = "0"; // La valeur de longueur des mots par default est a 0
            string value = ""; // Variable stockant la ligne extraite du fichier considere

            while (value != null) // Recuperation des elements ligne par ligne
            {
                if (line % 2 == 0 && line != 0) // Toutes les 2 lignes, on a une nouvelle liste de mots
                {
                    this.dicoTab[ranq] = value; // On ajoute la ligne de mots dans le tableau
                    this.lengthWords[ranq] = Convert.ToInt32(valLength); // On ajoute la valeur de la taille des mots de la ligne dans le tableau
                    ranq += 1;
                }
                else
                {
                    valLength = value; // Recuperation de la valeur de la taille des mots de la ligne suivante
                }
                line += 1;
                value = masterReader.ReadLine(); // Lecture de la prochaine ligne 
            }

            masterReader.Close();
        }

        public Dictionnaire (string lang)
        {
            switch (lang) // 2 langues : Francais (FR) et Anglais (EN)
            {
                case "FR":
                    this.lang = lang;
                    break;
                case "EN":
                    this.lang = lang;
                    break;
                default: // La langue par default est mise en Francais
                    this.lang = "FR";
                    break;
            }
            GetTabWords();
        }

        /// <summary>
        /// Retourne la liste de tous les mots de memes longueurs que le mot passe en parametres
        /// </summary>
        /// <param name="word"> Liste  </param>
        /// <returns> Retourne la liste de tous les mots de memes longueurs que le mot passe en parametres </returns>
        public List<string> WordList (string word)
        {
            List<string> list = new List<string>(); // Initialisaiton de la liste de retour
            string[] tabRet = null; // Pré-table pour récupérer tous les mots de la meme taille que le mot passe en parametres
            int index = 0; // Donne l'index correspondant à la taille du mot dans le tableau de mots
            int scaleWord = word.Length; // Taille du mot passe en parametres
            bool hit = false; // Variable d'etat verifiant si l'index a ete trouve

            for (int i = 0; i < lengthWords.Length && !hit; i++) // Boucle parcourant toutes les tailles de mots du dictionnaire
            {
                if (scaleWord == lengthWords[i])
                {
                    hit = true;
                    index = i;
                    tabRet = dicoTab[i].Split(" "); // Decoupage de la chaine de caracteres contenant les mots souhaites
                }
            }

            //Console.WriteLine(dicoTab[index]);

            for (int j = 0; j < tabRet.Length; j++) // Boucle permettant de former la liste contenant tous les mots de memes tailles que word
            {
                list.Add(tabRet[j]); 
            }
            return list;
        }

        /// <summary>
        /// Fonction qui retourne toutes les informations sur le dictionnaire courant
        /// </summary>
        /// <returns> Nombres de mots par taille (dans une chaine de caracteres formatee) </returns>
        public override string ToString()
        {
            string retValue = ""; // Initialisation de la chaine de caractere qui va stocker toutes les informations souhaitees
            for (int i = 0; i < this.dicoTab.Length; i++)
            {
                retValue += $"{this.dicoTab[i].Split(" ").Length} \t {this.lengthWords[i]} \n"; // Formatage de la chaine
            }
            //Console.WriteLine(retValue);
            return retValue;
        }

        /// <summary>
        /// Fonction recursive qui permet de trouver si un mot est dans le dictionnaire ou non
        /// </summary>
        /// <param name="mot"> Mot a trouver </param>
        /// <param name="lineTab"> Liste contenant tous les mots de memes tailles que le mot </param>
        /// <param name="end"> Rang de fin de la liste consideree </param>
        /// <param name="start"> Rang du debut de la liste consideree </param>
        /// <returns></returns>
        public bool RechDichoRecursif(string mot, List<string> lineTab, int end, int start = 0)
        {
            int length = end - start; // Calcul de la taille de la liste consideree
            int midRanq = (start + end) / 2; // Calcul du rang centrale de la liste

            if (length == 1) // Condition d'arret
            {
                if (mot == lineTab[start])
                {
                    return true; // Le mot est dans la liste
                }
                else
                {
                    return false; // Le mot n'est pas dans la liste
                }
            }

            if (lineTab[midRanq].CompareTo(mot) > 0) // Comparaison du mot au centre de la liste et du mot a trouver
            {
                return RechDichoRecursif(mot, lineTab, midRanq, start); // Element a gauche 
            }
            else
            {
                return RechDichoRecursif(mot, lineTab, end, midRanq); // Element a droite
            }
        }
    }
}
