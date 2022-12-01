using System;
using System.IO;

namespace JeuMotMeles
{
    internal class Program
    {
        static void DisplayMatrix(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        static string GetFilePath (string filename) 
        {
            string currentPath = Directory.GetCurrentDirectory();
            string[] listPath = currentPath.Split("\\");
            string path = "";

            for (int i = 0; i < listPath.Length - 3; i++)
            {
                path += $"{listPath[i]}/";
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
