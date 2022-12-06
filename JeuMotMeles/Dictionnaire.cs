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
            if (this.lang == "FR")
            {
                string path = Program.GetFilePath("MotsPossiblesFR.txt"); // Recuperation du chemin d'accès du fichier

                masterReader = new StreamReader(path); // Initialisation du StreamReader

                this.dicoTab = new string[14];
                this.lengthWords = new int[14];
                int line = 0;
                int ranq = 0;
                string valLength = "0";
                string value = "";

                while (value != null)
                {
                    if (line%2 == 0 && line != 0)
                    {
                        this.dicoTab[ranq] = value;
                        this.lengthWords[ranq] = Convert.ToInt32(valLength);
                        ranq += 1;
                    }
                    else
                    {
                        valLength = value;
                    }
                    line += 1;
                    value = masterReader.ReadLine();
                }

                masterReader.Close();
            }
            else if (this.lang == "EN")
            {
                string path = Program.GetFilePath("MotsPossiblesEN.txt"); // Recuperation du chemin d'accès du fichier

                masterReader = new StreamReader(path); // Initialisation du StreamReader
                int numLength = 0; // Compteur de taille differentes

                this.dicoTab = new string[7]; // 7 tailles differentes de mots
                this.lengthWords = new int[8];

                // Lecture de tous les élements du fichier (ligne par ligne)
                string lineGenerate = "";
                string value = masterReader.ReadLine();
                while (value != null)
                {
                    if (value.Length > 1)
                    {
                        lineGenerate += value;
                    }
                    else
                    {
                        if (numLength != 0)
                        {
                            this.dicoTab[numLength - 1] = lineGenerate;
                        }
                        numLength += 1;
                    }

                    value = masterReader.ReadLine();
                }
                masterReader.Close();
            }
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
        /// Retourne la liste de tous les mots de memes longueurs que le mot passé en paramètres
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<string> WordList (string word)
        {
            List<string> list = new List<string>();
            string[] tabRet = null;
            int index = 0;
            int scaleWord = word.Length;
            bool hit = false;
            for (int i = 0; i < lengthWords.Length && !hit; i++)
            {
                if (scaleWord == lengthWords[i])
                {
                    hit = true;
                    index = i;
                    tabRet = dicoTab[i].Split(" ");
                }
            }

            Console.WriteLine(dicoTab[index]);

            for (int j = 0; j < tabRet.Length; j++)
            {
                list.Add(tabRet[j]);
            }
            return list;
        }

        public override string ToString()
        {
            string retValue = "";
            for (int i = 0; i < this.dicoTab.Length; i++)
            {
                retValue += $"{this.dicoTab[i].Split(" ").Length} \t {this.lengthWords[i]} \n";
            }
            //Console.WriteLine(retValue);
            return retValue;
        }

        public bool RechDichoRecursif(string mot, List<string> lineTab, int end, int start = 0)
        {
            int length = end - start;
            if (length == 1) 
            {
                if (mot == lineTab[start])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            int midRanq = (start + end) / 2;
            if (lineTab[midRanq].CompareTo(mot) > 0)
            {
                return RechDichoRecursif(mot, lineTab, midRanq, start);
            }
            else
            {
                return RechDichoRecursif(mot, lineTab, end, midRanq);
            }
        }
    }
}
