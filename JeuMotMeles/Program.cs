using System;

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

        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\33782\\Downloads\\CasSimple.csv";
            Plateau matrixGame = new Plateau();
            matrixGame.ToRead(filePath);
            Console.WriteLine(matrixGame.ToString());
            Console.WriteLine(matrixGame.Test_Plateau("test", 1, 1, "SE"));
        }
    }
}
