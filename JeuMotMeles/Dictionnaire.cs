﻿using System;
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

                dicoTab = new string[14];
                int line = 0;
                int ranq = 0;
                string value = "";

                while (value != null)
                {
                    if (line%2 == 0 && line != 0)
                    {
                        dicoTab[ranq] = value;
                        ranq += 1;
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

        public override string ToString()
        {
            //Console.WriteLine(this.dicoTab.Length);
            for (int i = 0; i < this.dicoTab.Length; i++)
            {
                Console.WriteLine(this.dicoTab[i].Split(" ").Length);
            }
            Console.WriteLine(this.dicoTab[0]);
            return "";
        }
    }
}