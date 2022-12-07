using System;
using System.Collections.Generic;
using System.IO;

namespace JeuMotMeles
{
    public class Program
    {
        /// <summary>
        /// Fonction qui permet d'afficher tous les elements d'une matrice
        /// </summary>
        /// <param name="matrix"> Matrice a afficher </param>
        static void DisplayMatrix(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++) // Parcours des lignes
            {
                for (int j = 0; j < matrix.GetLength(1); j++) // Parcours des colonnes
                {
                    Console.Write($"{matrix[i, j]} "); // Affichage des elements
                }
                Console.WriteLine(); // Retourn a la ligne
            }
        }

        /// <summary>
        /// Fonction qui permet de générer le chemin d'accès pour la lecture et l'écriture d'un fichier dans le répertoire de notre programme
        /// </summary>
        /// <param name="filename"> Nom du fichier (en .csv) </param>
        /// <returns> Chemin d'accès du fichier à écrire / lire </returns>
        public static string GetFilePath (string filename)
        {
            string currentPath = Directory.GetCurrentDirectory(); // Recuperation du repertoir courant
            string[] listPath = currentPath.Split("\\"); // Recuperation des sous repertoir pour avoir toute l'arborescence
            string path = ""; // Initialisation du chemin d'acces

            string[] splitFileName = filename.Split(".");
            if (splitFileName.Length == 2) // Vérification du nom du fichier au format : XX.format
            {
                if (splitFileName[1] == "csv") // Vérification du format du fichier au .csv
                {
                    for (int i = 0; i < listPath.Length - 3; i++) // Parcours de toute l'arborescence en partant du debut jusqu'au repertoir du projet
                    {
                        path += $"{listPath[i]}/"; // Generation de la racine du chemin d'acces
                    }
                }
                else
                {
                    Console.WriteLine($"<FileType> : Class Program - GetFilePath | .{splitFileName[1]} format");
                    for (int i = 0; i < listPath.Length - 3; i++) // Parcours de toute l'arborescence en partant du debut jusqu'au repertoir du projet
                    {
                        path += $"{listPath[i]}/"; // Generation de la racine du chemin d'acces
                    }
                }
            }
            else
            {
                Console.WriteLine("<EmptyFileName> : Class Program - GetFilePath");
            }
            return path + filename;
        }
        public static void Affichage()
        {
            Console.WriteLine(" La partie commence ");
            Console.WriteLine("Choisissez la langue (FR/EN)");
            string langue = Console.ReadLine();
            Console.WriteLine("Joueur 1 , ecrivez votre prenom ");
            string prenom1 = Console.ReadLine();
            Joueur joueur_1 = new Joueur(prenom1);
            Console.WriteLine("Joueur 2 , ecrivez votre prenom ");
            string prenom2 = Console.ReadLine();
            Joueur joueur_2 = new Joueur(prenom2);

            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));


        }


        static void Main(string[] args)
        {
            string filePath = GetFilePath("CasSimple.csv");
            Plateau matrixGame = new Plateau();
            matrixGame.ToRead(filePath);
            Console.WriteLine(matrixGame.ToString());
            //filePath = GetFilePath("test.csv");
            //matrixGame.ToFile(filePath);

            Dictionnaire dicoFr = new Dictionnaire("FR");
            //Dictionnaire dicoEn = new Dictionnaire("EN");
            //Console.WriteLine(matrixGame.Test_Plateau("test", 1, 1, "SE"));
            dicoFr.ToString();
            List<string> list = new List<string>();
            string wordToFind = "CACA";
            list = dicoFr.WordList(wordToFind);
            bool result = dicoFr.RechDichoRecursif(wordToFind, list, list.Count - 1);
            Console.WriteLine(result);
        }
    }
}
