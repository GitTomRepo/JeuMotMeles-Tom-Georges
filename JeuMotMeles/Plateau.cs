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
        private string[,] matrix;
        private int level;
        private string[] words;

        public Plateau ()
        {
            this.level = 0;
            
            this.matrix = null;
            this.words = null;
        }

        private string DisplayMatrix ()
        {
            string matrixLines = "\t";
            if (this.matrix != null)
            {
                for (int i = 0; i < this.matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < this.matrix.GetLength(1); j++)
                    {
                        matrixLines += this.matrix[i, j] + " ";
                    }
                    matrixLines += "\n\t";
                }
            }
            else
            {
                matrixLines = "<NullMatrix : Class Plateau - DisplayMatrix";
            }

            return matrixLines;
        }

        private string DisplayWords ()
        {
            string tabWords = "";
            if (this.words != null)
            {
                for (int i = 0; i < this.words.Length; i++)
                {
                    tabWords += words[i] + " ";
                }
            }
            else
            {
                tabWords = "<NullTab : Class Plateau - DisplayWords";
            }

            return tabWords;
        }

        public string ToString ()
        {
            return $"Niveau : {this.level} - Mots à trouver : {DisplayWords()}\nMatrice : \n{DisplayMatrix()}";
        }

        public void ToFile (string nomFile) // TODO : Replace all the content of the nomFile.csv in the current directory by the actual content
        {
            //StreamWriter masterWriter = new StreamWriter(nomFile);

            int maxCol = 4;
            int colMatrix = this.matrix.GetLength(1);
            int lineMatrix = this.matrix.GetLength(0);
            int numWords = this.words.Length;

            int maxLine = 2 + lineMatrix;
            string[] lines = new string[maxLine];

            string lineContent;

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
                        // Line 1
                        if (j < 4)
                        {
                            switch (j)
                            {
                                case 0:
                                    lineContent += $"{this.level}";
                                    break;
                                case 1:
                                    lineContent += $"{lineMatrix}";
                                    break;
                                case 2:
                                    lineContent += $"{colMatrix}";
                                    break;
                                case 3:
                                    lineContent += $"{numWords}";
                                    break;
                            }
                        }
                    }
                    else if (i == 1)
                    {
                        // Line 2
                        if (j < numWords)
                        {
                            lineContent += this.words[j];
                        }
                    }
                    else
                    {
                        // Matrix
                        if (j < colMatrix)
                        {
                            lineContent += this.matrix[i - 2, j];
                        }
                    }

                    if (j != maxCol - 1) // Prevent from adding ";" at the end of the line
                    {
                        lineContent += ";";
                    }                    
                }
                Console.WriteLine(lineContent);
                lines[i] = lineContent; 
            }

            //masterWriter.Close();
        }

        public void ToRead (string nomfile)
        {
            StreamReader masterReader = new StreamReader(nomfile);
            List<string> lines = new List<string>();

            string value = masterReader.ReadLine();
            while (value != null)
            {
                lines.Add(value);
                value = masterReader.ReadLine();
            }

            string[] tabLineInfo = lines[0].Split(";");
            string[] tabLineWords = lines[1].Split(";");

            int level = Convert.ToInt32(tabLineInfo[0]);
            int nbLines = Convert.ToInt32(tabLineInfo[1]);
            int nbCol = Convert.ToInt32(tabLineInfo[2]);
            int nbWords = Convert.ToInt32(tabLineInfo[3]);

            string[] words = new string[nbWords];
            for (int i = 0; i < nbWords; i++)
            {
                words[i] = tabLineWords[i];
            }

            string[,] matrix = new string[nbLines, nbCol];
            string[] tabValues = new string[nbCol];
            this.matrix = new string[nbLines, nbCol];

            for (int j = 0; j < nbLines; j++)
            {
                tabValues = lines[j + 2].Split(";");
                for (int k = 0; k < nbCol; k++)
                {
                    this.matrix[j, k] = tabValues[k];
                }
            }

            this.level = level;
            this.words = tabLineWords;
            masterReader.Close();
        }

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
                switch (direction)
                {
                    case "N":
                        endCol = startCol;
                        endLine = startLine - lenWord;
                        break;

                    case "S":
                        endCol = startCol;
                        endLine = startLine + lenWord;
                        break;

                    case "E":
                        endCol = startCol + lenWord;
                        endLine = startLine;
                        break;

                    case "O":
                        endCol = startCol - lenWord;
                        endLine = startLine;
                        break;

                    case "NE":
                        endCol = startCol + lenWord;
                        endLine = startLine - lenWord;
                        break;

                    case "SE":
                        endCol = startCol + lenWord;
                        endLine = startLine + lenWord;
                        break;

                    case "NO":
                        endCol = startCol - lenWord;
                        endLine = startLine - lenWord;
                        break;

                    case "SO":
                        endCol = startCol - lenWord;
                        endLine = startLine + lenWord;
                        break;

                    default:
                        Console.WriteLine("[!] Invalid direction");
                        break;
                }

                if (endLine >= 0 && endCol >= 0 && endLine < this.matrix.GetLength(0) && endCol < this.matrix.GetLength(1))
                {
                    state = true;
                }
                Console.WriteLine($"End coord : {endLine}, {endCol}");
            }
            return state;
        }

        public void GenerePlateau ()
        {

        }
    }
}
