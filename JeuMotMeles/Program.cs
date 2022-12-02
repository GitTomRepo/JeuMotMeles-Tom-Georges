using System;
using System.IO;

namespace JeuMotMeles
{
    internal class Program
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

        static string GetFilePath (string filename) // TO DO : Vérifier que le fichier est au format csv
        {
            string currentPath = Directory.GetCurrentDirectory(); // Recuperation du repertoir courant
            string[] listPath = currentPath.Split("\\"); // Recuperation des sous repertoir pour avoir toute l'arborescence
            string path = ""; // Initialisation du chemin d'acces

            string[] splitFileName = filename.Split(".");
            if (splitFileName.Length == 2)
            {
                if (splitFileName[1] == "csv")
                {
                    for (int i = 0; i < listPath.Length - 3; i++) // Parcours de toute l'arborescence en partant du debut jusqu'au repertoir du projet
                    {
                        path += $"{listPath[i]}/"; // Generation de la racine du chemin d'acces
                    }
                }
            }
            for (int i = 0; i < listPath.Length - 3; i++) // Parcours de toute l'arborescence en partant du debut jusqu'au repertoir du projet
            {
                path += $"{listPath[i]}/"; // Generation de la racine du chemin d'acces
            }
            return path + filename;
        }

        static void Main(string[] args)
        {
            string filePath = GetFilePath("CasSimple.csv");
            Console.WriteLine(filePath);
            Plateau matrixGame = new Plateau();
            matrixGame.ToRead(filePath);
            Console.WriteLine(matrixGame.ToString());
            matrixGame.ToFile("test.txt");

            //Console.WriteLine(matrixGame.Test_Plateau("test", 1, 1, "SE"));
        }
    }
}
